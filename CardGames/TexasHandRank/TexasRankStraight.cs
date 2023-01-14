using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankStraight : PlayCardsRankSignature
	{
		public TexasRankStraight(ulong signature) : base(signature)
		{
			this.rankname = "straight";
		}

	}

}
