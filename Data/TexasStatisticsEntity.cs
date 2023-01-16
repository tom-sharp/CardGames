using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasStatisticsEntity
	{
		public TexasStatisticsEntity()
		{
			CardsSignature = new byte[8];
		}
		public int Id { get; set; }


		public bool Win { get; set; }
		public byte Players { get; set; }

		public byte RankId { get; set; }
		public byte RankId2Cards { get; set; }
		public byte RankId5Cards { get; set; }
		public byte RankId6Cards { get; set; }
		public byte RankIdCommonCards { get; set; }
		
		public byte[] CardsSignature { get; set; }

	}
}
