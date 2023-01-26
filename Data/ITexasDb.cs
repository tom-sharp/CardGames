using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Card.TexasHoldEm.Models;


namespace Games.Card.TexasHoldEm.Data
{
	public interface ITexasDb
	{

		ITexasAiDb AiDb { get; }

		bool AddHand(TexasStatisticsEntity hand);

		bool AddPlayRound(TexasPlayRoundEntity playround);

		Task<TexasStatisticsEntity> GetHandAsync(int id);

		Task<IEnumerable<TexasStatisticsEntity>> GetHandsAsync(bool winhand);
		Task<IEnumerable<TexasStatisticsEntity>> GetHandsAsync(bool winhand, byte card1, byte card2);

		int SaveChanges();
		void MigrateDb();
		void DeleteDb();
		bool ConnectDb();

	}
}
