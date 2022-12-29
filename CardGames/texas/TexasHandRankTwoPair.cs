using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankTwoPair : PlayCardHandRankPair, ITexasHandRank
	{
		public TexasHandRankTwoPair(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.TwoPair;
		}

		public TexasHoldEmHand Id { get; }

	}

}
