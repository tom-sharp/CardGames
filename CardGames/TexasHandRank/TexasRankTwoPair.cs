using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankTwoPair : PlayCardsRankSignature
	{
		public TexasRankTwoPair(ulong signature) : base(signature)
		{
			this.rankname = "Two pair";
		}

	}

}
