using System.Collections.Generic;

namespace Games.Card
{
	public interface ICardGameTable
	{

		void Run(int rounds = 1);

		ICardGameTable Statistics(ICardGameTableStatistics statistics);

		ICardGameTableStatistics GetStatistics();


		ICardGameTableSeat DealerSeat { get; }

		IEnumerable<ICardGameTableSeat> TableSeats { get; }

		ITokenWallet TablePot { get; }

		int JoinTable(CardPlayer player);

		void LeaveTable(int seat);
		ICardGameTableSeat NextActiveSeat(ICardGameTableSeat startseat = null);

		int ActiveSeatCount { get; }
		int SeatCount { get; }
		int PlayerCount { get; }


	}
}