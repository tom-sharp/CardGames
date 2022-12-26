using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public interface ICardGamePlayer
	{

		/// <summary>
		/// Mandatory token Bet. for player at seat
		/// </summary>
		void PlaceBet(ICardGameTableSeat seat, int tokens);

		/// <summary>
		/// Requested minimum tokens to bet
		/// </summary>
		bool AskBet(ICardGameTableSeat seat, int tokens);


	}
}
