using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankHighCard : PlayCardsRankSignature
	{
		public TexasRankHighCard(ulong signature) : base(signature)
		{
			this.rankname = "High card";
		}

	}

}
