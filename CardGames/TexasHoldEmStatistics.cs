using Syslib;
using Syslib.Games;
using Syslib.Games.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmStatistics : GameStatistics
	{
		public TexasHoldEmStatistics()
		{
			statsWinners = new int[(int)TexasHoldEmHand.RoyalStraightFlush + 1];
			statsHands = new int[(int)TexasHoldEmHand.RoyalStraightFlush + 1];
			allhands = new CList<ITexasHandRank>();
			winnerhands = new CList<ITexasHandRank>();
		}

		public void StatsAddWinner(ITexasHandRank hand)
		{
			if (hand == null) return;
			this.winnerhands.Add(hand);
			statsWinners[(int)hand.Id]++;
		}

		public void StatsAddHand(ITexasHandRank hand)
		{
			if (hand == null) return;
			this.allhands.Add(hand);
			statsHands[(int)hand.Id]++;
		}


		public int[] StatsWinnerHands { get { return this.statsWinners; } }
		public int[] StatsHands { get { return this.statsHands; } }


		public CList<ITexasHandRank> WinnerHands { get { return this.winnerhands; } }
		public CList<ITexasHandRank> AllHands { get { return this.allhands; } }

		int[] statsWinners;
		int[] statsHands;
		CList<ITexasHandRank> allhands;
		CList<ITexasHandRank> winnerhands;

	}
}
