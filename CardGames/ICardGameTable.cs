using System.Collections.Generic;

namespace Games.Card
{
	public interface ICardGameTable
	{

		void Run(int rounds = 1);

		ICardGameTable Statistics(ICardGameTableStatistics statistics);

		public ICardGameTableStatistics GetStatistics();


		ICardGameTableSeat DealerSeat { get; }

		IEnumerable<ICardGameTableSeat> TableSeats { get; }

		public ITokenWallet TablePot { get; }

		int JoinTable(CardPlayer player);

		void LeaveTable(int seat);
		ICardGameTableSeat NextActiveSeat(ICardGameTableSeat startseat = null);
		public int ActiveSeatCount { get; }
		public int SeatCount { get; }


	}
}