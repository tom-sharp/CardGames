using Syslib.Games.Card;

namespace Games.Card.TexasHoldEm
{
	class TexasHoldEmTable : CardGameTable
	{
		public TexasHoldEmTable(CardGameTableConfig tableConfig, ITexasHoldEmIO inout) : base(tableConfig)
		{
			this.carddealer = new TexasHoldEmDealer(this, inout);
		}



	}
}
