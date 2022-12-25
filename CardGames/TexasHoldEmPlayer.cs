using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayer : CardGamePlayer, ICardGamePlayer
	{
		public TexasHoldEmPlayer(CardGameTable gametable, ITexasHoldEmIO inout)
		{
			this.gametable = gametable;
			this.IO = inout;
		}



		public void Reset()
		{
		}


		public void Run()
		{

		}

		// return true if accept or raise bet or false if fold
		public bool AskBet(CardGameTableSeat tableseat, int tokens) {
			tableseat.PlaceBet(tokens);
			return true; 
		}


		ITexasHoldEmIO IO;
		CardGameTable gametable = null;

	}
}
