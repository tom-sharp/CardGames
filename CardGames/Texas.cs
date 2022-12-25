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
			this.playerlist = new CList<CardPlayer>();
			this.IO = new TexasHoldEmConIO();
			this.texastable = null;

			this.roundstoplay = 1;
			this.tableseats = 8;
			this.players = 5;
			this.tokens = 1000;
			this.statistics = false;
			this.quiet = false;
			this.quietnotroundsummary = false;
			this.quietnotstatistics = false;

		}

		public void Run(string[] args) {

			if (!SetUp(args)) { this.IO.ShowHelp(); return; }

			texastable.Run(roundstoplay);

			ShowStatistics();

		}

		private bool SetUp(string[] args) {
			if ((args != null) && (args.Length > 0))
			{
				var str = new CStr();
				var filter = new CStr("0123456789");
				foreach (var arg in args)
				{
					str.Str(arg).ToLower();
					if (str.BeginWith("?")) { return false; }
					else if (str.BeginWith("-s")) { statistics = true; quietnotstatistics = true; }
					else if (str.BeginWith("-qr")) { quiet = true; quietnotroundsummary = true; }
					else if (str.BeginWith("-q")) quiet = true;
					else if (str.BeginWith("r")) roundstoplay = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("s")) tableseats = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("p")) players = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("t")) tokens = str.FilterKeep(filter).ToInt32();
					else { IO.ShowProgressMessage($"Invalid argument {arg}"); return false; }
				}
			}

			this.IO.ShowProgressMessage($"Playing {roundstoplay} rounds with {players} players having {tokens} tokens each at table with {tableseats} seats ");
			this.IO.SupressOutput = this.quiet;
			this.IO.SupressOverrideRoundSummary = this.quietnotroundsummary;
			this.IO.SupressOverrideStatistics = this.quietnotstatistics;

			this.texastable = new TexasHoldEmTable(new CardGameTableConfig() { Seats = tableseats }, this.IO);
			if (statistics) texastable.Statistics(new TexasHoldEmStatistics(this.IO));

			int count = 0;
			while (++count <= players) playerlist.Add(new CardPlayer(new CardPlayerProfileRobot(), name: $"Player{count}", new TokenWallet(tokens: tokens)));
			foreach (var p in playerlist) { if (!p.JoinTable(texastable)) break; }

			if (playerlist.Count() == 0) return false;

			return true;
		}



		private void ShowStatistics() {
			this.IO.ShowGameStatistics(texastable.GetStatistics() as TexasHoldEmStatistics);
			this.IO.ShowGamePlayerStatistics(this.playerlist);
		}


		int roundstoplay;
		int tableseats;
		int players;
		int tokens;
		bool statistics;
		bool quiet;
		bool quietnotroundsummary;
		bool quietnotstatistics;
		ITexasHoldEmIO IO;
		CList<CardPlayer> playerlist;
		TexasHoldEmTable texastable;


	}
}
