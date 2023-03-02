using Syslib;
using Syslib.Games;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;
using Syslib.ConUI;
using System;
using Games.Card.TexasHoldEm.ConsoleUI;
using System.Threading;
using System.Collections.Generic;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmConUI : ITexasHoldEmUI
	{
		public TexasHoldEmConUI()
		{
			this.SupressOutput = false;
			this.SupressOverrideRoundSummary = false;
			this.speed = speedfast;
			this.consoletable = new TexasConsoleTable();

			this.ui = ConIO.PInstance;

			this.mainmenu = new ConMenu(45,10,22,consoletable);
			this.submenu1 = new ConMenu(2, 2, mainmenu);
			this.progressbar = new ConProgress(10, 5, 80, 3, this.consoletable);
			this.progressbar.CompleteCharacter = ConGraphics.BlockSolid;
			this.progressbar.InCompleteCharacter = ConGraphics.BlockShadeMedium;
			this.msg = new ConText(consoletable);
			this.errmsg = new ConText(consoletable);
			this.msg.Position(0,3);
			this.errmsg.Position(0, 4);
			this.SetUpColors();
		}

		public bool SupressOutput { get; set; }
		public bool SupressOverrideRoundSummary { get; set; }
		public bool SupressOverrideStatistics { get; set; }


		void Wait(int ms)
		{
			if (ms < 0) return;
			Thread.Sleep(ms);
		}

		public void Clear() {
			ui.Clear();
		}

		public bool Welcome() {

			int minWidth = 120;
			int minHeight = 30;

			if (!ui.SetConsoleMinSize(minWidth, minHeight)) {
				Console.WriteLine($"Error: unable to set console window size. Min {minWidth} x {minHeight}");
				return false;
			}

			ui.Clear();

			ui.HideCursor();
			ui.DrawBorder(0, 0, minWidth, 3);
			ui.ShowXY(20, 1, "Card Games");
			ui.ShowXY(0, 3, "");
			return true;
		}

		public void ShowHelp() {
			ui.PushColor();
			SetStdColor();
			ShowMsg("Arguments:");
			ShowMsg(" r = rounds to play     s = number of table seats    p = number of players");
			ShowMsg(" t = player tokens      ? = help");
			ShowMsg(" -q  = silent game output");
			ShowMsg(" -qs = silent game output, override for statistics");
			ShowMsg(" -qr = silent game output, override for round summary");
			ShowMsg(" -s  = show statistics");
			ShowMsg(" -db = Ai use database & save statistics to database (if enabled)");
			ShowMsg(" -createdb = create or upgrade db to latest version");
			ShowMsg(" -dropdb = delete db");
			ShowMsg(" -l  = learn Ai by training, saving to db");
			ShowMsg(" ex:  r10 p4");
			ShowMsg(" ex:  r10 p6 -db");
			ShowMsg(" ex:  r10 p6 s10 t500 -s");
			ShowMsg(" ex:  r100 p6 s6 t1500 -s -q");
			ShowMsg(" ex:  r100 p6 s6 t1500 -s -qr");
			ShowMsg(" ex:  r100 p6 s6 t1500 -s -qr -db");
			ShowMsg(" ex:  r2000 p6 -l");
			ui.PopColor();
		}



		public void DealFlop() 
		{
			if (this.SupressOutput) return;
			this.ReDrawGameTable();
			this.consoletable.CommonSeat.SeatComment.Color(HighLightColor);
			this.consoletable.CommonSeat.SeatComment.Text = "Dealing Flop"; this.Wait(speedfast);
			this.consoletable.DealPlayerCards(speedfast);
			this.consoletable.CommonSeat.SeatComment.Color(StdColor);
			this.consoletable.CommonSeat.SeatComment.Text = "Place bets";
		}

		public void DealTurn()
		{
			if (this.SupressOutput) return;
			this.ReDrawGameTable();
			this.consoletable.CommonSeat.SeatComment.Color(HighLightColor);
			this.consoletable.CommonSeat.SeatComment.Text = "Dealing Turn"; this.Wait(speedmedium);
			this.consoletable.DealCommonCards(speedfast);
			this.consoletable.CommonSeat.SeatComment.Color(StdColor);
			this.consoletable.CommonSeat.SeatComment.Text = "Place bets";
		}

		public void DealRiver()
		{
			if (this.SupressOutput) return;
			this.ReDrawGameTable();
			this.consoletable.CommonSeat.SeatComment.Color(HighLightColor);
			this.consoletable.CommonSeat.SeatComment.Text = "Dealing River"; this.Wait(speedslow);
			this.consoletable.DealCommonCards(speedfast);
			this.consoletable.CommonSeat.SeatComment.Color(StdColor);
			this.consoletable.CommonSeat.SeatComment.Text = "Place bets";
		}


		public void DealShowDown()
		{
			if (this.SupressOutput) return;
			this.ReDrawGameTable();
			this.consoletable.CommonSeat.SeatComment.Color(HighLightColor);
			this.consoletable.CommonSeat.SeatComment.Text = "Dealing Showdown"; this.Wait(speedslow);
			this.consoletable.DealCommonCards(speedfast);
			this.consoletable.CommonSeat.SeatComment.Color(StdColor);
			this.consoletable.CommonSeat.SeatComment.Text = "Place bets";
		}

		public void ReDrawGameTable()
		{
			if (this.table != null) this.consoletable.CommonSeat.TablePot = this.table.TablePot.Tokens;
			foreach (var seat in this.consoletable) seat.Update();
			this.consoletable.CommonSeat.Update();
		}

		public void RedrawConsoleTable() {
			this.consoletable.Update();
		}

		public void Finish() {
			if (!SupressOutput) ShowPlayerSummary();
			this.ui.ShowXY(0, this.consoletable.Y + this.consoletable.Height, "");
			ui.RestoreColor();
			ui.HideCursor(false);

			if (!ui.RestoreConsoleSize())
			{
				ui.Show($"Error: unable to restore console window");
			}
		}

		public void ShowNewRound(TexasHoldEmTable table) {

			if (SupressOutput) return;
			ui.HideCursor();
			SetUpTable(table);
			this.consoletable.Update();
//			ReDrawGameTable();
		}

		public void ShowPlayerSummary() {
			if (SupressOutput) return;
			this.consoletable.CommonSeat.SeatComment.Text = "Summary";
			foreach (var cseat in this.consoletable) { cseat.UpdateComment(); }
		}


		public bool ShowRoundSummary(TexasHoldEmTable table) {
			if (SupressOutput) return true;
			ReDrawGameTable();
			return AskPlayNext();
		}

		public void ShowProgress(double progress, double complete) {
			this.progressbar.ShowProgressEvent(this, new ProgressEventArgs(progress,complete));
		}

		public void ShowGameStatistics(TexasHoldEmStatistics statistics)
		{
			if ((SupressOutput) && (!SupressOverrideStatistics)) return;

			if (statistics != null) {
				int TotalHands = 0, TotalWin = 0;
				int[] WinningRank = new int[(int)PokerHand.RoyalStraightFlush + 1];
				int[] TotalRank = new int[(int)PokerHand.RoyalStraightFlush + 1];

				foreach (var entity in statistics.PlayRounds)
				{

					if (entity.WinRankId > (int)PokerHand.RoyalStraightFlush) BugCheck.Critical(this, "ShowGameStatistics - Unexpected Rank Id");
					TotalWin++;
					WinningRank[entity.WinRankId]++;
					foreach (var h in entity.PlayerHands) {
						TotalRank[h.HandRankId]++;
						TotalHands++;
					}
					statistics.SaveToDb(entity);
				}
				statistics.SaveDb();

				ShowMsg("\nStatistics:                  Winning Hand                Hands");
				double winpct, handpct, hands, handswin, hand, handwin;
				hands = TotalHands; handswin = TotalWin;
				for (int count = 0; count <= (int)PokerHand.RoyalStraightFlush; count++)
				{
					hand = 100 * TotalRank[count];
					handwin = 100 * WinningRank[count];
					winpct = handwin / handswin; handpct = hand / hands;
					ShowMsg($"{count,2}.  {(PokerHand)count,-20}   {winpct,5:f1} %   {WinningRank[count],7}           {handpct,5:f1} %   {TotalRank[count],7}");
				}
				ShowMsg($"-Rounds played {statistics.GamesPlayed,7}        Total:  {TotalWin,7}                     {TotalHands,7}");
			}
		}

		public void ShowGamePlayerStatistics(TexasHoldEmTable table)
		{
			if ((SupressOutput) && (!SupressOverrideStatistics)) return;

			if (table != null) {
				SetStdColor();
				ui.Show("Players:\n");
				foreach (var seat in table.TableSeats)
				{
					if (!seat.IsFree) {
						if (seat.Player.Type == GamePlayerType.Default)
							ui.Show($" {seat.Player.Name,-20}\n");
						else
							ui.Show($" {seat.Player.Name,-20} Tokens {seat.Player.Tokens,10}\n");
					}
				}
			}
		}


		public void ShowPlayerAction(ITexasHoldEmSeat seat) { if (SupressOutput) return; ShowPlayerSeat(seat); Wait(speed); }

		public void ShowPlayerSeat(ITexasHoldEmSeat seat) {

			if (SupressOutput) return;

			if (seat != null && seat.Player != null) {
				if (seat.Player.Type == GamePlayerType.Default) {
					this.consoletable.CommonSeat.TablePot = this.table.TablePot.Tokens;
					this.consoletable.CommonSeat.Update();
				} 
				else this.consoletable.PlayerSeat(seat)?.Update();
			}
		}


		public int AskMainMenu(IForEach<Syslib.ISelectItem> list)
		{
			if (SupressOutput) return 0;

			this.mainmenu.Clear();
			list.ForEach(o=> this.mainmenu.AddItem(new Syslib.ConUI.SelectItem() { Id = o.Id, Text = o.Text}));

			var answer = this.mainmenu.Select();
			this.mainmenu.ClearArea();
			if (answer == null) return 0;

			return answer.Id;
		}

		public bool AskPlayNext()
		{
			if (SupressOutput) return true;

			this.submenu1.Clear();
			this.submenu1.AddItem(new Syslib.ConUI.SelectItem() { Id = 1, Text = "Play next" });
			this.submenu1.AddItem(new Syslib.ConUI.SelectItem() { Id = 2, Text = "Back to menu" });

			var answer = this.submenu1.Select();
			this.submenu1.ClearArea();
			if (answer == null || answer.Id == 2) return false;

			return true;
		}


		// respond to request fold/check/call/raise
		// -1 Fold, 0 call or check, >0 raise that amount
		public int AskForBet(int requestedtokens, int canraisetokens)
		{
			if (SupressOutput) return 0;
			this.BuildMenus(requestedtokens, canraisetokens);
			int result;
			this.ui.PushColor();
			while (true) {
				var selected = this.mainmenu.Select();
				if (selected == null) { if (requestedtokens > 0) result = -1; else result = 0; break; } // esc pressed
				if (selected.Id == 1) { result = 0; break; } // check
				if (selected.Id == 2) { result = 0; break; } // call
				if (selected.Id == 3) { if (requestedtokens > 0) result = -1; else result = 0; break; } // fold
				if (selected.Id == 4) {
					var selectbetraise = this.submenu1.Select();
					if (selectbetraise != null) { result = selectbetraise.Id; submenu1.ClearArea(); break; }
					submenu1.ClearArea();
				}

			}
			this.ui.PopColor();
			this.mainmenu.ClearArea();
			return result;
		}


		public void ShowMsg(string msg)
		{
			this.msg.Text = msg;
		}

		public void ShowErrMsg(string msg)
		{
			this.errmsg.Text = msg;
		}



		void SetUpTable(TexasHoldEmTable table) {

			if (table.SeatCount <= 9) this.consoletable.SetUp(TexasConsoleTable.TableSetUp.Seats8);
			else if (table.SeatCount <= 11) this.consoletable.SetUp(TexasConsoleTable.TableSetUp.Seats10);
			else throw new ApplicationException($"Number of seats ({table.SeatCount}) is not supported in Console version");

			int count = 0;
			PlayerSeatConsole consoleseat;
			foreach (var seat in table.TableSeats)
			{
				if (seat.Player != null && seat.Player.Type == GamePlayerType.Default)
				{
					this.consoletable.CommonSeat.Seat = seat;
				}
				else
				{
					consoleseat = this.consoletable.PlayerSeat(count++);
					if (seat.IsFree) consoleseat.Seat = null; else consoleseat.Seat = seat;
				}
			}
			while ((consoleseat = this.consoletable.PlayerSeat(count++)) != null) consoleseat.Seat = null;


			this.table = table;

		}


		void BuildMenus(int requestedtokens, int canraisetokens) {

			this.mainmenu.Clear();
				
			if (requestedtokens == 0) 
				this.mainmenu.AddItem(new Syslib.ConUI.SelectItem() { Id = 1, Text = $"Check" });
			if (requestedtokens > 0) {
				this.mainmenu.AddItem(new Syslib.ConUI.SelectItem() { Id = 2, Text = $"Call {requestedtokens} tokens" });
				this.mainmenu.AddItem(new Syslib.ConUI.SelectItem() { Id = 3, Text = $"Fold" });
			}
			if (canraisetokens >= 0)
				this.mainmenu.AddItem(new Syslib.ConUI.SelectItem() { Id = 4, Text = $"Raise" });

			this.submenu1.Clear();

			this.submenu1.AddItem(new Syslib.ConUI.SelectItem() { Id = 1 * canraisetokens, Text = $"Raise {1 * canraisetokens} token" });
			this.submenu1.AddItem(new Syslib.ConUI.SelectItem() { Id = 2 * canraisetokens, Text = $"Raise {2 * canraisetokens} token" });
			this.submenu1.AddItem(new Syslib.ConUI.SelectItem() { Id = 3 * canraisetokens, Text = $"Raise {3 * canraisetokens} token" });
			this.submenu1.AddItem(new Syslib.ConUI.SelectItem() { Id = 4 * canraisetokens, Text = $"Raise {4 * canraisetokens} token" });
			this.submenu1.AddItem(new Syslib.ConUI.SelectItem() { Id = 5 * canraisetokens, Text = $"Raise {5 * canraisetokens} token" });

		}


		void SetStdColor() => ui.SetColor(ConsoleColor.Green, ConsoleColor.Black); 


		ConColor StdColor = new ConColor(ConsoleColor.Green, ConsoleColor.Black);
		ConColor InactiveColor = new ConColor(ConsoleColor.DarkGreen, ConsoleColor.Black);
		ConColor InTurnColor = new ConColor(ConsoleColor.Yellow, ConsoleColor.Black);
		ConColor ErrorColor = new ConColor(ConsoleColor.Red, ConsoleColor.Black);
		ConColor HighLightColor = new ConColor(ConsoleColor.Yellow, ConsoleColor.Blue);
		ConColor PlayCardBlackColor = new ConColor(ConsoleColor.Black, ConsoleColor.White);
		ConColor PlayCardRedColor = new ConColor(ConsoleColor.Red, ConsoleColor.White);
		ConColor PlayCardBacksideColor = new ConColor(ConsoleColor.Gray, ConsoleColor.White);
		ConColor MenuColor = new ConColor(ConsoleColor.DarkBlue, ConsoleColor.DarkGray);
		ConColor MenuSelectedColor = new ConColor(ConsoleColor.White, ConsoleColor.DarkBlue);


		void SetUpColors() {
			this.consoletable.Color(StdColor);
			this.consoletable.Border.Color(StdColor);
			this.consoletable.CommonSeat.Color(StdColor);
			this.consoletable.CommonSeat.InactiveColor.Set(InactiveColor);
			this.consoletable.CommonSeat.InTurnColor.Set(InTurnColor);
			this.consoletable.CommonSeat.HighLightColor.Set(HighLightColor);
			this.mainmenu.Color(MenuColor);
			this.mainmenu.ColorSelected.Set(MenuSelectedColor);
			this.submenu1.ColorSelected.Set(MenuSelectedColor);
			this.errmsg.Color(ErrorColor);
		}



		readonly static int speedslow = 1000;
		readonly static int speedmedium = 500;
		readonly static int speedfast = 250;

		int speed = speedslow;

		TexasConsoleTable consoletable;
		readonly ConIO ui;

		ConProgress progressbar;
		ConText msg;
		ConText errmsg;

		ConMenu mainmenu;
		ConMenu submenu1;

		TexasHoldEmTable table;

	}





}
