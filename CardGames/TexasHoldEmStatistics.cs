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

		public void ShowGameStatistics()
		{
			this.IO.ShowMsg("\nStatistics:                  Winning Hand                Hands");
			this.TotalHands = 0; this.TotalWin = 0;
			double winpct, handpct;
			for (int count = 0; count < this.StatsHands.Length; count++) { TotalHands += this.StatsHands[count]; TotalWin += this.StatsWinnerHands[count]; }
			for (int count = 0; count < this.StatsHands.Length; count++)
			{
				winpct = 100 * this.StatsWinnerHands[count] / TotalWin; handpct = 100 * this.StatsHands[count] / TotalHands;
				this.IO.ShowMsg($"{count,2}.  {(TexasHoldEmHand)count,-20}   {winpct,5:f1} %   {this.StatsWinnerHands[count],7}           {handpct,5:f1} %   {this.StatsHands[count],7}");
			}
			this.IO.ShowMsg($"-Rounds played {this.RoundsPlayed,7}        Total:  {TotalWin,7}                     {TotalHands,7}");
		}


		public void ShowGamePlayerstatistics(CList<CardPlayer> playerlist) {
			this.IO.ShowMsg("Players:");
			foreach (var p in playerlist)
			{
				this.IO.ShowMsg($" {p.Name,-20} Tokens {p.Tokens,10}");
			}

		}



		public int[] StatsWinnerHands { get { return this.statsWinners; } }
		public int[] StatsHands { get { return this.statsHands; } }


		int[] statsWinners;
		int[] statsHands;
		int TotalHands;
		int TotalWin;
		ITexasHoldEmIO IO;
	}
}
