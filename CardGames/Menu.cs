using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Data;
using Syslib;
using Syslib.Games.Card;
using Syslib.ConUI;
using Syslib.Games.Card.TexasHoldEm;

namespace CardGames
{
	public class Menu
	{
		public Menu(ICardGamesMenuUI ui) { this.ui = ui; }

		public void Run() 
		{
			BugCheck.DebugMode();
			if (ui.Welcome()) { SetUpMenu();  RunMainMenu(); ui.Finish(); }
		}

		void SetUpMenu() {
			dbTexas = new TexasDb(new TexasDbContext());
			uiTexas = new TexasHoldEmConUI();
			aiTexas = new TexasHoldEmAi(dbTexas.AiDb);
			configTexas = new TexasHoldEmSettings();
		}

		void RunMainMenu() {

			bool runmenu = true;
			Factory.Setup(ui: uiTexas, db: dbTexas, ai: aiTexas, settings: configTexas);

			while (runmenu)
			{
				var menu = new CList<Syslib.ISelectItem>()
				.Add(new Syslib.SelectItem() { Id = 1, Text = "Play Texas 6 players" })
				.Add(new Syslib.SelectItem() { Id = 2, Text = "Play Texas 8 players" })
				.Add(new Syslib.SelectItem() { Id = 6, Text = "Play Texas 10 players" })
				.Add(new Syslib.SelectItem() { Id = 3, Text = "Learn Texas Ai" })
				.Add(new Syslib.SelectItem() { Id = 4, Text = "Delete Db" })
				.Add(new Syslib.SelectItem() { Id = 5, Text = "Create Db" })
				.Add(new Syslib.SelectItem() { Id = 0, Text = "Quit" });

				switch (this.ui.AskMainMenu(menu))
				{
					case 0: runmenu = false; break;
					case 1: MenuPlayTexasHoldEm6Players(); break;
					case 2: MenuPlayTexasHoldEm8Players(); break;
					case 6:	MenuPlayTexasHoldEm10Players(); break;
					case 3:	MenuTrainTexasAI(); break;
					case 4:	MenuDeleteTexasDb(); break;
					case 5:	MenuCreateTexasDb(); break;
				}
			}
		}

		void MenuPlayTexasHoldEm6Players() 
		{
			configTexas.Players = 6; 
			configTexas.Seats = 8;
			configTexas.RoundsToPlay = 50; 
			configTexas.Tokens = 1000;
			Factory.TexasTable().PlayGame();
		}

		void MenuPlayTexasHoldEm8Players()
		{
			configTexas.Players = 8;
			configTexas.Seats = 8;
			configTexas.RoundsToPlay = 50;
			configTexas.Tokens = 1000;
			Factory.TexasTable().PlayGame();
		}
		void MenuPlayTexasHoldEm10Players()
		{
			configTexas.Players = 10;
			configTexas.Seats = 10;
			configTexas.RoundsToPlay = 50;
			configTexas.Tokens = 1000;
			Factory.TexasTable().PlayGame();
		}


		void MenuTrainTexasAI() 
		{
			int rounds = 1000, players = 6;
			aiTexas.OnProgress += AiTexas_OnProgress;

			while (players <= 10) 
			{
				ui.ShowMsg($"Train Ai for {players} players, {rounds} rounds");
				aiTexas.Learn(playrounds: rounds, players: players);
				players += 2;
			}

			if (aiTexas.DbError) ui.ShowErrMsg("Could not save entries to persistant db");
			aiTexas.OnProgress -= AiTexas_OnProgress;
		}

		private void AiTexas_OnProgress(object sender, Syslib.Events.ProgressEventArgs e)
		{
			this.ui.ShowProgress(e.Progress, e.Complete);
		}

		void MenuDeleteTexasDb()
		{
			ui.ShowMsg("delete...");
			if (dbTexas.DeleteDb())	ui.ShowMsg("Db deleted");
			else ui.ShowErrMsg("Failed to delete Db");
		}

		void MenuCreateTexasDb()
		{
			ui.ShowMsg("create...");
			if (dbTexas.MigrateDb()) ui.ShowMsg("Db created/migrated");
			else ui.ShowErrMsg("Failed to create Db");
		}

		ITexasDb dbTexas;
		ITexasHoldEmUI uiTexas;
		ITexasHoldEmAi aiTexas;
		ITexasHoldEmSettings configTexas;

		ICardGamesMenuUI ui;
	}
}
