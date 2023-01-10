using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankThreeOfAKind : PlayCardsSignature
	{
		public TexasRankThreeOfAKind(ulong rank, IPlayCardsRank ranksource) : base(rank, ranksource)
		{
			this.rankname = "Three of a kind";
		}

	}

}
