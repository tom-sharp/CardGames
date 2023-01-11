using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasStatisticsEntity
	{
		public int Id { get; set; }


		public bool Win { get; set; }
		public byte Players { get; set; }

		public byte RankId { get; set; }

		public byte PrivateCard1 { get; set; }
		public byte PrivateCard2 { get; set; }
		public byte CommonCard1 { get; set; }
		public byte CommonCard2 { get; set; }
		public byte CommonCard3 { get; set; }
		public byte CommonCard4 { get; set; }
		public byte CommonCard5 { get; set; }

	}
}
