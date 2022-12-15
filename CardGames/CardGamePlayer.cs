using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public abstract class CardGamePlayer
	{
		public CardGamePlayer()
		{

		}


		public string Name { get; set; }

		public int Tokens { get; set; }

		public bool Active { get; set; }

		public int BetTokens { get; set; }


	}


}
