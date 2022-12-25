using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
