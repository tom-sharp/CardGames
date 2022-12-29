using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankStraightFlush : PlayCardHandRankFlush, ITexasHandRank
	{
		public TexasHandRankStraightFlush(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.StraightFlush;
		}

		public TexasHoldEmHand Id { get; }

	}

}
