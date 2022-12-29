using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankFlush : PlayCardHandRankFlush, ITexasHandRank
	{
		public TexasHandRankFlush(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.Flush;
		}

		public TexasHoldEmHand Id { get; }

	}

}
