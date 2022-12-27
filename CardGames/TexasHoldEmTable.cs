using Syslib.Games.Card;

namespace Games.Card.TexasHoldEm
{
	class TexasHoldEmTable : CardGameTable
	{
		public TexasHoldEmTable(CardGameTableConfig tableConfig, ITexasHoldEmIO inout) : base(tableConfig)
		{
			this.carddealer = new TexasHoldEmDealer(this, inout);
		}


		public override ICardGameTable Statistics(ICardGameTableStatistics statistics)
		{
			if (statistics != null) this.statistics = statistics;
			return this;
		}





	}
}
