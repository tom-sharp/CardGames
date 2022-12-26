using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
