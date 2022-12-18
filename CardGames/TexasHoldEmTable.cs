using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	class TexasHoldEmTable : CardGameTable
	{
		public TexasHoldEmTable(int tableseats) : base(tableseats)
		{
			this.carddealer = new TexasHoldEmDealer(this);
		}
	}
}
