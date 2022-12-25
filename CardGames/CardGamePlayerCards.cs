using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syslib;

namespace Games.Card
{
	public class CardGamePlayerCards : ICardGamePlayerCards
	{
		public CardGamePlayerCards()
		{
			this.privatecards = new CList<Card>();
			this.publiccards = new CList<Card>();
			this.HandName = "";
			this.WinHand = false;
		}


		public void ClearHand()
		{
			this.privatecards.Clear();
			this.publiccards.Clear();
			this.HandName = "";
			this.WinHand = false;
		}


		public void TakePrivateCard(Card card)
		{
			if (card == null) return;
			this.privatecards.Add(card);
		}


		public void TakePublicCard(Card card)
		{
			if (card == null) return;
			this.publiccards.Add(card);
		}


		public CList<Card> GetCards()
		{
			return new CList<Card>().Add(this.privatecards).Add(this.publiccards);
		}


		public CList<Card> GetPrivateCards()
		{
			return new CList<Card>().Add(this.privatecards);
		}


		public CList<Card> GetPublicCards()
		{
			return new CList<Card>().Add(this.publiccards);
		}

		public string HandName { get; set; }
		public bool WinHand { get; set; }


		readonly CList<Card> privatecards;
		readonly CList<Card> publiccards;

	}
}
