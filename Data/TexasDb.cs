using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasDb : ITexasDb
	{

		public TexasDb(TexasDbContext ctx)
		{
			this.db = ctx;
		}

		public bool AddHand(TexasStatisticsEntity texashand)
		{
			if (texashand == null) return false;
			db.TexasHands.Add(texashand);
			return true;
		}

		public async Task<TexasStatisticsEntity> GetHandAsync(int id)
		{
			return await db.TexasHands.AsNoTracking().FirstOrDefaultAsync(hand => hand.Id == id);
		}



		public async Task<IEnumerable<TexasStatisticsEntity>> GetHandsAsync(bool winhand)
		{
			return await db.TexasHands.AsNoTracking().Where(hand => hand.Win == winhand).ToListAsync();
		}

		public async Task<IEnumerable<TexasStatisticsEntity>> GetHandsAsync(bool winhand, byte card1, byte card2)
		{
			return await db.TexasHands.AsNoTracking().Where(hand => hand.Win == winhand && 
				((card1 == hand.CardsSignature[0] && card2 == hand.CardsSignature[1]) || 
				(card2 == hand.CardsSignature[1] && card1 == hand.CardsSignature[0]))).ToListAsync();
		}



		public int SaveChanges()
		{
			int res =0;
			try
			{
				res = this.db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException ex) {

				Console.WriteLine($"Error: Concurr {ex.Message}");
			}
			catch (DbUpdateException ex)
			{
				Console.WriteLine($"Error: Update {ex.Message}");
			}

			// ADD ERROR HANDLE FOR UPDATE ERROR

			return res;
		}

		public void MigrateDb() {
			this.db.Database.Migrate();
		}


		readonly TexasDbContext db = null;

	}
}
