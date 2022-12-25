using Syslib;
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
			CList<Card> playerhand;
			CStr msg = new CStr();

			if ((SupressOutput) && (!SupressOverrideRoundSummary)) return;

			msg.Str($" Seat {counter,2}. ");
			msg.Append($"{table.DealerSeat.PlayerName,-15}  {table.DealerSeat.PlayerTokens,10}  {table.DealerSeat.IsActive}  ");
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
				if (!p.IsFree())
				{
					msg.Clear();
					msg.Append($" Seat {counter,2}. ");
					msg.Append($"{p.PlayerName,-15}  {p.PlayerTokens,10}  {p.IsActive}   {p.PlayerCards.HandName,-20} ");
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

		public void ShowGamePlayerStatistics(CList<CardPlayer> playerlist)
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



		void ShowMsg(string msg)
		{
			if (msg == null) Console.WriteLine("");
			else Console.WriteLine(msg);
		}


		bool SortCardsFunc(Card c1, Card c2) { if (c1.Rank < c2.Rank) return true; return false; }

	}
}
