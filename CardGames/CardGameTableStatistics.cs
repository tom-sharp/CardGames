using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class CardGameTableStatistics : ICardGameTableStatistics
	{
		public CardGameTableStatistics()
		{
			this.RoundsPlayed = 0;
		}


		public int RoundsPlayed { get; set; }

	}
}
