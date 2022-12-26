using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	class CardPlayerProfileRandom : CardPlayerProfile
	{
		public CardPlayerProfileRandom()
		{
			this.Defensive = 0;
			this.Offensive = 0;
			this.Randomness = 100;
			this.Name = "Random";
		}
	}
}
