using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public interface ICardPlayer
	{

		public void Reset();

		public bool TakePrivateCard(Card card);

		public bool TakePublicCard(Card card);

		public string Name { get; }
		public int Tokens { get; }

	}
}
