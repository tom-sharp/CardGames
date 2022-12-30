using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankNothing : PlayCardHandRankNothing, ITexasHandRank
	{
		public TexasHandRankNothing() : base()
		{
			Id = TexasHoldEmHand.Nothing;
		}

		public TexasHoldEmHand Id { get; }

	}

}
