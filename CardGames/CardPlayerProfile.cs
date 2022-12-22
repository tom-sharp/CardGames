using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class CardPlayerProfile : ICardPlayerProfile
	{
		public CardPlayerProfile(CardPlayerType cardPlayerType)
		{
			this.Type = cardPlayerType;
		}
		public CardPlayerType Type { get; }
	}

}
