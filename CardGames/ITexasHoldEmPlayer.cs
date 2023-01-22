using Syslib.Games;
using Syslib.Games.Card;
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
		void PlaceBet(int tokens, ICardTable table);


		/// <summary>
		/// Requested minimum tokens to bet
		/// </summary>
		void AskBet(int tokens, ICardTable table);

		public int BetRaiseCounter { get; }
	}
}
