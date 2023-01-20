using Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Models;
using Syslib;
using Syslib.Games;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;

namespace CardGames
{
	public class Texas
	{
		public Texas(ITexasHoldEmIO ui, TexasDbContext dbcontext)
		{
			this.DB = new TexasDb(dbcontext);
			this.UI = ui;
			this.AI = null;
			this.game = null;
			this.RoundsToPlay = -1;
		}

		public Texas Setup(ITexasHoldEmAi AI)
		{
			this.AI = AI;
			return this;
		}

		public Texas Setup(string[] arguments = null, ITexasHoldEmSettings usesettings = null) {
			ITexasHoldEmSettings settings = usesettings;
			if (settings == null) settings = new TexasHoldEmSettings();

			string result = ProcessArguments(arguments, settings);
			if (result != null) {
				if (result == "?") this.UI.ShowHelp();
				else this.UI.ShowMsg($"Invalid argument {result}");
				return this;
			}

			// Set up game based on  configuration
			this.game = new TexasHoldEmFactory(this.UI, this.DB, this.AI).TexasTable(settings);

			this.RoundsToPlay = settings.RoundsToPlay;

			this.AI.Learn(8000, this.game.PlayerCount, new TexasRankOn5Cards());

			return this;
		}



		public void Run()
		{
			int RoundsPlayed = 0;

			// Run Game
			while (true)
			{
				if (RoundsPlayed >= this.RoundsToPlay) break;

				// Allow here to  opt in or out new players after each round
				// Run method always return true unless there is a problem to continue

				if (game.PlayerCount < 2) continue; 

				game.PlayGame();
				RoundsPlayed++;
			}

			ShowStatistics(game);
			this.UI.Finish();
		}


		string ProcessArguments(string[] args, ITexasHoldEmSettings settings)
		{
			if ((args != null) && (args.Length > 0))
			{
				var str = new CStr();
				var filter = new CStr("0123456789");
				foreach (var arg in args)
				{
					str.Str(arg).ToLower();
					if (str.BeginWith("?")) { return "?"; }
					else if (str.BeginWith("-s")) { settings.EnableStatistics = true; settings.QuietNotStatistics = true; }
					else if (str.BeginWith("-db")) settings.UseDb = true;
					else if (str.BeginWith("-qr")) { settings.Quiet = true; settings.QuietNotSummary = true; }
					else if (str.BeginWith("-q")) settings.Quiet = true;
					else if (str.BeginWith("r")) settings.RoundsToPlay = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("s")) settings.TableSeats = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("p")) settings.Players = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("t")) settings.Tokens = str.FilterKeep(filter).ToInt32();
					else return arg;
				}
			}

			return null;
		}




		void ShowStatistics(ICardTable table) {
			this.UI.ShowGameStatistics(table.GetStatistics() as TexasHoldEmStatistics);
			this.UI.ShowGamePlayerStatistics(table);
		}

		int RoundsToPlay;
		ITexasHoldEmAi AI;
		ITexasHoldEmIO UI;
		TexasHoldEmTable game;
		readonly TexasDb DB;

	}
}
