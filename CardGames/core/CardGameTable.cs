using System.Collections.Generic;


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
			this.TablePot = new TokenWallet();

			this.tableseats[CardGame.DealerSeatNumber].Join(new CardPlayerDealer());

		}


		public void PlayRound()
		{
			if (this.carddealer == null) return;
			if (this.carddealer.DealRound()) this.statistics.RoundsPlayed++;
		}


		public int SeatCount { get { return this.tableseats.Length; } }


		public int PlayerCount
		{
			get
			{
				int count = 0;
				foreach (var seat in this.tableseats) { if (!seat.IsFree) count++; }
				return count - 1;   // remove dealer count
			}
		}


		public int ActiveSeatCount
		{
			get
			{
				int count = 0;
				foreach (var seat in this.tableseats) { if ((seat != null) && (seat.IsActive)) count++; }
				return count - 1;   // remove dealer count
			}
		}


		public int JoinTable(CardPlayer player)
		{
			int seat = 1;
			if (player == null) return 0;
			while (seat < tableseats.Length)
			{
				if (tableseats[seat].IsFree)
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


		public ITokenWallet TablePot { get; }


		public abstract ICardGameTable Statistics(ICardGameTableStatistics statistics);

		public ICardGameTableStatistics GetStatistics() { return this.statistics; }



		public ICardGameTableSeat DealerSeat { get { return this.tableseats[CardGame.DealerSeatNumber]; } }


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
		/// Return next active seat from table, or first active seat if argumnet is null
		/// Dealer seat is excluded from this search. If no active seats null is returned
		/// </summary>
		public ICardGameTableSeat NextActiveSeat(ICardGameTableSeat startseat = null)
		{
			int seat = 0, activeseats = 0;
			ICardGameTableSeat returnseat = null;
			while (true)
			{
				if ((seat != CardGame.DealerSeatNumber) && (this.tableseats[seat] != null))
				{
					if (this.tableseats[seat].IsActive) activeseats++;
					if ((returnseat == startseat) && (this.tableseats[seat].IsActive)) { returnseat = this.tableseats[seat]; break; }
					if (startseat == this.tableseats[seat]) returnseat = startseat;
				}
				seat++;
				if (seat == this.tableseats.Length)
				{
					if (activeseats == 0) returnseat = null;
					if (returnseat == null) break;
					seat = 0;
				}
			}
			return returnseat;
		}



		protected ICardGameTableStatistics statistics;
		protected ICardGameDealer carddealer;
		ICardGameTableSeat[] tableseats;
	}

}
