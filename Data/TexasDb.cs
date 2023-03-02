using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Card.TexasHoldEm.Models;


namespace Games.Card.TexasHoldEm.Data
{
	public class TexasDb : ITexasDb
	{

		public TexasDb(TexasDbContext ctx)
		{
			this.db = ctx;
			this.aidb = new TexasAiDb(ctx);
		}

		public ITexasAiDb AiDb => this.aidb;


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
				((card1 == hand.PrivateCard1 && card2 == hand.PrivateCard2) || 
				(card2 == hand.PrivateCard2 && card1 == hand.PrivateCard1))).ToListAsync();
		}


		public bool AddPlayRound(TexasPlayRoundEntity playround)
		{
			if (playround == null) return false;
			db.PlayRounds.Add(playround);
			return true;
		}


		public int SaveChanges()
		{
			int res;
			try
			{
				res = this.db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException ex) {

				Console.WriteLine($"Error: Concurr {ex.Message}");
				res = -1;
			}
			catch (DbUpdateException ex)
			{
				Console.WriteLine($"Error: Update {ex.Message}");
				res = -1;
			}

			return res;
		}

		public bool MigrateDb() {
			try 
			{
				this.db.Database.Migrate();
			}
			catch 
			{ 
				return false; 
			}
			return true;
		}

		public bool DeleteDb()
		{
			try
			{
				return this.db.Database.EnsureDeleted();
			}
			catch 
			{ 
				return false;
			}
		}

		public bool ConnectDb()
		{
			return this.db.Database.CanConnect();
		}

		ITexasAiDb aidb;
		readonly TexasDbContext db;

	}
}
