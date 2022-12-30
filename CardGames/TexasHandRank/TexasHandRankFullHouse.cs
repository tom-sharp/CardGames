using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankFullHouse : PlayCardHandRankFullHouse, ITexasHandRank
	{
		public TexasHandRankFullHouse(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.FullHouse;
		}

		public TexasHoldEmHand Id { get; }

	}

}
