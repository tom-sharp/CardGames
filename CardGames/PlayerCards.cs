using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syslib;

namespace Games.Card
{
	public class PlayerCards : IPlayerCards
	{
		public PlayerCards(int maxnumberofcards)
		{
			if (maxnumberofcards > 0)
			{
				cards = new Card[maxnumberofcards];
			}
		}

		public void Reset()
		{
			this.cardcount = 0;
			if (this.cards == null) return;

			int counter = 0;
			while (counter < cards.Length) this.cards[counter++] = null;
		}

		public bool AddCard(Card card)
		{
			if ((this.cards == null) || (card == null)) return false;
			if (cardcount == cards.Length) return false;
			int count = 0;
			while (count < this.cards.Length)
			{
				if (this.cards[count] == null)
				{
					this.cards[cardcount++] = card;
					return true;
				}
				count++;
			}
			return false;
		}

		public bool RemoveCard(Card card)
		{
			if ((this.cards == null) || (card == null)) return false;
			if (cardcount == cards.Length) return false;
			int count = 0;
			while (count < this.cards.Length)
			{
				if ((this.cards[count] != null) && (card.Rank == this.cards[count].Rank) && (card.Suite == this.cards[count].Suite))
				{
					this.cards[count] = null;
					this.cardcount--;
					return true;
				}
				count++;
			}
			return false;
		}

		int cardcount = 0;
		Card[] cards = null;
	}
}
