using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasStatisticsEntity
	{
		public TexasStatisticsEntity()
		{
		}
		public int Id { get; set; }


		public bool Win { get; set; }
		public byte Players { get; set; }

		public byte RankId { get; set; }

		[StringLength(30)]
		public string RankName { get; set; }
		[StringLength(30)]
		public string RankNamePrivate { get; set; }
		[StringLength(30)]
		public string RankNameCommon { get; set; }

		public byte RankIdPrivateCards { get; set; }
		public byte RankIdCommonCards { get; set; }

		public byte PrivateCard1 { get; set; }
		public byte PrivateCard2 { get; set; }


		public byte CommonCard1 { get; set; }
		public byte CommonCard2 { get; set; }
		public byte CommonCard3 { get; set; }
		public byte CommonCard4 { get; set; }
		public byte CommonCard5 { get; set; }

	}
}
