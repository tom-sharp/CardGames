using Syslib;
using Syslib.Games;
using Games.Card.TexasHoldEm.Models;
using Syslib.Games.Card.TexasHoldEm;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmStatistics : GameStatistics
	{
		public TexasHoldEmStatistics(ITexasDb db, ITexasHoldEmAi ai)
		{
			this.db = db;
			this.ai = ai;

			playrounds = new CList<TexasPlayRoundEntity>();
		}

		public void StatsAddRound(TexasPlayRoundEntity round)
		{
			if (round == null) return;
			this.playrounds.Add(round);
		}


		public int SaveToDb(TexasPlayRoundEntity round)
		{
			if ((round == null) || (this.db == null)) return 0;
			if (QueToDb > 10) { SaveDb(); QueToDb = 0; }
			this.db.AddPlayRound(round);
			return ++QueToDb;
		}


		public int SaveDb()
		{
			if (this.db == null) return 0;
			return this.db.SaveChanges();
		}


		public CList<TexasPlayRoundEntity> PlayRounds { get { return this.playrounds; } }


		readonly CList<TexasPlayRoundEntity> playrounds;
		readonly ITexasDb db;
		readonly ITexasHoldEmAi ai;
		int QueToDb = 0;

	}
}
