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

			this.ShowMsg(msg);
		}



		public void ShowNewRound(ICardTable table) {

			if (SupressOutput) return;

			ShowMsg("------------------ NEW ROUND | Texas Hold'em  ----------------");
		}


		public void ShowRoundSummary(ICardTable table) {
			int counter = 0;
			CList<IPlayCard> playerhand;

			if ((SupressOutput) && (!SupressOverrideRoundSummary)) return;

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
//				Syslib.ConsoleIO.ConIO.PInstance.ClearScrn();
				int TotalHands, TotalWin;
				ShowMsg("\nStatistics:                  Winning Hand                Hands");
				TotalHands = 0; TotalWin = 0;
				double winpct, handpct;
				for (int count = 0; count < statistics.StatsHands.Length; count++) { TotalHands += statistics.StatsHands[count]; TotalWin += statistics.StatsWinnerHands[count]; }
				for (int count = 0; count < statistics.StatsHands.Length; count++)
				{
					winpct = 100 * statistics.StatsWinnerHands[count] / TotalWin; handpct = 100 * statistics.StatsHands[count] / TotalHands;
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

		public void ReDrawGameTable(ICardTable table) {

			if (SupressOutput) return;

			Syslib.ConsoleIO.ConIO.PInstance.ClearScrn();
			int x1 = 0, y1 = 0, x2, y2, logx = 0, logy = 16, s = 0;
			int x3 = 75, y3 = 0;
			foreach (var seat in table.TableSeats) {
				x2 = x1; y2 = y1;
				Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"Seat {s,2}");
				if (seat.IsFree)
				{
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"Empty");
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"");
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"");
				}
				else {
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x3, y3++, $"{seat.Player.Name}");
					if (seat.Player.Type == GamePlayerType.Default)
					{
						Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x3, y3++, $"Pot:");
						Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x3, y3++, $"{table.TablePot.Tokens}");
						Syslib.ConsoleIO.ConIO.PInstance.ShowXY(50, 6, "");
					}
					else {
						Syslib.ConsoleIO.ConIO.PInstance.ShowXY(logx, logy++, $"{seat.Player.Status}");
						Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"{seat.Player.Tokens}");
					}
				}
				s++;
				x1 += 15;
				if (x1 >= 75) { y1 += 8; x1 = 0; }
			}

		}


		// respond to request fold/check/call/raise
		// -1 Fold, 0 call or check, >0 raise that amount
		public int AskForBet(int tokens)
		{
			if (SupressOutput) return 0;

			var mnu = new Syslib.ConsoleIO.ConMenu(60, 10, 10, true, false);
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

		int RespondRaise() {
			var mnu = new Syslib.ConsoleIO.ConMenu(61, 12, 20, true, false);
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



		void ShowMsg(string msg)
		{
			if (msg == null) Console.WriteLine("");
			else Console.WriteLine(msg);
		}




		int mnuret;
		CStr msg;
		bool SortCardsFunc(IPlayCard c1, IPlayCard c2) { if (c1.Rank < c2.Rank) return true; return false; }

	}
}
