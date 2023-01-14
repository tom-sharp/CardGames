using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankFullHouse : PlayCardsRankSignature
	{
		public TexasRankFullHouse(ulong signature) : base(signature)
		{
			this.rankname = "Full house";
		}

	}

}
