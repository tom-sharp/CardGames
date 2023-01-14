using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankPair : PlayCardsRankSignature
	{
		public TexasRankPair(ulong signature) : base(signature)
		{
			this.rankname = "Pair";
		}

	}

}
