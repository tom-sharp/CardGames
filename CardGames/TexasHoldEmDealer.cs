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
			this.player = new TexasHoldEmPlayer();
			this.firstCardSeat = 0;
			this.lastBetRaiseSeat = 0;
		}


		public bool Run(CardGameTableSeat[] players)
		{
			// Game Round
			if (ActivatePlayers(players) < 2) return false;			// minimum two players req to start playing
			if (!PlaceInitialBets(players)) return false;			// failed placing initial bets abort round
			this.cardStack.ShuffleCards();
			DealPlayerCards(players);
			DealPlayerCards(players);

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
			foreach (var p in players) { 
				Console.Write($" Seat {counter,2}. ");
				if (p.IsFree()) Console.WriteLine("Empty");
				else Console.WriteLine($"{p.Name,-15}  {p.Tokens,10}  {p.Active}");
				counter++;
			}

			return false;
		}


		// Loop through all seats and make players ready. Return total number of players
		private int ActivatePlayers(CardGameTableSeat[] players)
		{
			int count = 0, pos = 0;
			if (players == null) return 0;
			while (pos < players.Length) { if (!players[pos].IsFree()) { players[pos].NewRound(); count++; } pos++; }
			return count;
		}


		// this will advance trackers for first card seat as well as last bet Raise seat
		// expected that at least two active players exist to call this function
		private bool PlaceInitialBets(CardGameTableSeat[] players)
		{
			this.firstCardSeat = NextActivePlayer(players, this.firstCardSeat);
			this.lastBetRaiseSeat = NextActivePlayer(players, this.firstCardSeat);
			if ((this.firstCardSeat == 0) || (this.lastBetRaiseSeat == 0)) return false;

			players[firstCardSeat].RequiredBet(tokens: 1);
			players[lastBetRaiseSeat].RequiredBet(tokens: 2);

			return true;
		}

		// Deal around the table 1 card
		private bool DealPlayerCards(CardGameTableSeat[] players) {
			int seat = this.firstCardSeat;
			if (this.cardStack.CardsLeft < 15) this.cardStack.ShuffleCards();
			players[seat].TakePrivateCard(this.cardStack.NextCard());
			seat = NextActivePlayer(players, this.firstCardSeat);
			while (seat != this.firstCardSeat) {
				players[seat].TakePrivateCard(this.cardStack.NextCard());
				seat = NextActivePlayer(players, seat);
			}
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
		TexasHoldEmPlayer player = null;
		int firstCardSeat;			// player seat that recieces the first card in a deal around the table
		int lastBetRaiseSeat;		// player that placed the last bet and raised, requiring other to place bets
	}

	/// <summary>
	/// Value rank of a Texas Hold Em hand (2 private cards + 5 common public cards)
	/// </summary>
	public enum TexasHoldEmRank { Nothing = 0, Card2, Card3, Card4, Card5, Card6, Card7, Card8, Card9, Card10, Card11, Card12, Card13, Card14, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalStraightFlush }

}
