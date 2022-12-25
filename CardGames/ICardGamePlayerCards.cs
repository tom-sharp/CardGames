using Syslib;

namespace Games.Card
{
	public interface ICardGamePlayerCards
	{

		public void ClearHand();


		public void TakePrivateCard(Card card);


		public CList<Card> GetPrivateCards();


		public void TakePublicCard(Card card);


		public CList<Card> GetPublicCards();


		public CList<Card> GetCards();

		public string HandName { get; set; }
		public bool WinHand { get; set; }


	}
}