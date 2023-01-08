using Syslib;
using Syslib.Games;
using Syslib.Games.Card;
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


		public void ShowHelp() {
			ShowMsg("Arguments:");
			ShowMsg(" r = rounds to play     s = number of table seats    p = number of players");
			ShowMsg(" t = player tokens      ? = help");
			ShowMsg(" -q  = silent game output except for statistics");
			ShowMsg(" -qr = silent game output, override for round summary");
			ShowMsg(" -s = show statistics");
			ShowMsg(" ex:  r10 p4");
			ShowMsg(" ex:  r10 p6 s10 t500 -s");
			ShowMsg(" ex:  r100 p6 s6 t1500 -s -q");
			ShowMsg(" ex:  r100 p6 s6 t1500 -s -qr");
		}


		public void ShowProgressMessage(string msg) {

			if (SupressOutput) return;
			MsgLog.AddMsg(msg);
			UpdateMessageLog();
		}

		public void Finish() {
			Console.CursorVisible = true;
		}

		public void ShowNewRound(ICardTable table) {


			if (SupressOutput) return;
			Console.CursorVisible = false;
			Console.Clear();
			playerseats.Clear();
			MsgLog.Clear();

			WriteXY(0, 0, "------------------ NEW ROUND | Texas Hold'em  ----------------");

			SetUpTable(table);
			UpdatePlayers();
			UpdateMessageLog();
		}


		public void ShowRoundSummary(ICardTable table) {

			if ((SupressOutput) && (!SupressOverrideRoundSummary)) return;

			int counter = 0;
			CList<IPlayCard> playerhand;

			if (!SupressOutput) Console.Clear();

			foreach (var seat in table.TableSeats)
			{
				if (!seat.IsFree)
				{
					msg.Clear();
					msg.Append($" Seat {counter,2}. ");
					msg.Append($"{seat.Player.Name,-15}  {seat.Player.Tokens,10}  {seat.IsActive,5}   { seat.Player.Cards.Rank.Name,-20 } ");
					if (seat.Player.Cards.WinHand) msg.Append(" *WIN* "); else msg.Append("       ");
					playerhand = seat.Player.Cards.GetCards().Sort(SortCardsFunc);
					foreach (var card in playerhand)
					{
						msg.Append($"  {card.Symbol}");
					}
					ShowMsg(msg.ToString());
				}
				counter++;
			}
			ShowMsg($" Pot tokens:             {table.TablePot.Tokens,12}");
		}


		public void ShowGameStatistics(TexasHoldEmStatistics statistics)
		{
			if ((SupressOutput) && (!SupressOverrideStatistics)) return;

			if (statistics != null) {
				int TotalHands, TotalWin;
				ShowMsg("\nStatistics:                  Winning Hand                Hands");
				TotalHands = 0; TotalWin = 0;
				double winpct, handpct, hands, handswin, hand, handwin;
				for (int count = 0; count < statistics.StatsHands.Length; count++) { TotalHands += statistics.StatsHands[count]; TotalWin += statistics.StatsWinnerHands[count]; }
				hands = TotalHands; handswin = TotalWin;
				for (int count = 0; count < statistics.StatsHands.Length; count++)
				{
					hand = 100 * statistics.StatsHands[count];
					handwin = 100 * statistics.StatsWinnerHands[count];
					winpct = handwin / handswin; handpct = hand / hands;
					ShowMsg($"{count,2}.  {(TexasHoldEmHand)count,-20}   {winpct,5:f1} %   {statistics.StatsWinnerHands[count],7}           {handpct,5:f1} %   {statistics.StatsHands[count],7}");
				}
				ShowMsg($"-Rounds played {statistics.GamesPlayed,7}        Total:  {TotalWin,7}                     {TotalHands,7}");
			}
		}

		public void ShowGamePlayerStatistics(ICardTable table)
		{
			if ((SupressOutput) && (!SupressOverrideStatistics)) return;

			if (table != null) {
				ShowMsg("Players:");
				foreach (var seat in table.TableSeats)
				{
					if (!seat.IsFree) ShowMsg($" {seat.Player.Name,-20} Tokens {seat.Player.Tokens,10}");
				}
			}
		}

		public void ShowPlayerCards(ICardTableSeat playerseat, ICardTableSeat dealerset) {

			if (SupressOutput) return;

			var playercards = playerseat.Player.Cards.GetPrivateCards();
			var dealercards = dealerset.Player.Cards.GetPublicCards();
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
			this.UpdatePlayers();
		}

		public void ShowPlayerSeat(ICardTableSeat seat) {
			foreach (var s in playerseats)
			{
				if (s.Seat.Player != null && s.Seat == seat) { 
					if (s.Seat.Player.Type == GamePlayerType.Default) UpdateDealer(s);
					else UpdatePlayer(s); 
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
		}




		void ShowMsg(string msg)
		{
			if (msg == null) Console.WriteLine("");
			else Console.WriteLine(msg);
		}

		void UpdatePlayers()
		{
			foreach (var seat in this.playerseats)
			{
				if (seat.Seat.IsFree)
				{
					WriteXY(seat.X, seat.Y, "Empty");
				}
				else if (seat.Seat.Player.Type == GamePlayerType.Default)
				{
					UpdateDealer(seat);
				}
				else
				{
					UpdatePlayer(seat);
				}
			}
		}

		void UpdateDealer(PlayerSeat seat) {
			var Cards = seat.Seat.Player.Cards.GetCards().Sort(SortCardsFunc);
			var Hand = new CStr();
			foreach (var c in Cards) { Hand.Append($"{c.Symbol}  "); }
			seat.Cards = Hand.FilterRemoveTrail(" ").ToString();
			WriteXY(seat.X, seat.Y, "Dealer");
			WriteXY(seat.X, seat.Y + 2, $"Pot{this.table.TablePot.Tokens,7}");
			WriteXY(seat.X, seat.Y + 4, $"{seat.Cards}");
		}

		void UpdatePlayer(PlayerSeat seat)
		{
			var Cards = seat.Seat.Player.Cards.GetCards().Sort(SortCardsFunc);
			var Hand = new CStr();
			foreach (var c in Cards) { Hand.Append($"{c.Symbol}  "); }
			seat.Cards = Hand.FilterRemoveTrail(" ").ToString();
			if (seat.Seat.Player.Cards.Rank != null) seat.Hand = seat.Seat.Player.Cards.Rank.Name;
			WriteXY(seat.X, seat.Y, $"{seat.Seat.Player.Name}");
			WriteXY(seat.X, seat.Y + 1, $"Tkns{seat.Seat.Player.Tokens,6}");
			WriteXY(seat.X, seat.Y + 2, $"Bet{seat.Seat.Bets,7}");
			WriteXY(seat.X, seat.Y + 3, $"{seat.Seat.Player.Status,-10}");
			WriteXY(seat.X, seat.Y + 4, $"{seat.Cards}");
			if (seat.Seat.Player.Cards.WinHand) WriteXY(seat.X, seat.Y + 5, $"*WIN*");
		}

		void UpdateMessageLog() {
			int y = 0;

			foreach (var message in this.MsgLog.Msg)
			{
				WriteXY(MsgLog.X, MsgLog.Y + y, message);
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
			this.MsgLog.X = 74;
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

		class ProgressLog {
			public int X { get; set; }
			public int Y { get; set; }
			public void Clear() { for (int i = 0; i < loglength; i++) Msg[i] = ""; }
			public void AddMsg(string msg) { 
				int i = loglength; 
				while (--i > 0) { Msg[i] = Msg[i - 1]; }
				Msg[0] = $"{msg,-logwidth}";
			}
			const int loglength = 5;
			const int logwidth = 30;

			public string[] Msg  = new string[loglength];
		}

		class Menu {
			public int X { get; set; }
			public int Y { get; set; }
			int mnuret;
			public int Ask(int tokens) {
				var mnu = new Syslib.ConsoleIO.ConMenu(X, Y, 15, true, false);
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
				var mnu = new Syslib.ConsoleIO.ConMenu(X+1, Y+1, 18, true, false);
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


		CList<PlayerSeat> playerseats;
		ProgressLog MsgLog = new ProgressLog();
		Menu menu = new Menu();
		ICardTable table;
		CStr msg;
		bool SortCardsFunc(IPlayCard c1, IPlayCard c2) { if (c1.Rank < c2.Rank) return true; return false; }

	}
}
