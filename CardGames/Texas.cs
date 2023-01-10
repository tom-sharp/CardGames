using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games;
using Syslib.Games.Card;

namespace CardGames
{
	public class Texas
	{
		public Texas(ITexasHoldEmIO UI)
		{
			this.IO = UI;
			this.settings = new TexasSettings();
			this.setup = new TexasSetup(UI);
		}


		public void Run(string[] args) {

			// Get configuration
			string result = settings.ProcessArguments(args);
			if (result.Length > 0) { 
				if (result == "?") this.IO.ShowHelp();
				else this.IO.ShowProgressMessage($"Invalid argument {result}");
				return;
			}

			// Set up game based on  configuration
			var table = setup.TexasTable(settings);

			// Run Game
			while (true) {

				if (settings.RoundsToPlay > 0 && table.GetStatistics().GamesPlayed >= settings.RoundsToPlay) break;

				// Allow here to  opt in or out new players after each round
				// Run method always return true unless there is a problem to continue

				if (table.PlayerCount < 2) break;

				table.PlayGame();

			}

			ShowStatistics(table);
			this.IO.Finish();
		}




		private void ShowStatistics(ICardTable table) {
			this.IO.ShowGameStatistics(table.GetStatistics() as TexasHoldEmStatistics);
			this.IO.ShowGamePlayerStatistics(table);
		}


		ITexasHoldEmIO IO;
		TexasSettings settings;
		TexasSetup setup;
	}
}
