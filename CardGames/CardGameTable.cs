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
		public CardGameTable(int seats, ICardGameDealer dealer = null)
		{
			this.carddealer = dealer;
			if (seats >= 0) this.tableseats = new CardGameTableSeat[seats + 1];
			else this.tableseats = new CardGameTableSeat[1];
			for (int seat = 0; seat < this.tableseats.Length; seat++) { this.tableseats[seat] = new CardGameTableSeat(); }
			this.tableseats[CardGame.DealerSeatNumber].Join(new CardPlayer(name: "Dealer"));
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

		public void Run(int rounds = 2) {
			if (this.carddealer == null) return;
			while (this.carddealer.Run(this)) {
				RoundsPlayed++;

				// Allow here to  opt in or out new players after each round
				// Run method always return true unless there is a problem to continue
				if(RoundsPlayed >= rounds) break;
			}
		}

		// TEMPORARY WHILE MIGRATING FUNTIONALITY TO TABLE
//		public CardGameTableSeat[] Seats { get { return this.tableseats; } }

		/// <summary>
		/// return all the table seats as long there is a player at the seat. Dealer excluded
		/// </summary>
		public IEnumerable<CardGameTableSeat> TableSeats {
			get {
				int seat = 0;
				while (seat < this.tableseats.Length)
				{
					if ((seat != CardGame.DealerSeatNumber) && (this.tableseats[seat] != null)) {
						yield return this.tableseats[seat];
					}
					seat++;
				}
			}
		}

		/// <summary>
		/// return number of active player seats (dealer seat is excluded from count of active seats)
		/// </summary>
		public int CountActiveSeats 
		{ 
			get { 
				int count = 0;
				foreach (var seat in this.tableseats) { if ((seat != null) && (seat.Active)) count++; }
				return count - 1;	// remove dealer count
			} 
		}

		public int CountSeats { get { return this.tableseats.Length; } }

		// return Dealer seat
		public CardGameTableSeat DealerSeat { get { return this.tableseats[CardGame.DealerSeatNumber]; } }

		/// <summary>
		/// Return next active seat from table, or first active seat if argumnet is null
		/// Dealer seat is excluded from this search. If no active seats null is returned
		/// </summary>
		public CardGameTableSeat NextActiveSeat(CardGameTableSeat startseat = null) {
			int seat = 0;
			CardGameTableSeat returnseat = null;
			while (true) {
				if ((seat != CardGame.DealerSeatNumber) && (this.tableseats[seat] != null) && (this.tableseats[seat].Active))
				{
					if (returnseat == startseat) { returnseat = this.tableseats[seat]; break; }
					if (startseat == this.tableseats[seat]) returnseat = this.tableseats[seat];
				}
				seat++;
				if (seat == this.tableseats.Length) {
					if (returnseat == null) break;
					seat = 0;
				}
			}
			return returnseat;
		}

		public void AddPotTokens(int tokens) {
			TokenPot += tokens;
		}

		public int CollectPotTokens() {
			int tokens = TokenPot;
			TokenPot = 0;
			return tokens;
		}

		public int TokenPot { get; private set; }

		// Statistics
		public int RoundsPlayed { get; set; }


		protected ICardGameDealer carddealer = null; 
		CardGameTableSeat[] tableseats = null;
	}

}
