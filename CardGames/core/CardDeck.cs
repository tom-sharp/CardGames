using System;
using Syslib;

namespace Games.Card
{
	/// <summary>
	/// A Deck of cards considts of 52 cards and requested number of jokers
	/// the base 52 cards will hold 13 cards of each of the four suits Hearts, Diamonds, Spades and Clubs
	/// ranked from 2-14 
	/// </summary>
	public class CardDeck : ICardStack
	{

		public CardDeck(int jokers = 0)
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

		/// return total number of cards in deck
		public int CardsTotal { get { return deck.Length; } }


		/// return number of undrawn cards 
		public int CardsLeft { get { return deck.Length - nextcard; } }


		/// return next card in deck or null if no more undrawn cards
		/// if firstcard is set to true the current card pointer will reset to the first card before returning card
		public Card NextCard(bool firstcard = false)
		{
			if (firstcard) this.nextcard = 0;
			if (nextcard < this.deck.Length) return deck[nextcard++];
			return null;
		}


		/// will shuffle deck of cards and position card pointer to the first card
		public void ShuffleCards()
		{
			CRandom.Random.Shuffle<Card>(deck);
			nextcard = 0;
		}

		public void SortCards() {
			Array.Sort<Card>(this.deck, comparecards);
			this.nextcard = 0;
		}

		private int comparecards(Card x, Card y)
		{
			if (x.Suite > y.Suite) return 1;
			if (x.Suite < y.Suite) return -1;
			if (x.Rank > y.Rank) return 1;
			if (x.Rank < y.Rank) return -1; 
			return 0;
		}




		int nextcard = 0;
		private Card[] deck = null;
	}

}
