using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankThreeOfAKind : PlayCardHandRankPair, ITexasHandRank
	{
		public TexasHandRankThreeOfAKind(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.ThreeOfAKind;
		}

		public TexasHoldEmHand Id { get; }

	}

}
