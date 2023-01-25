using Syslib.Games;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public int BetRaiseCounter { get; set; }
	}
}
