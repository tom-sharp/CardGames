

namespace Games.Card
{
	public abstract class CardGameDealer : ICardGameDealer
	{
		public CardGameDealer(ICardGameTable gametable)
		{
			this.gametable = gametable;
		}

		abstract public bool DealRound();


		protected ICardGameTable gametable = null;

	}
}
