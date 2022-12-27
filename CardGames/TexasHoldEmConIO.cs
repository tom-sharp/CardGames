using Syslib;
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



		public void ShowNewRound(ICardGameTable table) {
			if (SupressOutput) return;
			ShowMsg("------------------ NEW ROUND | Texas Hold'em  ----------------");
		}


		public void ShowRoundSummary(ICardGameTable table) {
			int counter = 0;
			CList<IPlayCard> playerhand;

			if ((SupressOutput) && (!SupressOverrideRoundSummary)) return;

			msg.Str($" Seat {counter,2}. ");
			msg.Append($"{table.DealerSeat.Player.Name,-15}  {table.DealerSeat.Player.Wallet.Tokens,10}  {table.DealerSeat.IsActive}  ");
			msg.Append("                             "); 
			playerhand = table.DealerSeat.PlayerCards.GetCards().Sort(SortCardsFunc);
			foreach (var card in playerhand)
			{
				msg.Append($"  {card.Symbol}");
			}
			ShowMsg(msg.ToString());
			counter++;

			foreach (var p in table.TableSeats)
			{
				if (!p.IsFree)
				{
					msg.Clear();
					msg.Append($" Seat {counter,2}. ");
					msg.Append($"{p.Player.Name,-15}  {p.Player.Wallet.Tokens,10}  {p.IsActive}   {p.PlayerCards.HandName,-20} ");
					if (p.PlayerCards.WinHand) msg.Append(" *WIN* "); else msg.Append("       ");
					playerhand = p.PlayerCards.GetCards().Sort(SortCardsFunc);
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
				double winpct, handpct;
				for (int count = 0; count < statistics.StatsHands.Length; count++) { TotalHands += statistics.StatsHands[count]; TotalWin += statistics.StatsWinnerHands[count]; }
				for (int count = 0; count < statistics.StatsHands.Length; count++)
				{
					winpct = 100 * statistics.StatsWinnerHands[count] / TotalWin; handpct = 100 * statistics.StatsHands[count] / TotalHands;
					ShowMsg($"{count,2}.  {(TexasHoldEmHand)count,-20}   {winpct,5:f1} %   {statistics.StatsWinnerHands[count],7}           {handpct,5:f1} %   {statistics.StatsHands[count],7}");
				}
				ShowMsg($"-Rounds played {statistics.RoundsPlayed,7}        Total:  {TotalWin,7}                     {TotalHands,7}");
			}
		}

		public void ShowGamePlayerStatistics(CList<ICardPlayer> playerlist)
		{
			if ((SupressOutput) && (!SupressOverrideStatistics)) return;
			if (playerlist != null) {
				ShowMsg("Players:");
				foreach (var p in playerlist)
				{
					ShowMsg($" {p.Name,-20} Tokens {p.Wallet.Tokens,10}");
				}
			}
		}

		public void ShowPlayerCards(ICardGameTableSeat playerseat, ICardGameTableSeat dealerset) {
			if (SupressOutput) return;
			var playercards = playerseat.PlayerCards.GetPrivateCards();
			var dealercards = dealerset.PlayerCards.GetPublicCards();
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

		public void ReDrawGameTable(ICardGameTable table) {
			Syslib.ConsoleIO.ConIO.PInstance.ClearScrn();
			int x1 = 0, y1 = 0, x2, y2, logx = 0, logy = 16, s = 1;
			foreach (var seat in table.TableSeats) {
				x2 = x1; y2 = y1;
				Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"Seat {s,2}");
				if (seat.IsFree)
				{
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"Empty");
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"");
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"");
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"");
				}
				else {
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"{seat.Player.Name}");
					if (seat.IsActive) Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"Active");
					else Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"Fold");
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"{seat.Player.Wallet.Tokens}");
					Syslib.ConsoleIO.ConIO.PInstance.ShowXY(logx, logy++, $"{seat.Comment}");
				}
				s++;
				x1 += 15;
				if (x1 >= 75) { y1 += 8; x1 = 0; }
			}

			var dealer = table.DealerSeat;
			x2 = 75; y2 = 0;
			Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"Dealer");
			Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"{dealer.Player.Name}");
			Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"");
			Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"Pot:");
			Syslib.ConsoleIO.ConIO.PInstance.ShowXY(x2, y2++, $"{table.TablePot.Tokens}");
			Syslib.ConsoleIO.ConIO.PInstance.ShowXY(50, 6, "");

		}


		// respond to request fold/check/call/raise
		// -1 Fold, 0 call or check, >0 raise that amount
		public int AskForBet(int tokens)
		{
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
