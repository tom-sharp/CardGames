using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Games.Card.TexasHoldEm.Models
{
	public class TexasPlayRoundEntity
	{
		public int Id { get; set; }
		public byte Card1Signature { get; set; }
		public byte Card2Signature { get; set; }
		public byte Card3Signature { get; set; }
		public byte Card4Signature { get; set; }
		public byte Card5Signature { get; set; }
		public byte Card3RankId { get; set; }
		public byte Card4RankId { get; set; }
		public byte Card5RankId { get; set; }
		public byte WinRankId { get; set; }

		[StringLength(30)]
		public string Card5RankName { get; set; }

		public byte Players { get; set; }

		public ICollection<TexasPlayerHandEntity> PlayerHands { get; set; } 

	}
}
