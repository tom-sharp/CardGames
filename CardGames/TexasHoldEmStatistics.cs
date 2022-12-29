using Syslib;
using Syslib.Games.Card;
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

		public void StatsAddWinner(ITexasHandRank hand)
		{
			if (hand == null) return;
			statsWinners[(int)hand.Id]++;
		}

		public void StatsAddHand(ITexasHandRank hand)
		{
			if (hand == null) return;
			statsHands[(int)hand.Id]++;
		}


		public int[] StatsWinnerHands { get { return this.statsWinners; } }
		public int[] StatsHands { get { return this.statsHands; } }


		int[] statsWinners;
		int[] statsHands;
		ITexasHoldEmIO IO;
	}
}
