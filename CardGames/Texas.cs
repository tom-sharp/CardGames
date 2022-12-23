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
		public Texas()
		{
			this.PlayerList = new CList<CardPlayer>();
			this.IO = new TexasHoldEmConIO();
			this.texastable = null;
		}
		public void Run(string[] args) {

			if (!SetUp(args)) { this.Help(); return; }

			texastable.Run(roundstoplay);

			this.IO.SupressOutput = false;

			var stats = texastable.GetStatistics() as TexasHoldEmStatistics;
			if (stats != null) {
				stats.ShowGameStatistics();
				stats.ShowGamePlayerstatistics(this.PlayerList);
			}

		}

		private bool SetUp(string[] args) {
			if ((args != null) && (args.Length > 0))
			{
				var str = new CStr();
				var filter = new CStr("0123456789");
				foreach (var arg in args)
				{
					str.Str(arg).ToLower();
					if (str.BeginWith("h")) {return false; }
					else if (str.BeginWith("-s")) statistics = true;
					else if (str.BeginWith("-q")) quiet = true;
					else if (str.BeginWith("r")) roundstoplay = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("s")) tableseats = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("p")) players = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("t")) tokens = str.FilterKeep(filter).ToInt32();
					else { IO.ShowMsg($"Invalid argument {arg}"); return false; }
				}
			}

			this.IO.ShowMsg($"Playing {roundstoplay} rounds with {players} players having {tokens} tokens each at table with {tableseats} seats ");
			this.IO.SupressOutput = this.quiet;

			this.texastable = new TexasHoldEmTable(new CardGameTableConfig() { Seats = tableseats }, this.IO);
			if (statistics) texastable.Statistics(new TexasHoldEmStatistics(this.IO));

			int count = 0;
			while (++count <= players) PlayerList.Add(new CardPlayer(new CardPlayerProfileRobot(), name: $"Player{count}", tokens: tokens));
			foreach (var p in PlayerList) { if (!p.JoinTable(texastable)) break; }

			if (PlayerList.Count() == 0) return false;

			return true;
		}


		private void ShowStatistics(TexasHoldEmStatistics stats) {
			if (stats != null)
			{
				this.IO.SupressOutput = false;
				this.IO.ShowMsg("\nStatistics:                  Winning Hand                Hands");
				int TotalHands = 0, TotalWin = 0;
				double winpct, handpct;
				for (int count = 0; count < stats.StatsHands.Length; count++) { TotalHands += stats.StatsHands[count]; TotalWin += stats.StatsWinnerHands[count]; }
				for (int count = 0; count < stats.StatsHands.Length; count++)
				{
					winpct = 100 * stats.StatsWinnerHands[count] / TotalWin; handpct = 100 * stats.StatsHands[count] / TotalHands;
					this.IO.ShowMsg($"{count,2}.  {(TexasHoldEmHand)count,-20}   {winpct,5:f1} %   {stats.StatsWinnerHands[count],7}           {handpct,5:f1} %   {stats.StatsHands[count],7}");
				}
				this.IO.ShowMsg($"-Rounds played {stats.RoundsPlayed,7}        Total:  {TotalWin,7}                     {TotalHands,7}");

				this.IO.ShowMsg("Players:");
				foreach (var p in this.PlayerList) {
					this.IO.ShowMsg($" {p.Name,-20} Tokens {p.Tokens,10}");
				}
			}

		}


		private void Help() {
			this.IO.ShowMsg("Arguments:");
			this.IO.ShowMsg("r = rounds to play     s = number of table seats    p = number of players");
			this.IO.ShowMsg("t = player tokens      -s = enable statistics       -q = quiet game output");
			this.IO.ShowMsg("h = help");
			this.IO.ShowMsg("ex: r10 p5");
			this.IO.ShowMsg("ex: r10 p5 s8 t1000 -s");
		}


		int roundstoplay = 1;
		int tableseats = 8;
		int players = 5;
		int tokens = 1000;
		bool statistics = false;
		bool quiet = false;
		ITexasHoldEmIO IO;
		CList<CardPlayer> PlayerList;
		TexasHoldEmTable texastable;


	}
}
