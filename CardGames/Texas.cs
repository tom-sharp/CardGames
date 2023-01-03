using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games;
using Syslib.Games.Card;

namespace CardGames
{
	public class Texas
	{
		public Texas(ITexasHoldEmIO inout)
		{
			this.IO = inout;
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

			if (!ProcessArguments(args)) { this.IO.ShowHelp(); return; }
			if (!SetUp()) { this.IO.ShowHelp(); return; }

			while (true) {

				if (texastable.GetStatistics().GamesPlayed >= roundstoplay) break;

				texastable.PlayGame();

				// Allow here to  opt in or out new players after each round
				// Run method always return true unless there is a problem to continue
				if (texastable.PlayerCount < 2) break;

			}

			ShowStatistics();

		}

		bool ProcessArguments(string[] args) {
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
			return true;
		}

		private bool SetUp() {

			this.IO.ShowProgressMessage($"Playing {roundstoplay} rounds with {players} players having {tokens} tokens each at table with {tableseats} seats ");
			this.IO.SupressOutput = this.quiet;
			this.IO.SupressOverrideRoundSummary = this.quietnotroundsummary;
			this.IO.SupressOverrideStatistics = this.quietnotstatistics;

			this.texastable = new TexasHoldEmTable(new CardTableConfig() { Seats = tableseats }, IO);
			
			if (this.statistics) texastable.Statistics(new TexasHoldEmStatistics(this.IO));


			texastable.Join(new TexasHoldEmPlayerDealer(new CardPlayerConfig() { Tokens = tokens }, this.IO));

			int count = 0;
			while (++count <= players) {

				texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"Player{count}", Tokens = tokens }));

//				if (count == 2) playerlist.Add(new CardPlayerRobot(name: $"Player{count} rnd", tokens, new GamePlayerProfileRandom()));
////				else if (count == 3) playerlist.Add(new CardPlayerHuman(name: $"Human", new TokenWallet(tokens: tokens)));
//				else playerlist.Add(new CardPlayerRobot(name: $"Player{count}", tokens));
			
			}


			return true;
		}



		private void ShowStatistics() {
			this.IO.ShowGameStatistics(texastable.GetStatistics() as TexasHoldEmStatistics);
			this.IO.ShowGamePlayerStatistics(this.texastable);
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
		CardTable texastable;


	}
}
