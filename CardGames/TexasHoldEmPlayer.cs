using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class TexasHoldEmPlayer : CardGamePlayer, ICardGamePlayer
	{
		public TexasHoldEmPlayer(string name = null, int tokens = 0)
		{
			if (name == null) Name = "Player"; else Name = name;
			if (tokens < 0) Tokens = 0; else Tokens = tokens;
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
			throw new NotImplementedException();
		}

	}
}
