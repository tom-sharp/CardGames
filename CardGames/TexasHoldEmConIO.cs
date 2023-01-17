using Syslib;
using Syslib.Games;
using Syslib.Games.Card;
using Syslib.ConUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmConIO : ITexasHoldEmIO
	{
		public TexasHoldEmConIO()
		{
			this.SupressOutput = false;
			this.SupressOverrideRoundSummary = false;
			this.msg = new CStr();
			this.playerseats = new CList<PlayerSeat>();
		}

		public bool SupressOutput { get; set; }
		public bool SupressOverrideRoundSummary { get; set; }
		public bool SupressOverrideStatistics { get; set; }

		public void Clear() {
			Console.Clear();
		}

		public void ShowHelp() {
			ui.PushColor();
			SetStdColor();
			ShowMsg("Arguments:");
			ShowMsg(" r = rounds to play     s = number of table seats    p = number of players");
			ShowMsg(" t = player tokens      ? = help");
			ShowMsg(" -s  = silent game output except for statistics");
			ShowMsg(" -qr = silent game output, override for round summary");
			ShowMsg(" -s  = show statistics");
			ShowMsg(" -db = save statistics to database");
			ShowMsg(" ex:  r10 p4");
			ShowMsg(" ex:  r10 p6 s10 t500 -s");
			ShowMsg(" ex:  r100 p6 s6 t1500 -s -q");
			ShowMsg(" ex:  r100 p6 s6 t1500 -s -qr");
			ShowMsg(" ex:  r100 p6 s6 t1500 -s -qr -db");
			ui.PopColor();
		}


		public void ShowProgressMessage(string msg) {
			if (SupressOutput) return;
			var str = new CStr(msg);
			while (str.Length() < this.MsgLog.Width) str.Append(' ');
			str.Set(MsgLog.Width, 0);
			MsgLog.Add(str);
			UpdateMessageLog();
		}

		public void Finish() {
			if (SupressOutput) return;
			ui.RestoreColor();
			ui.HideCursor(false);
		}

		public void ShowNewRound(ICardTable table) {


			if (SupressOutput) return;
			SetStdColor();
			ui.HideCursor();
			ui.Clear();
			playerseats.Clear();
			MsgLog.Clear();

			ui.ShowXY(0, 0, "------------------ NEW ROUND | Texas Hold'em  ----------------");

			SetUpTable(table);
			UpdatePlayers(showcards: false);
			UpdateMessageLog();
		}


		public void ShowRoundSummary(ICardTable table, bool samepage) {

			if ((SupressOutput) && (!SupressOverrideRoundSummary)) return;

			int counter = 0;
			IPlayCards playerhand;

			//if (!SupressOutput) Console.Clear();
			//SetStdColor();

			if (samepage && !SupressOutput)
			{
				UpdatePlayers(showcards: true);
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
				ui.ShowXY(this.menu.X, this.menu.Y, "Press Any Key");
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
					TotalRank[entity.WinRankId]++;
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

		public void ShowPlayerCards(ICardTableSeat playerseat, ICardTableSeat dealerset) {

			if (SupressOutput) return;

			var playercards = playerseat.Player.Cards.GetCards();
			var dealercards = dealerset.Player.Cards.GetCards();
			msg.Str($" {playerseat.Player.Name}: ");
			foreach (var c in playercards) {
				msg.Append($" {c.Symbol} ");
			}
			msg.Append(" | ");
			foreach (var c in dealercards)
			{
				msg.Append($" {c.Symbol} ");
			}
			ShowMsg(msg.ToString());
		}

		public void ReDrawGameTable() {
			this.UpdatePlayers(showcards: false);
		}

		public void ShowPlayerSeat(ICardTableSeat seat) {
			foreach (var s in playerseats)
			{
				if (s.Seat.Player != null && s.Seat == seat) { 
					if (s.Seat.Player.Type == GamePlayerType.Default) UpdateDealer(s);
					else UpdatePlayer(s, showcards: false); 
					break; 
				}
			}
		}

		public void ShowPlayerActiveSeat(ICardTableSeat seat)
		{
			foreach (var s in playerseats)
			{
				if (s.Seat.Player != null && s.Seat == seat)
				{
					if (s.Seat.Player.Type == GamePlayerType.Default) UpdateDealerActive(s);
					else UpdatePlayerActive(s);
					break;
				}
			}
		}


		// respond to request fold/check/call/raise
		// -1 Fold, 0 call or check, >0 raise that amount
		public int AskForBet(int tokens)
		{
			if (SupressOutput) return 0;
			int result = this.menu.Ask(tokens);
			Cleanup(this.menu.X, this.menu.Y, 21, 10);
			return result;
		}

		void Cleanup(int x, int y, int width, int height) {
			var str = new CStr(width, 32);
			int count = 0;
			while (count < height) { WriteXY(x, y + count, str.ToString()); count++; }
			ui.HideCursor();
		}




		public void ShowMsg(string msg)
		{
			if (msg == null) Console.WriteLine("");
			else Console.WriteLine(msg);
		}

		void UpdatePlayers(bool showcards)
		{
			foreach (var seat in this.playerseats)
			{
				if (seat.Seat.IsFree)
					ui.ShowXY(seat.X, seat.Y, "Empty");
				else if (seat.Seat.Player.Type == GamePlayerType.Default)
					UpdateDealer(seat);
				else
					UpdatePlayer(seat, showcards: showcards);
			}
		}

		void UpdateDealer(PlayerSeat seat) {
			var Cards = seat.Seat.Player.Cards.GetCards();
			var Hand = new CStr();
			foreach (var c in Cards) { Hand.Append($"{c.Symbol}  "); }
			seat.Cards = Hand.FilterRemoveTrail(" ").ToString();
			SetStdColor();
			ui.ShowXY(seat.X, seat.Y, "Dealer");
			ui.ShowXY(seat.X, seat.Y + 2, $"Pot{this.table.TablePot.Tokens,7}");
			ui.ShowXY(seat.X, seat.Y + 4, $"");
			foreach (var c in Cards) { WritePlayCardLarge(c); }
		}

		void UpdateDealerActive(PlayerSeat seat)
		{
			var Cards = seat.Seat.Player.Cards.GetCards();
			var Hand = new CStr();
			foreach (var c in Cards) { Hand.Append($"{c.Symbol}  "); }
			seat.Cards = Hand.FilterRemoveTrail(" ").ToString();

			ui.PushColor();
			SetHighLightColor();

			ui.ShowXY(seat.X, seat.Y, $"{seat.Seat.Player.Name}");
			SetActiveColor();

			ui.ShowXY(seat.X, seat.Y + 2, $"Pot{this.table.TablePot.Tokens,7}");
			ui.ShowXY(seat.X, seat.Y + 4, $"");
			foreach (var c in Cards) { WritePlayCardLarge(c); }

			ui.PopColor();
		}


		void UpdatePlayer(PlayerSeat seat, bool showcards)
		{
			var Cards = seat.Seat.Player.Cards.GetCards();
			var joker = new PlayCardJoker();
			var invalid = new PlayCardHeart(0);

			if (seat.Seat.IsActive) SetStdColor(); else SetInactiveColor();
			if (seat.Seat.Player.Cards.WinHand) SetActiveColor();

			ui.ShowXY(seat.X, seat.Y, $"{seat.Seat.Player.Name}");
			ui.ShowXY(seat.X, seat.Y + 1, $"Tkns{seat.Seat.Player.Tokens,6}");
			ui.ShowXY(seat.X, seat.Y + 2, $"Bet{seat.Seat.Bets,7}");
			ui.ShowXY(seat.X, seat.Y + 3, $"{seat.Seat.Player.Status,-10}");
			if (seat.Seat.Player.Cards.WinHand) { ui.ShowXY(seat.X, seat.Y + 4, $" *WIN*"); }
			ui.ShowXY(seat.X, seat.Y + 5, $"");
			ui.PushColor();
			if (seat.Seat.IsActive)
			{
				if (seat.Seat.Player.Type == GamePlayerType.Human)
					foreach (var c in Cards) { WritePlayCardLarge(c); }
				else if (showcards)
					foreach (var c in Cards) { WritePlayCardLarge(c); }
				else
					foreach (var c in Cards) { WritePlayCardLarge(invalid); }
			}
			else {
				ui.PushCursor();
				ui.Show(new CStr(8, 32).ToString());
				ui.PopCursor(); ui.MoveCursor(columns: 0, rows: 1); ui.PushCursor();
				ui.Show(new CStr(8, 32).ToString());
				ui.PopCursor(); ui.MoveCursor(columns: 0, rows: 1);
				ui.Show(new CStr(8, 32).ToString());
			}
			ui.PopColor();

		}

		void UpdatePlayerActive(PlayerSeat seat)
		{
			var Cards = seat.Seat.Player.Cards.GetCards();
			var Hand = new CStr();
			var invalid = new PlayCardHeart(0);
			var joker = new PlayCardJoker();

			if (seat.Seat.Player.Cards.Signature != null) seat.Hand = seat.Seat.Player.Cards.Signature.Name;
			ui.PushColor();

			SetHighLightColor();
			ui.ShowXY(seat.X, seat.Y, $"{seat.Seat.Player.Name}");
			SetActiveColor();

			ui.ShowXY(seat.X, seat.Y + 1, $"Tkns{seat.Seat.Player.Tokens,6}");
			ui.ShowXY(seat.X, seat.Y + 2, $"Bet{seat.Seat.Bets,7}");
			ui.ShowXY(seat.X, seat.Y + 3, $"{seat.Seat.Player.Status,-10}");
			ui.ShowXY(seat.X, seat.Y + 5, $"");

			if (seat.Seat.Player.Type == GamePlayerType.Human)
				foreach (var c in Cards) { WritePlayCardLarge(c); }
			else
				foreach (var c in Cards) { WritePlayCardLarge(invalid); }

			ui.PopColor();
		}

		void UpdateMessageLog() {
			int y = 0;
			SetStdColor();

			while (this.MsgLog.Count() > this.MsgLog.Height) { this.MsgLog.First(); this.MsgLog.Remove(); }

			foreach (var message in this.MsgLog)
			{
				ui.ShowXY(MsgLog.X, MsgLog.Y + y, $"{message}");
				y++;
			}

		}

		/// <summary>
		/// Setup screen for players
		/// est 20 char width / player and 7 rows height
		/// P P P P P D
		/// P P P P P
		/// log
		/// log
		/// log
		/// </summary>
		/// <param name="table"></param>
		void SetUpTable(ICardTable table) {
			int topleftX = 2, topleftY = 2, dealerX = 92, dealerY = 2;

			this.table = table;
			foreach (var seat in table.TableSeats) {
				if (seat.Player != null && seat.Player.Type == GamePlayerType.Default) {
					this.playerseats.Add(new PlayerSeat() { Seat = seat, X = dealerX, Y = dealerY, Cards = "" });
				}
				else {
					this.playerseats.Add(new PlayerSeat() { Seat = seat, X = topleftX, Y = topleftY, Cards = "" });
					topleftX += 18;
				}
				if (topleftX >= 90) { topleftX = 2; topleftY += 10; }
			}
			this.MsgLog.X = 70;
			this.MsgLog.Y = 18;
			this.menu.X = 74;
			this.menu.Y = 10;

		}

		class PlayerSeat {
			public ICardTableSeat Seat { get; set; }
			public int X { get; set; }
			public int Y { get; set; }
			public string Hand { get; set; } = "";
			public string Cards { get; set; } = "";
		}


		class ProgressLog : CList<CStr> {
			public int X { get; set; }
			public int Y { get; set; }
			public int Width => 43;
			public int Height => 5;

		}

		class Menu {
			public int X { get; set; }
			public int Y { get; set; }
			int mnuret;
			public int Ask(int tokens) {
				var mnu = new Syslib.ConUI.ConMenu(X, Y, 15, true, false);
				mnuret = 0;
				if (tokens > 0)
				{
					mnu.AddItem("Fold");
					mnu.AddItem($"Call {tokens} tokens");
					mnu.AddItem($"Raise", RespondRaise);
					switch (mnu.Select())
					{
						case 1: return -1;  // fold
						case 2: return 0;  // call
						case 3: return mnuret;  // raise
					}
				}
				else
				{
					mnu.AddItem("Check");
					mnu.AddItem("Raise", RespondRaise);
					switch (mnu.Select())
					{
						case 1: return 0;  // check
						case 2: return mnuret;  // raise
					}
				}
				if (tokens > 0) return -1;
				return 0;
			}

			int RespondRaise()
			{
				var mnu = new Syslib.ConUI.ConMenu(X+1, Y+1, 18, true, false);
				mnu.AddItem($"Raise 1 token");
				mnu.AddItem($"Raise 2 tokens");
				mnu.AddItem($"Raise 3 tokens");
				mnu.AddItem($"Raise 4 tokens");
				mnu.AddItem($"Raise 5 tokens");
				switch (mnu.Select())
				{
					case 1: mnuret = 1; return 0;
					case 2: mnuret = 2; return 0;
					case 3: mnuret = 3; return 0;
					case 4: mnuret = 4; return 0;
					case 5: mnuret = 5; return 0;
				}
				mnuret = 0;
				return 0;
			}

		}

		void WriteXY(int x, int y, string msg) { Console.CursorLeft = x; Console.CursorTop = y; Console.Write(msg); }

		void SetStdColor() { 
			ui.SetColor(ConsoleColor.Green, ConsoleColor.Black);
		}

		void SetInactiveColor()
		{
			ui.SetColor(ConsoleColor.DarkGreen, ConsoleColor.Black);
		}

		void SetActiveColor()
		{
			ui.SetColor(ConsoleColor.Yellow, ConsoleColor.Black);
		}

		void SetHighLightColor()
		{
			ui.SetColor(ConsoleColor.Yellow, ConsoleColor.Blue);
		}
		void SetPlayCardColorBlack()
		{
			ui.SetColor(ConsoleColor.Black, ConsoleColor.White);
		}
		void SetPlayCardColorRed()
		{
			ui.SetColor(ConsoleColor.Red, ConsoleColor.White);
		}

		void SetPlayCardColorBackside() { 
			ui.SetColor(ConsoleColor.Gray, ConsoleColor.White);
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

		void WritePlayCardLarge(IPlayCard card)
		{
			var backside = new CStr(3, 21).ToString();
			switch (card.Suit)
			{
				case PlayCardSuit.Invalid: SetPlayCardColorBackside(); break;
				case PlayCardSuit.Heart: SetPlayCardColorRed(); break;
				case PlayCardSuit.Diamond: SetPlayCardColorRed(); break;
				default: SetPlayCardColorBlack(); break;
			}
			ui.PushCursor();
			ui.PushCursor();
			if (card.Suit == PlayCardSuit.Invalid) ui.Show(backside);
			else { ui.Show(card.Symbol[0]); ui.Show("  "); }
			ui.PopCursor(); ui.MoveCursor(columns: 0, rows: 1); ui.PushCursor();
			if (card.Suit == PlayCardSuit.Invalid) ui.Show(backside);
			else { ui.Show(' '); ui.Show(card.Symbol[1]); ui.Show(' '); }
			ui.PopCursor(); ui.MoveCursor(columns: 0, rows: 1);
			if (card.Suit == PlayCardSuit.Invalid) ui.Show(backside);
			else { ui.Show("  "); ui.Show(card.Symbol[0]); }
			ui.PopCursor();
			SetStdColor();
			ui.MoveCursor(columns: 5, rows: 0);
		}


		ConIO ui = ConIO.PInstance;
		CList<PlayerSeat> playerseats;
		ProgressLog MsgLog = new();
		Menu menu = new();
		ICardTable table;
		CStr msg;
		bool SortCardsFunc(IPlayCard c1, IPlayCard c2) { if (c1.Rank < c2.Rank) return true; return false; }

	}





}
