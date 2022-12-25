using System.Collections.Generic;

namespace Games.Card
{
	public interface ICardGameTable
	{


		/// <summary>
		/// Complete one round and return
		/// </summary>
		void PlayRound();


		/// <summary>
		/// return total number of table seats
		/// </summary>
		int SeatCount { get; }


		/// <summary>
		/// retunr number of players at table
		/// </summary>
		int PlayerCount { get; }


		/// <summary>
		/// return number of active players, participate in current round
		/// </summary>
		int ActiveSeatCount { get; }


		/// <summary>
		/// Set a cumstom statistics object
		/// </summary>
		ICardGameTable Statistics(ICardGameTableStatistics statistics);


		/// <summary>
		/// Return statistics object
		/// </summary>
		/// <returns></returns>
		ICardGameTableStatistics GetStatistics();


		/// <summary>
		/// Return Table seat for Dealer
		/// </summary>
		ICardGameTableSeat DealerSeat { get; }


		/// <summary>
		/// Return Player table seats, dealer seat excluded
		/// </summary>
		IEnumerable<ICardGameTableSeat> TableSeats { get; }


		/// <summary>
		/// Return Table pot
		/// </summary>
		ITokenWallet TablePot { get; }


		/// <summary>
		/// Let a player join the table if there is a free seat.
		/// Return the seat number
		/// </summary>
		int JoinTable(CardPlayer player);


		/// <summary>
		/// Will free seat number
		/// </summary>
		/// <param name="seat"></param>
		void LeaveTable(int seat);


		/// <summary>
		/// Return next active table seat, or first active seat if argumnet is null
		/// Dealer seat is excluded from this search. If no active seats null is returned
		/// </summary>
		ICardGameTableSeat NextActiveSeat(ICardGameTableSeat startseat = null);



	}
}