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
		}

		public void ShowMsg(string msg)
		{
			if (!SupressOutput) {
				if (msg == null) Console.WriteLine("");
				else Console.WriteLine(msg);
			}
		}


		public void ShowNewRound(ICardGameTable table) {
			ShowMsg("------------------ NEW ROUND | Texas Hold'em  ----------------");
		}


		public void ShowTableSeatHands(ICardGameTable table) {
			int counter = 0;
			CList<Card> playerhand;
			CStr msg = new CStr();

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

		bool SortCardsFunc(Card c1, Card c2) { if (c1.Rank < c2.Rank) return true; return false; }

		public bool SupressOutput { get; set; }

	}
}
