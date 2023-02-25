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
				.Add(new Syslib.SelectItem() { Id = 3, Text = "Learn Texas Ai" })
				.Add(new Syslib.SelectItem() { Id = 4, Text = "Delete Db" })
				.Add(new Syslib.SelectItem() { Id = 5, Text = "Create Db" })
				.Add(new Syslib.SelectItem() { Id = 0, Text = "Quit" });

				switch (this.ui.AskMainMenu(menu))
				{
					case 0: runmenu = false; break;
					case 1: MenuPlayTexasHoldEm6Players(); break;
					case 2:	MenuPlayTexasHoldEm8Players(); break;
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

		void MenuTrainTexasAI() 
		{
			ui.ShowMsg($"Train Ai for 6 players, 100 rounds");
			aiTexas.Learn(playrounds: 100, players: 6);
			ui.ShowMsg($"Train Ai for 8 players, 100 rounds");
			aiTexas.Learn(playrounds: 100, players: 8);
			if (aiTexas.DbError) ui.ShowErrMsg("Error saving entries to db");
		}

		void MenuDeleteTexasDb()
		{
			dbTexas.DeleteDb();
			ui.ShowMsg("Db deleted");
		}

		void MenuCreateTexasDb()
		{
			dbTexas.MigrateDb();
			ui.ShowMsg("Db created");
		}

		ITexasDb dbTexas;
		ITexasHoldEmUI uiTexas;
		ITexasHoldEmAi aiTexas;
		ITexasHoldEmSettings configTexas;

		ICardGamesMenuUI ui;
	}
}
