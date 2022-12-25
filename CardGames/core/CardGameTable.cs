﻿using System;
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
	public abstract class CardGameTable : ICardGameTable
	{
		public CardGameTable(CardGameTableConfig tableConfig)
		{
			if ((tableConfig != null) && (tableConfig.IsValid())) this.tableseats = new CardGameTableSeat[tableConfig.Seats + 1];
			else this.tableseats = new CardGameTableSeat[1];

			for (int seat = 0; seat < this.tableseats.Length; seat++) this.tableseats[seat] = new CardGameTableSeat();

			this.statistics = new CardGameTableStatistics();
			this.carddealer = null;
			this.cardplayer = null;
			this.TablePot = new TokenWallet();

			this.tableseats[CardGame.DealerSeatNumber].Join(new CardPlayer(name: "Dealer"));

		}


		public void Run(int rounds = 1)
		{
			int roundsplayed = 0;
			if (this.carddealer == null) return;
			while (true)
			{
				if (this.carddealer.DealRound())
				{
					this.statistics.RoundsPlayed++;
					roundsplayed++;
				}
				else break;
				if (roundsplayed >= rounds) break;
			}
		}

		public abstract ICardGameTable Statistics(ICardGameTableStatistics statistics);

		public ICardGameTableStatistics GetStatistics() { return this.statistics; }

		public int JoinTable(CardPlayer player)
		{
			int seat = 1;
			if (player == null) return 0;
			while (seat < tableseats.Length)
			{
				if (tableseats[seat].IsFree())
				{
					if (!tableseats[seat].Join(player)) return 0;
					return seat;
				}
				seat++;
			}
			return 0;
		}


		public void LeaveTable(int seat)
		{
			if ((seat < 1) || (seat >= tableseats.Length)) return;     // do not remove dealer
			tableseats[seat].Leave();
		}



		/// <summary>
		/// return all the table seats as long there is a player at the seat. Dealer excluded
		/// </summary>
		public IEnumerable<ICardGameTableSeat> TableSeats
		{
			get
			{
				int seat = 0;
				while (seat < this.tableseats.Length)
				{
					if ((seat != CardGame.DealerSeatNumber) && (this.tableseats[seat] != null))
					{
						yield return this.tableseats[seat];
					}
					seat++;
				}
			}
		}

		/// <summary>
		/// return number of active player seats (dealer seat is excluded from count of active seats)
		/// </summary>
		public int ActiveSeatCount
		{
			get
			{
				int count = 0;
				foreach (var seat in this.tableseats) { if ((seat != null) && (seat.IsActive)) count++; }
				return count - 1;   // remove dealer count
			}
		}

		public int SeatCount { get { return this.tableseats.Length; } }

		public int PlayerCount 
		{ 
			get 
			{
				int count = 0;
				foreach (var seat in this.tableseats) { if (!seat.IsFree()) count++; }
				return count - 1;   // remove dealer count
			}
		}

		// return Dealer seat
		public ICardGameTableSeat DealerSeat { get { return this.tableseats[CardGame.DealerSeatNumber]; } }

		/// <summary>
		/// Return next active seat from table, or first active seat if argumnet is null
		/// Dealer seat is excluded from this search. If no active seats null is returned
		/// </summary>
		public ICardGameTableSeat NextActiveSeat(ICardGameTableSeat startseat = null)
		{
			int seat = 0;
			ICardGameTableSeat returnseat = null;
			while (true)
			{
				if ((seat != CardGame.DealerSeatNumber) && (this.tableseats[seat] != null) && (this.tableseats[seat].IsActive))
				{
					if (returnseat == startseat) { returnseat = this.tableseats[seat]; break; }
					if (startseat == this.tableseats[seat]) returnseat = this.tableseats[seat];
				}
				seat++;
				if (seat == this.tableseats.Length)
				{
					if (returnseat == null) break;
					seat = 0;
				}
			}
			return returnseat;
		}


		public ITokenWallet TablePot { get; }

		protected ICardGameTableStatistics statistics;
		protected ICardGameDealer carddealer;
		protected ICardGamePlayer cardplayer;
		ICardGameTableSeat[] tableseats;
	}

}