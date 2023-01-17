using Syslib;
using Syslib.Games;
using Games.Card.TexasHoldEm.Data;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmStatistics : GameStatistics
	{
		public TexasHoldEmStatistics(ITexasDb db)
		{
			this.db = db;
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


		//public int SaveToDb(TexasStatisticsEntity hand) {
		//	if ((hand == null) || (this.db == null)) return 0;
		//	if (QueToDb > 10) {	SaveDb(); QueToDb = 0; }
		//	this.db.AddHand(hand);
		//	return ++QueToDb;
		//}

		public int SaveDb()
		{
			if (this.db == null) return 0;
			return this.db.SaveChanges();
		}


		public CList<TexasPlayRoundEntity> PlayRounds { get { return this.playrounds; } }

//		public CList<TexasStatisticsEntity> AllHands { get { return this.allhands; } }
//		readonly CList<TexasStatisticsEntity> allhands;

		readonly CList<TexasPlayRoundEntity> playrounds;
		readonly ITexasDb db;
		int QueToDb = 0;

	}
}
