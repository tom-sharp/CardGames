using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankFlush : PlayCardHandRankPair, ITexasHandRank
	{
		public TexasHandRankFlush(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.Flush;
		}

		public TexasHoldEmHand Id { get; }

	}

}
