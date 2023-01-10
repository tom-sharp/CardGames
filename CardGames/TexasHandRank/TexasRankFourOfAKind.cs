using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankFourOfAKind : PlayCardsSignature
	{
		public TexasRankFourOfAKind(ulong rank, IPlayCardsRank ranksource) : base(rank, ranksource)
		{
			this.rankname = "Four of a kind";
		}

	}

}
