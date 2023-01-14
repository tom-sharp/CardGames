using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasRankThreeOfAKind : PlayCardsRankSignature
	{
		public TexasRankThreeOfAKind(ulong signature) : base(signature)
		{
			this.rankname = "Three of a kind";
		}

	}

}
