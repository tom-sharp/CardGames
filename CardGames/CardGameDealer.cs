using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public abstract class CardGameDealer : ICardGameDealer
	{
		public CardGameDealer(CardGameTable gametable)
		{
			this.gametable = gametable;
		}

		abstract public bool Run();


		protected CardGameTable gametable = null;

	}
}
