using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankRoyalStraightFlush : PlayCardsRankSignature
	{
		public TexasRankRoyalStraightFlush(ulong signature) : base(signature)
		{
			this.rankname = "Royal straight flush";
		}

	}

}
