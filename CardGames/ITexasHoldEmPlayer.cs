using Syslib.Games;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;

namespace Games.Card.TexasHoldEm
{
	internal interface ITexasHoldEmPlayer : IGamePlayer
	{


		/// <summary>
		/// Required Tokens to bet
		/// </summary>
		void PlaceBet(ITexasHoldEmPlayerTurnInfo info);


		/// <summary>
		/// Requested minimum tokens to bet
		/// </summary>
		void AskBet(ITexasHoldEmPlayerTurnInfo info);

		int BetRaiseCounter { get; set; }

		PlayerAction Action { get; set; }
	}
}
