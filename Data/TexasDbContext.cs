﻿using Games.Card.TexasHoldEm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syslib.Games.Card.TexasHoldEm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm.Data
{

	public class TexasDbContext : DbContext
	{
		public TexasDbContext()
		{
		}
		public TexasDbContext(DbContextOptions<TexasDbContext> ctxoption) : base(ctxoption)
		{	
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDb; Initial Catalog=TexasHoldEm-statistics-ai-game",
			options => options.MaxBatchSize(1000)
			);
			
		}

		public DbSet<TexasStatisticsEntity> TexasHands { get; set; }
		public DbSet<TexasPlayRoundEntity> PlayRounds { get; set; }
		public DbSet<TexasPlayerHandEntity> PlayRoundHands { get; set; }

		public DbSet<TexasHoldEmAiEntity> TexasAI { get; set; }


	}
}
