using Syslib.Games.Card;

namespace Games.Card.TexasHoldEm
{
	public interface ITexasHandRank : IPlayCardHandRank
	{
		
		TexasHoldEmHand Id { get; }
	}
}