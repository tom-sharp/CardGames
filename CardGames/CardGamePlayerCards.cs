using Syslib;
using Syslib.Games.Card;


namespace Games.Card
{
	public class CardGamePlayerCards : ICardGamePlayerCards
	{
		public CardGamePlayerCards()
		{
			this.privatecards = new CList<IPlayCard>();
			this.publiccards = new CList<IPlayCard>();
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


		public void TakePrivateCard(IPlayCard card)
		{
			if (card == null) return;
			this.privatecards.Add(card);
		}


		public void TakePublicCard(IPlayCard card)
		{
			if (card == null) return;
			this.publiccards.Add(card);
		}


		public CList<IPlayCard> GetCards()
		{
			return new CList<IPlayCard>().Add(this.privatecards).Add(this.publiccards);
		}


		public CList<IPlayCard> GetPrivateCards()
		{
			return new CList<IPlayCard>().Add(this.privatecards);
		}


		public CList<IPlayCard> GetPublicCards()
		{
			return new CList<IPlayCard>().Add(this.publiccards);
		}

		public string HandName { get; set; }
		public bool WinHand { get; set; }


		readonly CList<IPlayCard> privatecards;
		readonly CList<IPlayCard> publiccards;

	}
}
