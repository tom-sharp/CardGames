using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankPair : PlayCardHandRankPair, ITexasHandRank
	{
		public TexasHandRankPair(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.Pair;
		}

		public TexasHoldEmHand Id { get; }

	}

}
