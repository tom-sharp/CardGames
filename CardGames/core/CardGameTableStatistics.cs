

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
