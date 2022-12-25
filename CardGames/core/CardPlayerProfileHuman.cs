using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	class CardPlayerProfileHuman : CardPlayerProfile
	{
		public CardPlayerProfileHuman():base(new CardPlayerType("Human", 0))
		{
		}
	}
}
