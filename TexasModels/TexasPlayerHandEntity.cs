using System.ComponentModel.DataAnnotations;

namespace Games.Card.TexasHoldEm.Models
{
	public class TexasPlayerHandEntity
	{
		public int Id { get; set; }

		public bool WinRound { get; set; }

		public byte Card1Signature { get; set; }
		public byte Card2Signature { get; set; }

		public byte Card2RankId { get; set; }
		public byte Card5RankId { get; set; }
		public byte Card6RankId { get; set; }

		public byte HandRankId { get; set; }

		[StringLength(30)]
		public string HandRankName { get; set; }


		public int PlayRoundId { get; set; }

		public TexasPlayRoundEntity PlayRound { get; set; }


	}
}
