using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syslib;

namespace Games.Card
{

	/// <summary>
	/// CardGameTable provide a plattform to put togeather players and a card game
	/// The dealer control card game flow and what type of card game to play
	/// Seat position 0 is reserved for the dealer if required to participate in game
	/// </summary>
	public abstract class CardGameTable
	{
		public CardGameTable(int seats, ICardGameDealer dealer)
		{
			this.carddealer = dealer;
			if (seats >= 0) this.tableseats = new CardGameTableSeat[seats + 1];
			else this.tableseats = new CardGameTableSeat[1];
			for (int seat = 0; seat < this.tableseats.Length; seat++) { this.tableseats[seat] = new CardGameTableSeat(); }
			this.tableseats[0].Join(new CardPlayer(name: "Dealer"));
			this.RoundsPlayed = 0;
		}

		public bool JoinTable(CardPlayer player) {
			int seat = 1;
			if (player == null) return false;
			while (seat < tableseats.Length) {
				if (tableseats[seat].IsFree()) {
					if (!tableseats[seat].Join(player)) return false;
					player.TableSeat = seat;
					return true;
				}
				seat++;
			}
			return false;
		}

		public void LeaveTable(CardPlayer player) {
			if (player == null) return;
			LeaveTable(player.TableSeat);
		}

		public void LeaveTable(int seat) {
			if ((seat < 1) || (seat >= tableseats.Length)) return;     // do not remove dealer
			tableseats[seat].Leave();
		}

		public void Run() {
			if (this.carddealer == null) return;
			while (this.carddealer.Run(tableseats)) {
				RoundsPlayed++;

				// Allow here to  opt in or out new players after each round
				// Run method alwas return true unless there is a problem to continue

			}
		}

		public int TokenPot { get; private set; }

		// Statistics
		public int RoundsPlayed { get; set; }


		ICardGameDealer carddealer = null; 
		CardGameTableSeat[] tableseats = null;
	}
}
