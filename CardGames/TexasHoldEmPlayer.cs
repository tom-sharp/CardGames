using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class TexasHoldEmPlayer : CardGamePlayer, ICardGamePlayer
	{
		public TexasHoldEmPlayer(CardGameTable gametable)
		{
			this.gametable = gametable;
		}



		public void Reset()
		{
		}


		public void Run()
		{

		}







		CardGameTable gametable = null;

	}
}
