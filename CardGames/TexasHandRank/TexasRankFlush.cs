using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankFlush : PlayCardsRankSignature
	{
		public TexasRankFlush(ulong signature) : base(signature)
		{
			this.rankname = "Flush";
		}

	}

}
