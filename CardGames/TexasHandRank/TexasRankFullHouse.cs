using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankFullHouse : PlayCardsSignature
	{
		public TexasRankFullHouse(ulong rank, IPlayCardsRank ranksource) : base(rank, ranksource)
		{
			this.rankname = "Full house";
		}

	}

}
