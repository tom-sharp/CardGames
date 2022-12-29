using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankNothing : PlayCardHandRankPair, ITexasHandRank
	{
		public TexasHandRankNothing() : base(0)
		{
			Id = TexasHoldEmHand.Nothing;
		}

		public TexasHoldEmHand Id { get; }

	}

}
