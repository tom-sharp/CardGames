using Syslib;
using Syslib.Games;
using Syslib.Games.Card;
using Syslib.ConUI;
using System;
using Games.Card.TexasHoldEm.ConsoleUI;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmConUI : ITexasHoldEmIO
	{
		public TexasHoldEmConUI()
		{
			this.SupressOutput = false;
			this.SupressOverrideRoundSummary = false;

			this.consoletable = new TexasConsoleTable();
			this.msg = new CStr();
			this.SetUpColors();

			this.ui = ConIO.PInstance;

			this.betmenu = new ConMenu(45,10,20,consoletable);
			this.betraisemenu = new ConMenu(2, 2, betmenu);
			this.playroundmenu = new ConMenu(betmenu);


		}

		public bool SupressOutput { get; set; }
		public bool SupressOverrideRoundSummary { get; set; }
		public bool SupressOverrideStatistics { get; set; }

		public void Clear() {
			ui.Clear();
		}

		public bool Welcome() {

			ui.Clear();

			int minWidth = 120;
			int minHeight = 30;

			if (!ui.SetConsoleMinSize(minWidth, minHeight)) {
				Console.WriteLine($"Error: unable to set console window size. Min {minWidth} x {minHeight}");
				return false;
			}

			ui.DrawBorder(0, 0, minWidth, 3);
			ui.ShowXY(20, 1, "Texas Hold'Em");
			ui.ShowXY(0, 3, "Setup...");
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


		public void ReDrawGameTable()
		{
			foreach (var seat in this.consoletable) seat.Update();
			this.consoletable.CommonSeat.Update();
		}

		public void ShowProgressMessage(string msg) {
			if (SupressOutput) return;
			UpdateMessageLog(msg);
		}

		public void Finish() {
			if (SupressOutput) return;
			ui.RestoreColor();
			ui.HideCursor(false);

			if (!ui.RestoreConsoleSize())
			{
				ui.Show($"Error: unable to restore console window");
			}
		}

		public void ShowNewRound(ICardTable table) {

			if (SupressOutput) return;
			SetStdColor();
			ui.HideCursor();
			ui.Clear();
//			MsgLog.Clear();
			this.consoletable.Log.Clear();
			SetUpTable(table);
			ReDrawGameTable();
			UpdateMessageLog("------ NEW ROUND | Texas Hold'em  ------");
		}


		public void ShowRoundSummary(ICardTable table, bool samepage) {

			if ((SupressOutput) && (!SupressOverrideRoundSummary)) return;

			int counter = 0;
			IPlayCards playerhand;

			//if (!SupressOutput) Console.Clear();
			//SetStdColor();

			if (samepage && !SupressOutput)
			{
				ReDrawGameTable();
			}
			else {
				foreach (var seat in table.TableSeats)
				{
					if (!seat.IsFree)
					{
						msg.Clear();
						msg.Append($" Seat {counter,2}. {seat.Player.Name,-15}  ");
						if (seat.Player.Type == GamePlayerType.Default) { msg.Append(new CStr(12, 32)); }
						else msg.Append($"{seat.Player.Tokens,10}  ");
						if (seat.IsActive)
						{
							if (seat.Player.Type == GamePlayerType.Default) { msg.Append(new CStr(20, 32)); SetActiveColor(); }
							else msg.Append($"{seat.Player.Cards.Signature.Name,-20 }");
							if (seat.Player.Cards.WinHand) { msg.Append(" *WIN* "); SetActiveColor(); } else msg.Append(new CStr(7, 32));
							playerhand = seat.Player.Cards.GetCards().Sort(SortCardsFunc);
							ui.Show(msg.ToString());
							foreach (var c in playerhand) { WritePlayCard(c); }
						}
						else {
							ui.Show(msg.ToString());
						}
						ui.Show("\n");
						SetStdColor();
					}
					counter++;
				}
			}

			ui.Show("\n");
			if (!SupressOutput) {
				SetHighLightColor();
				ui.ShowXY(this.betmenu.X, this.betmenu.Y, "Press Any Key");
				ui.GetKey();
				SetStdColor();
			}
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

		public void ShowGamePlayerStatistics(ICardTable table)
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


		public void ShowPlayerSeat(ICardTableSeat seat) {

			if (seat != null && seat.Player != null) {
				if (seat.Player.Type == GamePlayerType.Default) {
					this.consoletable.CommonSeat.TablePot = this.table.TablePot.Tokens;
					this.consoletable.CommonSeat.Update();
				} 
				else this.consoletable.PlayerSeat(seat)?.Update();
			}
		}

		public bool AskPlayAnotherRound()
		{
			if (SupressOutput) return true;

			this.playroundmenu.Clear();
			this.playroundmenu.AddItem(new SelectItem() { Id = 1, Text = "Play another round" });
			this.playroundmenu.AddItem(new SelectItem() { Id = 2, Text = "Quit" });

			var answer = this.playroundmenu.Select();
			this.playroundmenu.Clear();
			if (answer == null || answer.Id == 2) return false;
			return true;
		}


		// respond to request fold/check/call/raise
		// -1 Fold, 0 call or check, >0 raise that amount
		public int AskForBet(int requestedtokens, int canraisetokens)
		{
			if (SupressOutput) return 0;
			this.BuildMenus(requestedtokens, canraisetokens);
			int result = 0;
			this.ui.PushColor();
			while (true) {
				var selected = this.betmenu.Select();
				if (selected == null) { if (requestedtokens > 0) result = -1; else result = 0; break; } // esc pressed
				if (selected.Id == 1) { result = 0; break; } // check
				if (selected.Id == 2) { result = 0; break; } // call
				if (selected.Id == 3) { if (requestedtokens > 0) result = -1; else result = 0; break; } // fold
				if (selected.Id == 4) {
					var selectbetraise = this.betraisemenu.Select();
					if (selectbetraise != null) { result = selectbetraise.Id; break; }
					this.consoletable.Log.Update();
				}

			}
			this.ui.PopColor();
			this.betmenu.ClearArea();
			return result;
		}


		public void ShowMsg(string msg)
		{
			if (msg == null) Console.WriteLine("");
			else Console.WriteLine(msg);
		}

		public void ShowErrMsg(string msg)
		{
			ui.PushColor();
			SetErrorColor();
			if (msg == null) Console.WriteLine("");
			else Console.WriteLine(msg);
			ui.PopColor();
		}




		void UpdateMessageLog(string msg) {
			this.consoletable.Log.Add(msg).Update();
		}


		void SetUpTable(ICardTable table) {

			if (table.SeatCount <= 10) this.consoletable.SetUp(TexasConsoleTable.TableSetUp.Seats10);
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

			this.betmenu.Clear();
				
			if (requestedtokens == 0) 
				this.betmenu.AddItem(new SelectItem() { Id = 1, Text = $"Check" });
			if (requestedtokens > 0) {
				this.betmenu.AddItem(new SelectItem() { Id = 2, Text = $"Call {requestedtokens} tokens" });
				this.betmenu.AddItem(new SelectItem() { Id = 3, Text = $"Fold" });
			}
			if (canraisetokens >= 0)
				this.betmenu.AddItem(new SelectItem() { Id = 4, Text = $"Raise" });

			this.betraisemenu.Clear();
			this.betraisemenu.AddItem(new SelectItem() { Id = 1 * canraisetokens, Text = $"Raise {1 * canraisetokens} token" });
			this.betraisemenu.AddItem(new SelectItem() { Id = 2 * canraisetokens, Text = $"Raise {2 * canraisetokens} token" });
			this.betraisemenu.AddItem(new SelectItem() { Id = 3 * canraisetokens, Text = $"Raise {3 * canraisetokens} token" });
			this.betraisemenu.AddItem(new SelectItem() { Id = 4 * canraisetokens, Text = $"Raise {4 * canraisetokens} token" });
			this.betraisemenu.AddItem(new SelectItem() { Id = 5 * canraisetokens, Text = $"Raise {5 * canraisetokens} token" });

		}


		void SetStdColor() => ui.SetColor(ConsoleColor.Green, ConsoleColor.Black); 
		void SetErrorColor() => ui.SetColor(ConsoleColor.White, ConsoleColor.Red);
		void SetInactiveColor() => ui.SetColor(ConsoleColor.DarkGreen, ConsoleColor.Black);
		void SetActiveColor() => ui.SetColor(ConsoleColor.Yellow, ConsoleColor.Black);
		void SetHighLightColor() => ui.SetColor(ConsoleColor.Yellow, ConsoleColor.Blue);
		void SetPlayCardColorBlack() => ui.SetColor(ConsoleColor.Black, ConsoleColor.White);
		void SetPlayCardColorRed() => ui.SetColor(ConsoleColor.Red, ConsoleColor.White);
		void SetPlayCardColorBackside() => ui.SetColor(ConsoleColor.Gray, ConsoleColor.White);

		ConColor StdColor = new ConColor(ConsoleColor.Green, ConsoleColor.Black);
		ConColor InactiveColor = new ConColor(ConsoleColor.DarkGreen, ConsoleColor.Black);
		ConColor InTurnColor = new ConColor(ConsoleColor.Yellow, ConsoleColor.Black);
		ConColor ErrorColor = new ConColor(ConsoleColor.Red, ConsoleColor.Black);
		ConColor HighLightColor = new ConColor(ConsoleColor.Yellow, ConsoleColor.Blue);

		void SetUpColors() {
			this.consoletable.Color(StdColor);
			this.consoletable.Border.Color(StdColor);
			this.consoletable.CommonSeat.Color(StdColor);
			this.consoletable.CommonSeat.InactiveColor.Set(InactiveColor);
			this.consoletable.CommonSeat.InTurnColor.Set(InTurnColor);
			this.consoletable.CommonSeat.HighLightColor.Set(HighLightColor);
		}


		void WritePlayCard(IPlayCard card) {
			var backside = new CStr(2, 21).ToString();
			switch (card.Suit) {
				case PlayCardSuit.Invalid: SetPlayCardColorBackside();break;
				case PlayCardSuit.Heart: SetPlayCardColorRed(); break;
				case PlayCardSuit.Diamond: SetPlayCardColorRed(); break;
				default: SetPlayCardColorBlack(); break;
			}
			if (card.Suit == PlayCardSuit.Invalid) ui.Show(backside);
			else ui.Show(card.Symbol);
			SetStdColor();
			ui.MoveCursor(columns: 4, rows: 0);
		}



		TexasConsoleTable consoletable;

		ConIO ui;


		ConMenu betmenu;
		ConMenu betraisemenu;
		ConMenu playroundmenu;
		ICardTable table;
		CStr msg;
		bool SortCardsFunc(IPlayCard c1, IPlayCard c2) { if (c1.Rank < c2.Rank) return true; return false; }

	}





}
