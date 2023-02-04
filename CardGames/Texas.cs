using Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Data;
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
			this.settings = null;
		}


		public Texas Setup(string[] arguments = null, ITexasHoldEmSettings usesettings = null) {
			settings = usesettings;
			if (settings == null) settings = new TexasHoldEmSettings();

			if (!this.UI.Welcome()) { RoundsToPlay = -1; return this; }

			string result = ProcessArguments(arguments);
			if (result != null) {
				if (result == "?") this.UI.ShowHelp();
				else this.UI.ShowMsg($"Invalid argument {result}");
				return this;
			}

			this.SetupDB();
			this.SetupAI();
			this.SetupFactory();
			this.SetupGame();
			this.SetupPlayers();
			this.SetupStatistics();
			this.SetupUI();

			return this;
		}



		public void Run()
		{
			int RoundsPlayed = 0;

			while (this.RoundsToPlay >= 0)
			{
				if (this.RoundsToPlay > 0 && RoundsPlayed >= this.RoundsToPlay) break;

				// Allow here to  opt in or out new players after each round
				// Run method always return true unless there is a problem to continue

				if (game.PlayerCount < 2) continue;

				if (!game.PlayGame()) break;
				RoundsPlayed++;

				if (this.RoundsToPlay > 0) {
					if (RoundsPlayed >= this.RoundsToPlay) {
						if (!this.UI.AskPlayAnotherRound()) break;
						this.RoundsToPlay = RoundsPlayed + 1;
					}
				}
				else if (!this.UI.AskPlayAnotherRound()) break;
			}

			this.FinishUp();
		}



		string ProcessArguments(string[] args)
		{
			if ((args != null) && (args.Length > 0))
			{
				var str = new CStr();
				var filter = new CStr("0123456789");
				foreach (var arg in args)
				{
					str.Str(arg).ToLower();
					if (str.BeginWith("?")) { return "?"; }
					else if (str.IsEqual("-s")) { settings.EnableStatistics = true; settings.QuietNotStatistics = true; }
					else if (str.IsEqual("-db")) settings.UseDb = true;
					else if (str.IsEqual("-createdb")) settings.CreateDb = true;
					else if (str.IsEqual("-dropdb")) settings.DropDb = true;
					else if (str.IsEqual("-qr")) { settings.Quiet = true; settings.QuietNotSummary = true; }
					else if (str.IsEqual("-q")) settings.Quiet = true;
					else if (str.IsEqual("-l")) { settings.LearnAi = true; settings.UseDb = true; }
					else if (str.BeginWith("r")) settings.RoundsToPlay = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("s")) settings.TableSeats = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("p")) settings.Players = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("t")) settings.Tokens = str.FilterKeep(filter).ToInt32();
					else return arg;
				}
			}

			return null;
		}

		void SetupFactory() {
			Factory.Setup(this.UI, this.DB, this.AI, settings);
		}

		void SetupDB() {
			if (this.settings.DropDb) {
				this.DB.DeleteDb();
				UI.ShowMsg("Db deleted");
			}
			if (this.settings.CreateDb) {
				this.DB.MigrateDb();
				UI.ShowMsg("Db migrated");
			}
			if (this.settings.UseDb) {
				if (!this.DB.ConnectDb()) UI.ShowErrMsg("Warning: Can't connect to db");
			}
		}

		void SetupAI()
		{
			int players = settings.Players;

			if (settings.UseDb && this.DB != null) this.AI = new TexasHoldEmAi(this.DB.AiDb);
			else this.AI = new TexasHoldEmAi();

			if (settings.LearnAi)
			{
				UI.ShowMsg($"Train Ai {settings.RoundsToPlay} rounds with {players} players");
				this.AI.Learn(settings.RoundsToPlay, players, new TexasRankOn5Cards());
				settings.RoundsToPlay = 0;
			}
			else
			{
				if (settings.TableSeats < players) players = settings.TableSeats;
				this.AI.Learn(settings.LearnAiGamePlay, players, new TexasRankOn5Cards());
			}

		}

		void SetupGame()
		{

			this.game = Factory.TexasTable();
			this.RoundsToPlay = settings.RoundsToPlay;

			if (settings.Quiet) this.game.SleepTime = -1;
			else this.game.SleepTime = settings.SleepTime;

		}

		void SetupStatistics() {

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


		void SetupPlayers() {

			int count = 0;
			while (count++ < settings.Players)
			{

				if (count == 1) this.game.Join(Factory.TexasHumanPlayer(name: "Human", settings));
				else if (count == 7) this.game.Join(Factory.TexasBalancedRobotPlayer(name: $"Robot {count}", settings));
				else if (count == 8) this.game.Join(Factory.TexasBalancedRobotPlayer(name: $"Robot {count}", settings));
				else if (count == 9) this.game.Join(Factory.TexasBalancedRobotPlayer(name: $"Robot {count}", settings));
				else if (count == 10) this.game.Join(Factory.TexasBalancedRobotPlayer(name: $"Robot {count}", settings));
				else this.game.Join(Factory.TexasAIPlayer(name: $"Ai {count}", settings));

			}
		}


		void SetupUI() {

			UI.ShowMsg($"Playing {settings.RoundsToPlay} rounds with {settings.Players} players having {settings.Tokens} tokens each at table with {settings.TableSeats} seats ");
			UI.SupressOutput = settings.Quiet;
			UI.SupressOverrideRoundSummary = settings.QuietNotSummary;
			UI.SupressOverrideStatistics = settings.QuietNotStatistics;

		}

		void FinishUp()
		{
			if (this.RoundsToPlay >= 0)
			{
				if (settings.UseDb && (this.AI.Save() < 0))
				{
					UI.ShowErrMsg($"Warning: Failed to save AI entries to db");
				}
				if (settings.EnableStatistics) 
				{
					this.ShowStatistics();
				}
				this.UI.Finish();
			}
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
		ITexasHoldEmSettings settings;

	}
}
