using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Card;
using Games.Card.TexasHoldEm;
using Syslib;

namespace CardGames
{
	public class Texas
	{
		public void Run(string[] args) {

			SetArgs(args);
			Console.WriteLine($"Playing {roundstoplay} rounds with {players} players having {tokens} tokes each at table with {tableseats} seats ");

			var texastable = new TexasHoldEmTable(tableseats: tableseats);
			var Players = new CList<CardPlayer>();
			int count = 0;
			while (++count <= players) Players.Add(new CardPlayer(new CardPlayerProfileRobot(),name: $"Player{count}", tokens: tokens));
			foreach (var p in Players) { if (!p.JoinTable(texastable)) break; }

			
			texastable.EnableStats().Run(roundstoplay);

			var stats = texastable.GetStatistics() as TexasHoldEmStats;
			if (stats != null) {
				Console.WriteLine("\nStatistics:                        Winner              Hand");
				int TotalHands = 0, TotalWin = 0;
				double winpct, handpct;
				for (count = 0; count < stats.StatsHands.Length; count++) { TotalHands += stats.StatsHands[count]; TotalWin += stats.StatsWinnerHands[count]; }
				for (count = 0; count < stats.StatsHands.Length; count++)
				{
					winpct = 100 * stats.StatsWinnerHands[count] / TotalWin; handpct = 100 * stats.StatsHands[count] / TotalHands;
					Console.WriteLine($"{count,2}.  {(TexasHoldEmHand)count,-20}   {stats.StatsWinnerHands[count],10}  {winpct,5}%  {stats.StatsHands[count],10}  {handpct,5}%");
				}
				Console.WriteLine($"Total Rounds played {stats.RoundsPlayed}");
			}


		}

		private void SetArgs(string[] args) {
			if ((args != null) && (args.Length > 0))
			{
				var str = new CStr();
				var filter = new CStr("0123456789");
				foreach (var arg in args)
				{
					str.Str(arg);
					if (str.BeginWith("h")) { Help(); return; }
					else if (str.BeginWith("r")) roundstoplay = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("s")) tableseats = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("p")) players = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("t")) tokens = str.FilterKeep(filter).ToInt32();
					else { Console.WriteLine($"Invalid argument {arg}"); return; }
				}
			}

		}

		private void Help() {
			Console.WriteLine("Arguments:");
			Console.WriteLine("r = rounds to play     s = number of table seats    p = number of players");
			Console.WriteLine("t = player tokens      h = help");
			Console.WriteLine("ex: r10 p5");
			Console.WriteLine("ex: r10 p5 s8 t1000");
		}


		int roundstoplay = 1;
		int tableseats = 8;
		int players = 5;
		int tokens = 1000;

	}
}
