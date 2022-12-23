using System.Collections.Generic;

namespace Games.Card
{
	public interface ICardGameTable
	{

		void Run(int rounds = 1);

		ICardGameTable Statistics(ICardGameTableStatistics statistics);

		public ICardGameTableStatistics GetStatistics();


		CardGameTableSeat DealerSeat { get; }

		IEnumerable<CardGameTableSeat> TableSeats { get; }


		int JoinTable(CardPlayer player);

		void LeaveTable(int seat);


	}
}