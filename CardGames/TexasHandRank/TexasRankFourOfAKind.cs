using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankFourOfAKind : PlayCardsRankSignature
	{
		public TexasRankFourOfAKind(ulong signature) : base(signature)
		{
			this.rankname = "Four of a kind";
		}

	}

}
