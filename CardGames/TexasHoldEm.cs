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


	public class TexasHoldem {

		public TexasHoldem() {
			this.cardStack = new CardStack(1);
			this.cardStack.ShuffleCards();
			this.players = new CList<ICardPlayer>();
		}

		public void AddPlayer(ICardPlayer player) {
			this.players.Add(player);
		}
		public void RemovePlayer(ICardPlayer player) {
			if (this.players.IsExist(player)) this.players.Remove();
		}

		public void Run() {
		}

		CList<ICardPlayer> players;
		CardStack cardStack; 
	}

	/// <summary>
	/// Value rank of a Texas Hold Em hand (2 private cards + 5 common public cards)
	/// </summary>
	public enum TexasHoldEmRank { Nothing = 0, Card2, Card3, Card4, Card5, Card6, Card7, Card8, Card9, Card10, Card11, Card12, Card13, Card14, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalStraightFlush }

}
