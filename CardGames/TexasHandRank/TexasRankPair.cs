using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankPair : PlayCardsSignature
	{
		public TexasRankPair(ulong rank, IPlayCardsRank ranksource) : base(rank, ranksource)
		{
			this.rankname = "Pair";
		}

	}

}
