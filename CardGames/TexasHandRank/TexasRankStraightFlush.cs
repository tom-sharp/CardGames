using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankStraightFlush : PlayCardsSignature
	{
		public TexasRankStraightFlush(ulong rank, IPlayCardsRank ranksource) : base(rank, ranksource)
		{
			this.rankname = "Straight flush";
		}

	}

}
