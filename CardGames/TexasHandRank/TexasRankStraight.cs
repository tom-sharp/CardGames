using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankStraight : PlayCardsSignature
	{
		public TexasRankStraight(ulong rank, IPlayCardsRank ranksource) : base(rank, ranksource)
		{
			this.rankname = "straight";
		}

	}

}
