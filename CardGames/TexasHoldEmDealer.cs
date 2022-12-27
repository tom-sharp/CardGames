using System;
using Syslib;
using Syslib.Games.Card;

namespace Games.Card.TexasHoldEm
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


	public class TexasHoldEmDealer : CardGameDealer
	{

		public TexasHoldEmDealer(ICardGameTable gametable, ITexasHoldEmIO inout) :base(gametable) {
			this.cardStack = new PlayCardStack(decks: 1);
			this.tableseatrank = new CList<TexasHoldEmHandRank>();
			this.firstCardSeat = null;
			this.lastBetRaiseSeat = null;
			this.IO = inout;
			this.cardplayer = new TexasHoldEmPlayer(this.gametable, this.IO);
		}


		override public bool DealRound()
		{
			// Game Round
			if (SetUpGameRound() < 2) return false;
			if (!PlaceInitialBets()) { RollBackBets(); return false; }
			this.PlayRound();
			return true;
		}


		// Loop through all seats and make players ready. Return total number of players
		private int SetUpGameRound()
		{
			int count = 0;

			if (this.gametable == null) return 0;

			this.requiredbet = 1;
			this.gametable.TablePot.Clear();
			this.tableseatrank.Clear();

			this.gametable.DealerSeat.NewRound();

			foreach (var seat in this.gametable.TableSeats)
			{
				if (!seat.IsFree) {
					seat.NewRound();
					this.tableseatrank.Add(new TexasHoldEmHandRank() { TableSeat = seat });
					count++;
				}
			}
			return count;
		}


		private void PlayRound() {
			this.IO.ShowNewRound(this.gametable);
			this.cardStack.ShuffleCards();                  // shuffle deck
			DealPlayerCards(cards: 1);                      // deal 1 card to each active player
			DealPlayerCards(cards: 1);                      // deal 1 card to each active player
			PlaceBets();
			DealPublicCards(cards: 3);                      // deal 3 public cards (dealer hand)
			PlaceBets();
			DealPublicCards(cards: 1);                      // deal 1 public cards (dealer hand)
			PlaceBets();
			DealPublicCards(cards: 1);                      // deal 1 public cards (dealer hand)
			PlaceBets();
			FindWinner();
			this.IO.ShowRoundSummary(this.gametable);
			Statistics();
		}



		// this will advance trackers for first card seat as well as last bet Raise seat
		// expected that at least two active players exist to call this function
		private bool PlaceInitialBets()
		{
			this.firstCardSeat = this.gametable.NextActiveSeat(this.firstCardSeat);
			this.lastBetRaiseSeat = this.gametable.NextActiveSeat(this.firstCardSeat);
			if ((this.firstCardSeat == null) || (this.lastBetRaiseSeat == null)) return false;
			this.cardplayer.PlaceBet(seat: this.firstCardSeat, tokens: this.requiredbet);
			this.requiredbet *= 2;
			this.cardplayer.PlaceBet(seat: this.lastBetRaiseSeat, tokens: this.requiredbet);
			this.IO.ReDrawGameTable(this.gametable);
			return true;
		}

		// ask for bets around the table until all active player has placed bets and ther is still at least two player
		private void PlaceBets() {

			// If run immediately after init bets: first seat to be asked is next active after last raise 
			// If run in normal bet roundfirst to ask is fistcard seat (may have folded and not active)

			ICardGameTableSeat seat = this.gametable.NextActiveSeat(this.lastBetRaiseSeat);
			if (this.lastBetRaiseSeat == null) seat = this.firstCardSeat;
			while (seat != this.lastBetRaiseSeat) {
				if (this.gametable.ActiveSeatCount < 2) break;
				if (seat.IsActive) {
					this.cardplayer.AskBet(seat, this.requiredbet - seat.Bets);
					if (this.requiredbet < seat.Bets) {
						this.requiredbet = seat.Bets;
						this.lastBetRaiseSeat = seat;
					}
					this.IO.ShowProgressMessage(seat.Comment);
					if (this.lastBetRaiseSeat == null) this.lastBetRaiseSeat = seat;
					this.IO.ReDrawGameTable(this.gametable);
				}
				seat = this.gametable.NextActiveSeat(seat);
			}
			this.CollectPlayerBets();
			this.lastBetRaiseSeat = null;
			this.requiredbet = 0;

		}


		private void CollectPlayerBets() {

			foreach (var seat in this.gametable.TableSeats) {
				if (seat != null) this.gametable.TablePot.AddTokens(seat.CollectBet());
			}

		}

		// roll back bets made, as for some reson round was not completed
		private void RollBackBets() {
			foreach (var seat in this.gametable.TableSeats) {
				if (seat != null) seat.RollbackBet();
			}
		}

		// Deal 1 card around the table
		private bool DealPlayerCards(int cards) {
			ICardGameTableSeat seat = this.firstCardSeat;
			int card = 0;

			if (cards < 1) return false;

			if (this.cardStack.CardsLeft < (cards * this.gametable.ActiveSeatCount)) this.cardStack.ShuffleCards();
			seat.PlayerCards.TakePrivateCard(this.cardStack.NextCard());
			seat = this.gametable.NextActiveSeat(seat);

			while (seat != this.firstCardSeat) {
				card = 0;
				while (card < cards) { seat.PlayerCards.TakePrivateCard(this.cardStack.NextCard()); card++; }
				seat = this.gametable.NextActiveSeat(seat);
			}
			return true;
		}

		private bool DealPublicCards(int cards) {
			int card = 0;
			if (cards > this.cardStack.CardsTotal) return false;
			if (this.cardStack.CardsLeft < cards + 1) this.cardStack.ShuffleCards();
			while (card++ < cards) { this.gametable.DealerSeat.PlayerCards.TakePublicCard(this.cardStack.NextCard()); }
			return true;
		}

		private void FindWinner() {
			TexasHoldEmHand winnerhand = TexasHoldEmHand.Nothing;
			Int64 winnerrank = 0;

			foreach (var rank in this.tableseatrank) {
				rank.RankHand(rank.TableSeat.PlayerCards.GetCards().Add(this.gametable.DealerSeat.PlayerCards.GetCards()));
				rank.TableSeat.PlayerCards.HandName = $"{rank.Hand}";
			}

			this.tableseatrank.Sort(SortRank);

			var WinnersSeats = new CList<ICardGameTableSeat>();
			foreach (var rank in this.tableseatrank) {
				if ((winnerhand == TexasHoldEmHand.Nothing) && (rank.TableSeat.IsActive))
				{
					winnerhand = rank.Hand; winnerrank = rank.Value;
					WinnersSeats.Add(rank.TableSeat);
				}
				else if ((rank.TableSeat.IsActive) && (winnerhand == rank.Hand) && (winnerrank == rank.Value)){ WinnersSeats.Add(rank.TableSeat); }
			}
			int potshare = this.gametable.TablePot.Tokens; 
			if (WinnersSeats.Count() > 1) { potshare /= WinnersSeats.Count(); this.IO.ShowProgressMessage($"Pot is split with {WinnersSeats.Count()} each winning {potshare} tokens"); }
			foreach (var seat in WinnersSeats) {
				seat.PlayerCards.WinHand = true;
				seat.WinTokens(this.gametable.TablePot.RemoveTokens(potshare));
				seat.Comment = $" - Winner {seat.Player.Name} wins  {potshare} tokens and now have {seat.Player.Wallet.Tokens}";

				this.IO.ShowProgressMessage(seat.Comment);
				if ((this.gametable.TablePot.Tokens < potshare) && (this.gametable.TablePot.Tokens > 0)) {
					seat.WinTokens(this.gametable.TablePot.Clear());
					seat.Comment = $" Uneven potshare given to {seat.Player.Name} and now have {seat.Player.Wallet.Tokens}";
					this.IO.ShowProgressMessage(seat.Comment);
				}
			}

		}

		private bool SortRank(TexasHoldEmHandRank rank1, TexasHoldEmHandRank rank2) {
			if (rank1.Hand < rank2.Hand) return true;
			if (rank1.Hand == rank2.Hand) { if (rank1.Value < rank2.Value) return true; }
			return false;
		}


		private void Statistics() {
			var stats = this.gametable.GetStatistics() as TexasHoldEmStatistics;
			if (stats != null) {
				bool winner = true;
				foreach (var rank in this.tableseatrank)
				{
					if ((rank.TableSeat.IsActive) && (winner)) { stats.StatsAddWinner(rank.Hand); winner = false; }
					stats.StatsAddHand(rank.Hand);
				}
			}

		}


		ITexasHoldEmIO IO;
		IPlayCardStack cardStack;
		CList<TexasHoldEmHandRank> tableseatrank = null;
		TexasHoldEmPlayer cardplayer;

		int requiredbet;
		ICardGameTableSeat firstCardSeat;    // player seat that recieces the first card in a deal around the table
		ICardGameTableSeat lastBetRaiseSeat;		// player that placed the last bet and raised, requiring other to place bets
	}


}
