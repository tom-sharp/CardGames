using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class TexasHoldEmPlayer : CardGamePlayer, ICardGamePlayer
	{
		public TexasHoldEmPlayer()
		{
		}


		public bool TablePlayers(CardGameTableSeat[] players) {
			if ((this.tableplayers = players) == null) return false;
			return true;
		}

		public void Reset()
		{
		}


		public void Run()
		{

		}


		CardGameTableSeat[] tableplayers = null;

	}
}
