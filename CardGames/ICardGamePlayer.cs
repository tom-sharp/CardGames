using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public interface ICardGamePlayer
	{
	
		public void Reset();

		public bool TakePrivateCard(Card card);

		public bool TakePublicCard(Card card);

		public void Run();


		public string Name { get; set;}

		public int Tokens { get; set; }

		public bool Active { get; set; }


		public int BetTokens { get; set; }

	}
}
