using Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Data;
using Syslib;
using Syslib.Events;
using Syslib.Games;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;
using System;

namespace CardGames
{
	public class Texas : Progress<ProgressEventArgs>
	{
		public Texas(ITexasHoldEmUI ui, TexasDbContext dbcontext)
		{
			this.DB = new TexasDb(dbcontext);
			this.UI = ui;
			this.AI = null;
			this.game = null;
			this.SetupOK = true;
			this.settings = null;
		}

		public void SetUpProgress(object sender, ProgressEventArgs e) {
			this.UI.ShowProgress(e.Progress, e.Complete);
		}

		public Texas Setup(string[] arguments = null, ITexasHoldEmSettings usesettings = null) {


			settings = usesettings;
			if (settings == null) settings = new TexasHoldEmSettings();

			if (!(this.SetupOK = this.UI.Welcome())) return this;

			string result = ProcessArguments(arguments);
			if (result != null) {
				if (result == "?") this.UI.ShowHelp();
				else this.UI.ShowMsg($"Invalid argument {result}");
				this.SetupOK = false;
				return this;
			}

			this.SetupDB();
			this.SetupAI();
			this.SetupFactory();
			this.SetupGame();
			this.SetupStatistics();
			this.SetupUI();

			return this;
		}



		public void Run()
		{
			if (this.SetupOK) game.PlayGame();
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
					if (str.BeginWith("?") || str.BeginWith("-?")) { return "?"; }
					else if (str.IsEqual("-s")) { settings.EnableStatistics = true; settings.QuietNotStatistics = true; }
					else if (str.IsEqual("-createdb")) settings.CreateDb = true;
					else if (str.IsEqual("-dropdb")) settings.DropDb = true;
					else if (str.IsEqual("-qr")) { settings.Quiet = true; settings.QuietNotSummary = true; }
					else if (str.IsEqual("-q")) { settings.Quiet = true; }
					else if (str.IsEqual("-l")) { settings.LearnAi = true; settings.Quiet = true; }
					else if (str.BeginWith("r")) settings.RoundsToPlay = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("s")) settings.Seats = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("p")) settings.Players = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("t")) settings.Tokens = str.FilterKeep(filter).ToInt32();
					else return arg;
				}
			}

			return null;
		}

		void SetupFactory() 
		{
			Factory.Setup(this.UI, this.DB, this.AI, settings);
		}

		void SetupDB() 
		{
			if (this.settings.DropDb) {	this.DB.DeleteDb();	UI.ShowMsg("Db deleted"); }
			if (this.settings.CreateDb) { this.DB.MigrateDb(); UI.ShowMsg("Db migrated"); }
			if (!this.DB.ConnectDb()) UI.ShowErrMsg("Warning: Can't connect to db");
		}

		void SetupAI()
		{
			int players = settings.Players;

			if (this.DB != null) this.AI = new TexasHoldEmAi(this.DB.AiDb);
			else this.AI = new TexasHoldEmAi();

			this.AI.OnProgress += this.SetUpProgress;

			if (settings.LearnAi)
			{
				UI.ShowMsg($"Train Ai {settings.RoundsToPlay} rounds with {players} players");
				this.AI.Learn(settings.RoundsToPlay, players);
				if (this.AI.DbError) UI.ShowErrMsg("Error saving entries to db");
				settings.RoundsToPlay = -1;
			}
			else
			{
				// fallback on db error
				if (!this.DB.AiDb.CanConnect()) {
					if (settings.Seats < players) players = settings.Seats;
					this.AI.Learn(settings.LearnAiFallback, players);
					if (this.AI.DbError) UI.ShowErrMsg("Error saving entries to db");
				}
			}
			this.AI.OnProgress -= this.SetUpProgress;

		}

		void SetupGame()
		{

			this.game = Factory.TexasTable();
			if (this.settings.Quiet) this.settings.SleepTime = -1;

		}

		void SetupStatistics() {

			//if (settings.EnableStatistics)
			//{
			//	if (settings.UseDb)
			//	{
			//		this.game.Statistics(new TexasHoldEmStatistics(DB, AI));
			//	}
			//	else
			//	{
			//		this.game.Statistics(new TexasHoldEmStatistics(null, AI));
			//	}
			//}

		}


		void SetupUI() {
			UI.SupressOutput = settings.Quiet;
			UI.SupressOverrideRoundSummary = settings.QuietNotSummary;
			UI.SupressOverrideStatistics = settings.QuietNotStatistics;

		}




		//void ShowStatistics() {
		//	this.UI.ShowGameStatistics(this.game.GetStatistics() as TexasHoldEmStatistics);
		//	this.UI.ShowGamePlayerStatistics(this.game);
		//}

		bool SetupOK;
		TexasHoldEmAi AI;
		ITexasHoldEmUI UI;
		TexasHoldEmTable game;
		readonly ITexasDb DB;
		ITexasHoldEmSettings settings;

	}
}
