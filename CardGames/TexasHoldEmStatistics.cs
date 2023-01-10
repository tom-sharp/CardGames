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
			statsWinners = new int[(int)PokerHand.RoyalStraightFlush + 1];
			statsHands = new int[(int)PokerHand.RoyalStraightFlush + 1];
			allhands = new CList<IPlayCardsSignature>();
			winnerhands = new CList<IPlayCardsSignature>();
		}

		public void StatsAddWinner(IPlayCardsSignature hand)
		{
			if (hand == null) return;
			this.winnerhands.Add(hand);
			statsWinners[hand.RankId]++;
		}

		public void StatsAddHand(IPlayCardsSignature hand)
		{
			if (hand == null) return;
			this.allhands.Add(hand);
			statsHands[hand.RankId]++;
		}


		public int[] StatsWinnerHands { get { return this.statsWinners; } }
		public int[] StatsHands { get { return this.statsHands; } }


		public CList<IPlayCardsSignature> WinnerHands { get { return this.winnerhands; } }
		public CList<IPlayCardsSignature> AllHands { get { return this.allhands; } }

		int[] statsWinners;
		int[] statsHands;
		CList<IPlayCardsSignature> allhands;
		CList<IPlayCardsSignature> winnerhands;

	}
}
