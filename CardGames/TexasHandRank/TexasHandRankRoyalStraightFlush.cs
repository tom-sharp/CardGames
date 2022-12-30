using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankRoyalStraightFlush : PlayCardHandRankRoyalStraightFlush, ITexasHandRank
	{
		public TexasHandRankRoyalStraightFlush(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.RoyalStraightFlush;
		}

		public TexasHoldEmHand Id { get; }

	}

}
