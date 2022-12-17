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

		public TexasHoldEmDealer() {
			this.cardStack = new CardStack(decks: 1);
			this.texasplayer = new TexasHoldEmPlayer();
			this.tableplayers = null;
			this.firstCardSeat = 0;
			this.lastBetRaiseSeat = 0;
		}


		override public bool Run(CardGameTableSeat[] players)
		{
			// Game Round
			if ((this.tableplayers = players) == null) return false;
			if (SetUpGameRound() < 2) return false;
			if (!PlaceInitialBets()) { RollBackBets(); return false; }

			this.cardStack.ShuffleCards();					// shuffle deck
			DealPlayerCards();								// deal 1 card to each active player
			DealPlayerCards();                              // deal 1 card to each active player
			DealPublicCards(cards: 3);                      // deal 3 public cards (dealer hand)
			DealPublicCards(cards: 1);                      // deal 1 public cards (dealer hand)
			DealPublicCards(cards: 1);						// deal 1 public cards (dealer hand)


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
			foreach (var p in players) { 
				Console.Write($" Seat {counter,2}. ");
				if (!p.IsFree()) {
					Console.Write($"{p.Name,-15}  {p.Tokens,10}  {p.Active}  ");
					playerhand = p.ShowCards();
					foreach (var card in playerhand) {
						Console.Write($"  {card.Symbol}");
					}
					Console.Write("\n");
				}
				else Console.WriteLine("Empty");
				counter++;
			}

			return false;
		}


		// Loop through all seats and make players ready. Return total number of players
		private int SetUpGameRound()
		{
			int count = 0, pos = 0;
			if (this.tableplayers == null) return 0;
			this.texasplayer.TablePlayers(this.tableplayers);
			this.requiredbet = 1;
			while (pos < this.tableplayers.Length) { if (!this.tableplayers[pos].IsFree()) { this.tableplayers[pos].NewRound(); count++; } pos++; }
			return count;
		}


		// this will advance trackers for first card seat as well as last bet Raise seat
		// expected that at least two active players exist to call this function
		private bool PlaceInitialBets()
		{
			this.firstCardSeat = NextActivePlayer(this.tableplayers, this.firstCardSeat);
			this.lastBetRaiseSeat = NextActivePlayer(this.tableplayers, this.firstCardSeat);
			if ((this.firstCardSeat == 0) || (this.lastBetRaiseSeat == 0)) return false;

			if (!this.tableplayers[firstCardSeat].PlaceBet(tokens: this.requiredbet)) return false;
			this.requiredbet *= 2;
			if (!this.tableplayers[lastBetRaiseSeat].PlaceBet(tokens: 2)) return false;

			return true;
		}

		private void RollBackBets() {
			int seat = 0;
			while (seat < this.tableplayers.Length) {
				this.tableplayers[seat++].ReturnBet();
			}
		}

		// Deal 1 card around the table
		private bool DealPlayerCards() {
			int seat = this.firstCardSeat;
			if (this.cardStack.CardsLeft < 15) this.cardStack.ShuffleCards();
			this.tableplayers[seat].TakePrivateCard(this.cardStack.NextCard());
			seat = NextActivePlayer(this.tableplayers, this.firstCardSeat);
			while (seat != this.firstCardSeat) {
				this.tableplayers[seat].TakePrivateCard(this.cardStack.NextCard());
				seat = NextActivePlayer(this.tableplayers, seat);
			}
			return true;
		}

		private bool DealPublicCards(int cards) {
			int card = 0;
			if (cards > this.cardStack.CardsTotal) return false;
			if (this.cardStack.CardsLeft < cards + 1) this.cardStack.ShuffleCards();
			while (card++ < cards) this.tableplayers[0].TakePublicCard(this.cardStack.NextCard());
			return true;
		}



		private int CountActivePlayers(CardGameTableSeat[] players)	{
			int count = 0, pos = 1;
			while (pos < players.Length) { if (players[pos].Active) count++; pos++; }
			return count;
		}


		// look through list of layers starting at seat startposition, and return next seat number after
		// startposition that have an active player. NOTE that dealer seat is excluded in search and if there is no active
		// player 0 will be returned.
		private int NextActivePlayer(CardGameTableSeat[] players, int startposition) {
			int pos = startposition + 1;

			if (players.Length < 2) return 0;	// only dealer seat exist

			while (pos != startposition) {
				if (pos >= players.Length) pos = 1;
				if ((players[pos] != null) && players[pos].Active) { return pos; }
				pos++;
			}
			return 0;       // no active players
		}


		CardStack cardStack;
		TexasHoldEmPlayer texasplayer = null;
		CardGameTableSeat[] tableplayers = null;
		int requiredbet;
		int firstCardSeat;			// player seat that recieces the first card in a deal around the table
		int lastBetRaiseSeat;		// player that placed the last bet and raised, requiring other to place bets
	}

	/// <summary>
	/// Value rank of a Texas Hold Em hand (2 private cards + 5 common public cards)
	/// </summary>
	public enum TexasHoldEmRank { Nothing = 0, Card2, Card3, Card4, Card5, Card6, Card7, Card8, Card9, Card10, Card11, Card12, Card13, Card14, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalStraightFlush }

}
