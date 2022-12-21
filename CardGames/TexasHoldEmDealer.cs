using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syslib;

namespace Games.Card
{

	/*
		  Texas Hold'em

		  Each player recieves two private cards and there after each player have three option;
		   - fold (will not participate in this round any longer)
		   - call (add required bet to the pot)
		   - raise (add required bet to the pot and add another bet, require other players to commit again)

		  The first player out is the one to the left of the dealer, rotating clockwise
		  When starting the round a minimum bet is required to participate in the game round
		  and the two first players to the left of dealer are required to place a half and a full bet.

		  Therafter dealer place three public cards on table that is common for everyone, now have a total
		  of five cards each, yet again remaining players make a choice fold, call/check or raise.

		  Thereafter dealer place a fourth public card on the table, and remaining players decide if they
		  will fold, call/check or raise.

		  Finally dealer place a fifth card on the table and now there is a total of five public common cards
		  and two private cards. Reamining players make their decissions (fold,call/check or raise).

		  When the final bet is placed, remaining players show their hand and the one with higest ranked hand win the pot.
		  If two or more players have the same rank, highest card value of the rank wins, or if the rank card value are
		  same the pot is split

		  Number of maximum required cards in one round is (5 + players * 2):
			2 players = 9 cards		3 players = 11 cards		4 players = 13 cards	5 players = 15 cards
			6 players = 17 cards	7 players = 19 cards		8 players = 21 cards	9 players = 23 cards
			10 players = 25 cards. 

		  Texas Hold'em utilizes 1 deck and deck is shuffled after each round

	*/


	public class TexasHoldEmDealer : CardGameDealer, ICardGameDealer
	{

		public TexasHoldEmDealer(CardGameTable gametable) :base(gametable) {
			this.cardStack = new CardStack(decks: 1);
			this.texasplayer = new TexasHoldEmPlayer(gametable);
			this.tableseatrank = new CList<TexasHoldEmHandRank>();
			this.firstCardSeat = null;
			this.lastBetRaiseSeat = null;
		}


		override public bool Run(CardGameTable gametable)
		{
			// Game Round
			this.gametable = gametable;
			if (this.gametable == null) return false;
			if (SetUpGameRound() < 2) return false;
			if (!PlaceInitialBets()) { RollBackBets(); return false; }


			Console.WriteLine("------------------ NEW ROUND ----------------");

			this.cardStack.ShuffleCards();                  // shuffle deck
			DealPlayerCards(cards: 1);						// deal 1 card to each active player
			DealPlayerCards(cards: 1);                      // deal 1 card to each active player
			PlaceBets();
			CollectPlayerBets();
			DealPublicCards(cards: 3);                      // deal 3 public cards (dealer hand)
			PlaceBets();
			CollectPlayerBets();
			DealPublicCards(cards: 1);                      // deal 1 public cards (dealer hand)
			PlaceBets();
			CollectPlayerBets();
			DealPublicCards(cards: 1);                      // deal 1 public cards (dealer hand)
			PlaceBets();
			CollectPlayerBets();
			FindWinner();




			// GAME CODE HERE
			// 1. track seat first card reciever
			// 2. forced bet for the two first players - trac last raise
			// 3. deal cards, two private for each player
			// 4. take bets
			// 5. deal three common public cards
			// 6. take bets
			// 7. deal another public card
			// 8. take bets
			// 9. deal the fifth public comon card
			// 10 take bets
			// 11 decide who wins the pot and distribute earnings
			// 12. reset all player hands
			// 13. return to look for add or remove players

			Console.WriteLine($"Texas Hold'em");
			int counter = 0;
			CList<Card> playerhand;

			Console.Write($" Seat {counter,2}. ");
			Console.Write($"{this.gametable.DealerSeat.Name,-15}  {this.gametable.DealerSeat.Tokens,10}  {this.gametable.DealerSeat.Active}  ");
			playerhand = this.gametable.DealerSeat.ShowCards();
			foreach (var card in playerhand)
			{
				Console.Write($"  {card.Symbol}");
			}
			Console.Write("\n");
			counter++;

			foreach (var p in this.gametable.TableSeats) { 
				if (!p.IsFree()) {
					Console.Write($" Seat {counter,2}. ");
					Console.Write($"{p.Name,-15}  {p.Tokens,10}  {p.Active}  ");
					playerhand = p.ShowCards();
					foreach (var card in playerhand) {
						Console.Write($"  {card.Symbol}");
					}
					Console.Write("\n");
				}
				counter++;
			}
			return true;
		}


		// Loop through all seats and make players ready. Return total number of players
		private int SetUpGameRound()
		{
			int count = 0;
			this.requiredbet = 1;
			this.gametable.CollectPotTokens();
			this.tableseatrank.Clear();

			this.gametable.DealerSeat.NewRound();

			foreach (var seat in this.gametable.TableSeats)
			{
				if (!seat.IsFree()) {
					seat.NewRound();
					this.tableseatrank.Add(new TexasHoldEmHandRank() { TableSeat = seat });
					count++;
				}
			}
			return count;
		}


		// this will advance trackers for first card seat as well as last bet Raise seat
		// expected that at least two active players exist to call this function
		private bool PlaceInitialBets()
		{
			this.firstCardSeat = this.gametable.NextActiveSeat(this.firstCardSeat);
			this.lastBetRaiseSeat = this.gametable.NextActiveSeat(this.firstCardSeat);
			if ((this.firstCardSeat == null) || (this.lastBetRaiseSeat == null)) return false;

			if (!this.firstCardSeat.PlaceBet(tokens: this.requiredbet)) return false;
			this.requiredbet *= 2;
			if (!this.lastBetRaiseSeat.PlaceBet(tokens: this.requiredbet)) return false;

			return true;
		}

		// ask for bets around the table until all active player has placed bets and ther is still at least two player
		private void PlaceBets() {

			// If run immediately after init bets: first seat to be asked is next active after last raise 
			// If run in normal bet roundfirst to ask is fistcard seat (may have folded and not active)

			CardGameTableSeat seat = this.gametable.NextActiveSeat(this.lastBetRaiseSeat);
			if (this.lastBetRaiseSeat == null) seat = this.firstCardSeat;
			while (seat != this.lastBetRaiseSeat) {
				if (seat.Active) {
					if (seat.AskBet(this.requiredbet - seat.Bets)) {
						if (this.requiredbet < seat.Bets) {
							this.requiredbet = seat.Bets;
							this.lastBetRaiseSeat = seat;
//							Console.WriteLine($" - Player {seat.Name} raised  {seat.Bets}");
						}
//						else Console.WriteLine($" - Player {seat.Name} called  {seat.Bets}");
					}
//					else Console.WriteLine($" - Player {seat.Name} folded  {seat.Bets}");

					if (this.lastBetRaiseSeat == null) this.lastBetRaiseSeat = seat;
				}
				seat = this.gametable.NextActiveSeat(seat);
			}
			this.lastBetRaiseSeat = null;
			this.requiredbet = 0;

		}


		private void CollectPlayerBets() {

			foreach (var seat in this.gametable.TableSeats) {
				if (seat != null) this.gametable.AddPotTokens(seat.CollectBet());
			}

		}

		// roll back bets made, as for some reson round was not completed
		private void RollBackBets() {
			foreach (var seat in this.gametable.TableSeats) {
				if (seat != null) seat.ReturnBet();
			}
		}

		// Deal 1 card around the table
		private bool DealPlayerCards(int cards) {
			CardGameTableSeat seat = this.firstCardSeat;
			int card = 0;

			if (cards < 1) return false;

			if (this.cardStack.CardsLeft < (cards * this.gametable.CountActiveSeats)) this.cardStack.ShuffleCards();
			seat.TakePrivateCard(this.cardStack.NextCard());
			seat = this.gametable.NextActiveSeat(seat);

			while (seat != this.firstCardSeat) {
				card = 0;
				while (card < cards) { seat.TakePrivateCard(this.cardStack.NextCard()); card++; }
				seat = this.gametable.NextActiveSeat(seat);
			}
			return true;
		}

		private bool DealPublicCards(int cards) {
			int card = 0;
			if (cards > this.cardStack.CardsTotal) return false;
			if (this.cardStack.CardsLeft < cards + 1) this.cardStack.ShuffleCards();
			while (card++ < cards) { this.gametable.DealerSeat.TakePublicCard(this.cardStack.NextCard()); }
			return true;
		}

		private void FindWinner() {
			foreach (var rank in this.tableseatrank) {
				if (rank.TableSeat.Active)
				{
					if (rank.TableSeat == this.gametable.DealerSeat) rank.RankHand(rank.TableSeat.ShowCards());
					else rank.RankHand(rank.TableSeat.ShowCards().Add(this.gametable.DealerSeat.ShowCards()));
				}

			}

			this.tableseatrank.Sort(SortRank);
			Console.WriteLine("winner is:");
			foreach (var rank in this.tableseatrank) {
				Console.WriteLine($" {rank.TableSeat.Name,-15}  {rank.Hand,-15} {rank.Value,10}");
			}
			
		}

		private bool SortRank(TexasHoldEmHandRank rank1, TexasHoldEmHandRank rank2) {
			if (rank1.Hand < rank2.Hand) return true;
			if (rank1.Hand == rank2.Hand) { if (rank1.Value < rank2.Value) return true; }
			return false;
		}

		CardStack cardStack;
		TexasHoldEmPlayer texasplayer = null;
		CList<TexasHoldEmHandRank> tableseatrank = null;

		int requiredbet;
		CardGameTableSeat firstCardSeat;    // player seat that recieces the first card in a deal around the table
		CardGameTableSeat lastBetRaiseSeat;		// player that placed the last bet and raised, requiring other to place bets
	}


}
