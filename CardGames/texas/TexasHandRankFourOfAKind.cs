using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankFourOfAKind : PlayCardHandRankFourOfAKind, ITexasHandRank
	{
		public TexasHandRankFourOfAKind(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.FourOfAKind;
		}

		public TexasHoldEmHand Id { get; }

	}

}
