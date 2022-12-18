using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public abstract class CardGameDealer
	{
		public CardGameDealer(CardGameTable gametable)
		{
			this.gametable = gametable;
		}

		abstract public bool Run(CardGameTable gametable);

		public string Name { get; set; }

		public int Tokens { get; set; }

		public bool Active { get; set; }


		public int BetTokens { get; set; }

		protected CardGameTable gametable = null;

	}
}
