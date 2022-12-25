using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmStatistics : CardGameTableStatistics
	{
		public TexasHoldEmStatistics(ITexasHoldEmIO inout)
		{
			statsWinners = new int[(int)TexasHoldEmHand.RoyalStraightFlush + 1];
			statsHands = new int[(int)TexasHoldEmHand.RoyalStraightFlush + 1];
			IO = inout;
		}

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
		ITexasHoldEmIO IO;
	}
}
