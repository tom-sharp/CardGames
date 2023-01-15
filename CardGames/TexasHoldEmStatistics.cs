using Syslib;
using Syslib.Games;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmStatistics : GameStatistics
	{
		public TexasHoldEmStatistics(ITexasDb db)
		{
			this.db = db;
			allhands = new CList<TexasStatisticsEntity>();
		}

		public void StatsAddHand(TexasStatisticsEntity hand)
		{
			if (hand == null) return;
			this.allhands.Add(hand);
		}

		public int SaveToDb(TexasStatisticsEntity hand) {
			if ((hand == null) || (this.db == null)) return 0;
			if (QueToDb > 10) {	SaveDb(); QueToDb = 0; }
			this.db.AddHand(hand);
			return ++QueToDb;
		}

		public int SaveDb()
		{
			if (this.db == null) return 0;
			return this.db.SaveChanges();
		}


		public CList<TexasStatisticsEntity> AllHands { get { return this.allhands; } }

		readonly CList<TexasStatisticsEntity> allhands;
		readonly ITexasDb db;
		int QueToDb = 0;

	}
}
