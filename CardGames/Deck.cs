using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syslib;

namespace Games.Card
{
	/// <summary>
	/// A Deck of cards considts of 52 cards and requested number of jokers
	/// the base 52 cards will hold 13 cards of each of the four suits Hearts, Diamonds, Spades and Clubs
	/// ranked from 2-14 
	/// </summary>
	public class Deck
	{
		public Deck(int jokers = 0)
		{
			int cards = 52, card = 0;
			if (jokers > 0)
			{
				if (jokers < 6) cards += jokers;
				else cards += 6;
			}
			deck = new Card[cards];

			// create basic cards
			for (int v = 2; v < 15; v++) { deck[card++] = new Card(CardSuite.Heart, v); }
			for (int v = 2; v < 15; v++) { deck[card++] = new Card(CardSuite.Diamond, v); }
			for (int v = 2; v < 15; v++) { deck[card++] = new Card(CardSuite.Spade, v); }
			for (int v = 2; v < 15; v++) { deck[card++] = new Card(CardSuite.Club, v); }

			// create jokers
			while (card < cards) { deck[card++] = new Card(CardSuite.Joker, 0); }
			this.nextcard = 0;
		}


		// return next card in deck or null if no more undrawn cards
		public Card Next() {
			if (nextcard < this.deck.Length) return deck[nextcard++];
			return null;
		}

		// will shuffle deck of cards and position card pointer to the first card
		public void Shuffle() {
			var rnd = new CRandom();
			rnd.Shuffle<Card>(deck);
			nextcard = 0;
		}

		int nextcard = 0;
		private Card[] deck = null;
	}

}
