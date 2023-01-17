using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm.Data
{

	public class TexasDbContext : DbContext
	{

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDb; Initial Catalog=TexasHoldEm",
			options => options.MaxBatchSize(1000));
		}


		public DbSet<TexasStatisticsEntity> TexasHands { get; set; }
		public DbSet<TexasPlayRoundEntity> PlayRounds { get; set; }
		public DbSet<TexasPlayerHandEntity> PlayRoundHands { get; set; }


	}
}
