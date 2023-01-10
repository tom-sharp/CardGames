using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankTwoPair : PlayCardsSignature
	{
		public TexasRankTwoPair(ulong rank, IPlayCardsRank ranksource) : base(rank, ranksource)
		{
			this.rankname = "Two pair";
		}

	}

}
