using Syslib.Games.Card;

namespace Games.Card
{
	public abstract class CardGamePlayer : ICardGamePlayer
	{
		public CardGamePlayer()
		{	

		}

		public virtual void PlaceBet(ICardGameTableSeat seat, int tokens) {
			seat.PlaceBet(tokens);
		}

		public virtual bool AskBet(ICardGameTableSeat seat, int tokens) {
			seat.PlaceBet(tokens);
			return true;
		}


	}


}
