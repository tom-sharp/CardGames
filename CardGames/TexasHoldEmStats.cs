using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmStats : ICardGameStats
	{
		public TexasHoldEmStats()
		{
			statsWinners = new int[(int)TexasHoldEmHand.RoyalStraightFlush + 1];
			statsHands = new int[(int)TexasHoldEmHand.RoyalStraightFlush + 1];

		}

		public int RoundsPlayed { get; set; }

		public void StatsAddWinner(TexasHoldEmHand hand)
		{
			statsWinners[(int)hand]++;
		}

		public void StatsAddHand(TexasHoldEmHand hand)
		{
			statsHands[(int)hand]++;
		}

		public int[] StatsWinnerHands { get { return this.statsWinners; } }
		public int[] StatsHands { get { return this.statsHands; } }


		int[] statsWinners;
		int[] statsHands;

	}
}
