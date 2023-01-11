using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public interface ITexasDb
	{

		public bool AddHand(TexasStatisticsEntity hand);

		public Task<TexasStatisticsEntity> GetHandAsync(int id);

		public Task<IEnumerable<TexasStatisticsEntity>> GetHandsAsync(bool winhand);
		public Task<IEnumerable<TexasStatisticsEntity>> GetHandsAsync(bool winhand, byte card1, byte card2);
		int SaveChanges();

		void MigrateDb();
	}
}
