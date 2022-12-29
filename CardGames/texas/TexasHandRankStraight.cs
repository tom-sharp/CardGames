using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankStraight : PlayCardHandRankPair, ITexasHandRank
	{
		public TexasHandRankStraight(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.Straight;
		}

		public TexasHoldEmHand Id { get; }

	}

}
