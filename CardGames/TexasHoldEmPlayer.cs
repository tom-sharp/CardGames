using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class TexasHoldEmPlayer : ICardPlayer
	{
		public TexasHoldEmPlayer(string name = null, int tokens = 0)
		{
			if (name == null) Name = "";
			else Name = name;
			if (tokens > 0) Tokens = tokens;
			else Tokens = 0;
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

		public string Name { get; set; }
		public int Tokens { get; set; }

	}
}
