using Syslib.Games.Card;


namespace Games.Card.TexasHoldEm
{
	class TexasHandRankHighCard : PlayCardHandRankHighCard, ITexasHandRank
	{
		public TexasHandRankHighCard(ulong rank) : base(rank)
		{
			Id = TexasHoldEmHand.HighCard;
		}

		public TexasHoldEmHand Id { get; }

	}

}
