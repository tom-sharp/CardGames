using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankFlush : PlayCardsSignature
	{
		public TexasRankFlush(ulong rank, IPlayCardsRank ranksource) : base(rank, ranksource)
		{
			this.rankname = "Flush";
		}

	}

}
