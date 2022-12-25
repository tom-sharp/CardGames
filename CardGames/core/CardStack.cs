using System;
using Syslib;


namespace Games.Card
{

	/// <summary>
	/// A card stack is a stack of cards consisting of a number of decks, where cards are drawn from
	/// </summary>
	class CardStack : ICardStack
	{
		/// Init stack of cards with number of decks. A standard deck of 52 cards is used as base if
		/// no deck template is provided (default)
		public CardStack(int decks, CardDeck decktemplate = null)
		{
			int cards;
			Card card;
			var basedeck = decktemplate;

			if (basedeck == null) basedeck = new CardDeck();
			cards = basedeck.CardsTotal * decks;

			if (cards > 0)
			{
				this.stack = new Card[cards];
				int counter = 0;
				while (counter < cards)
				{
					card = basedeck.NextCard(firstcard: true);
					while ((card != null) && (counter < cards))
					{
						this.stack[counter++] = card;
						card = basedeck.NextCard();
					}
				}
			}

			this.nextcard = 0;
		}


		/// return total number of cards in deck
		public int CardsTotal { get { if (this.stack == null) return 0; return this.stack.Length; } }


		/// return number of undrawn cards 
		public int CardsLeft { get { if (this.stack == null) return 0; return this.stack.Length - this.nextcard; } }



		/// return next card in stack or null if no more undrawn cards
		/// if firstcard is set to true the current card pointer will reset to the first card before returning card
		public Card NextCard(bool firstcard = false)
		{
			if (firstcard) this.nextcard = 0;
			if ((this.stack != null) && (nextcard < this.stack.Length)) return stack[nextcard++];
			return null;
		}


		/// will shuffle deck of cards and position card pointer to the first card
		public void ShuffleCards()
		{
			CRandom.Random.Shuffle<Card>(this.stack);
			this.nextcard = 0;
		}

		public void SortCards()
		{
			Array.Sort<Card>(this.stack, comparecards);
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
		private Card[] stack = null;

	}
}
