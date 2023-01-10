using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankRoyalStraightFlush : PlayCardsSignature
	{
		public TexasRankRoyalStraightFlush(ulong rank, IPlayCardsRank ranksource) : base(rank, ranksource)
		{
			this.rankname = "Royal straight flush";
		}

	}

}
