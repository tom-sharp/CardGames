using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankStraightFlush : PlayCardsRankSignature
	{
		public TexasRankStraightFlush(ulong signature) : base(signature)
		{
			this.rankname = "Straight flush";
		}

	}

}
