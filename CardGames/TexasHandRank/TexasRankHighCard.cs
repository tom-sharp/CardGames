using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankHighCard : PlayCardsSignature
	{
		public TexasRankHighCard(ulong rank, IPlayCardsRank ranksource) : base(rank, ranksource)
		{
			this.rankname = "High card";
		}

	}

}
