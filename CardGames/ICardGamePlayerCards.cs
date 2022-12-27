using Syslib;
using Syslib.Games.Card;

namespace Games.Card
{
	public interface ICardGamePlayerCards
	{

		public void ClearHand();


		public void TakePrivateCard(IPlayCard card);


		public CList<IPlayCard> GetPrivateCards();


		public void TakePublicCard(IPlayCard card);


		public CList<IPlayCard> GetPublicCards();


		public CList<IPlayCard> GetCards();

		public string HandName { get; set; }
		public bool WinHand { get; set; }


	}
}