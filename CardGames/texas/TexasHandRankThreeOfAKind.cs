using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankThreeOfAKind : PlayCardHandRankThreeOfAKind, ITexasHandRank
	{
		public TexasHandRankThreeOfAKind(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.ThreeOfAKind;
		}

		public TexasHoldEmHand Id { get; }

	}

}
