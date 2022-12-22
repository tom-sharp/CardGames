using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	class TexasHoldEmTable : CardGameTable
	{
		public TexasHoldEmTable(int tableseats) : base(tableseats)
		{
			this.carddealer = new TexasHoldEmDealer(this);
		}


		public override ICardGameTable EnableStats()
		{
			this.statistics = new TexasHoldEmStats();

			return this;
		}

	}
}
