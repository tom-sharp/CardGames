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
		public void Reset()
		{
		}

		public bool TakePrivateCard(Card card)
		{
			return true;
		}

		public bool TakePublicCard(Card card)
		{
			return true;
		}

		public void Run()
		{

		}




	}
}
