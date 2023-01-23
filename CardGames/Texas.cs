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


		public Texas Setup(string[] arguments = null, ITexasHoldEmSettings usesettings = null) {
			ITexasHoldEmSettings settings = usesettings;
			if (settings == null) settings = new TexasHoldEmSettings();

			if (!this.UI.Welcome()) { RoundsToPlay = -1; return this; }

			string result = ProcessArguments(arguments, settings);
			if (result != null) {
				if (result == "?") this.UI.ShowHelp();
				else this.UI.ShowMsg($"Invalid argument {result}");
				return this;
			}

			this.SetupAI(settings);
			this.SetupFactory(settings);
			this.SetupGame(settings);
			this.SetupPlayers(settings);
			this.SetupStatistics(settings);
			this.SetupUI(settings);

			return this;
		}



		public void Run()
		{
			int RoundsPlayed = 0;

			while (true)
			{
				if (RoundsPlayed >= this.RoundsToPlay) break;

				// Allow here to  opt in or out new players after each round
				// Run method always return true unless there is a problem to continue

				if (game.PlayerCount < 2) continue; 

				game.PlayGame();
				RoundsPlayed++;
			}
			if (this.RoundsToPlay >= 0) {
				this.AI.Save();
				this.ShowStatistics();
				this.UI.Finish();
			}
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
					else if (str.BeginWith("-l")) { settings.LearnAi = true; settings.UseDb = true; }
					else if (str.BeginWith("r")) settings.RoundsToPlay = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("s")) settings.TableSeats = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("p")) settings.Players = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("t")) settings.Tokens = str.FilterKeep(filter).ToInt32();
					else return arg;
				}
			}

			return null;
		}

		void SetupFactory(ITexasHoldEmSettings settings) {
			Factory.Setup(this.UI, this.DB, this.AI, settings);
		}

		void SetupAI(ITexasHoldEmSettings settings)
		{
			if (settings.UseDb && this.DB != null) this.AI = new TexasHoldEmAi(this.DB.AiDb);
			else this.AI = new TexasHoldEmAi();

			if (settings.LearnAi)
			{
				this.AI.Learn(settings.RoundsToPlay, settings.Players, new TexasRankOn5Cards());
				settings.RoundsToPlay = 0;
			}
			else
			{
				this.AI.Learn(8000, settings.Players, new TexasRankOn5Cards());
			}

		}

		void SetupGame(ITexasHoldEmSettings settings)
		{

			this.game = Factory.TexasTable();
			this.RoundsToPlay = settings.RoundsToPlay;

			if (settings.Quiet) this.game.SleepTime = -1;
			else this.game.SleepTime = settings.SleepTime;

		}

		void SetupStatistics(ITexasHoldEmSettings settings) {

			if (settings.EnableStatistics)
			{
				if (settings.UseDb)
				{
					this.game.Statistics(new TexasHoldEmStatistics(DB, AI));
				}
				else
				{
					this.game.Statistics(new TexasHoldEmStatistics(null, AI));
				}
			}

		}


		void SetupPlayers(ITexasHoldEmSettings settings) {

			int count = 0;
			while (++count < settings.Players)
			{

				if (count == 5) this.game.Join(Factory.TexasHumanPlayer(name: "Human", settings));
				else if (count == 6) this.game.Join(Factory.TexasCallRobotPlayer(name: $"CallRobot", settings));
				else if (count == 7) this.game.Join(Factory.TexasRaiseRobotPlayer(name: $"RaiseRobot", settings));
				else if (count == 8) this.game.Join(Factory.TexasRandomRobotPlayer(name: $"RndRobot", settings));
				else if (count == 9) this.game.Join(Factory.TexasBalancedRobotPlayer(name: $"Robot {count}", settings));
				else this.game.Join(Factory.TexasAIPlayer(name: $"Ai {count}", settings));

			}

			if (count <=5) this.game.Join(Factory.TexasHumanPlayer(name: "Human", settings));
			else this.game.Join(Factory.TexasAIPlayer(name: $"Ai {count}", settings));
		}


		void SetupUI(ITexasHoldEmSettings settings) {

			UI.ShowMsg($"Playing {settings.RoundsToPlay} rounds with {settings.Players} players having {settings.Tokens} tokens each at table with {settings.TableSeats} seats ");
			UI.SupressOutput = settings.Quiet;
			UI.SupressOverrideRoundSummary = settings.QuietNotSummary;
			UI.SupressOverrideStatistics = settings.QuietNotStatistics;

		}



		void ShowStatistics() {
			this.UI.ShowGameStatistics(this.game.GetStatistics() as TexasHoldEmStatistics);
			this.UI.ShowGamePlayerStatistics(this.game);
		}

		int RoundsToPlay;
		TexasHoldEmAi AI;
		ITexasHoldEmIO UI;
		TexasHoldEmTable game;
		readonly ITexasDb DB;

	}
}
