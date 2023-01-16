using Games.Card.TexasHoldEm;
using Syslib.Games;
using Syslib.Games.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
	class TexasHoldEmFactory
	{
		public TexasHoldEmFactory(ITexasHoldEmIO UI, ITexasDb db)
		{
			this.IO = UI;
			this.DB = db;
		}


		public TexasHoldEmTable TexasTable(ITexasHoldEmSettings settings)
		{
			TexasHoldEmTable texastable;
			this.IO.ShowMsg($"Playing {settings.RoundsToPlay} rounds with {settings.Players} players having {settings.Tokens} tokens each at table with {settings.TableSeats} seats ");
			this.IO.SupressOutput = settings.Quiet;
			this.IO.SupressOverrideRoundSummary = settings.QuietNotSummary;
			this.IO.SupressOverrideStatistics = settings.QuietNotStatistics;


			texastable = new TexasHoldEmTable(new CardTableConfig() { Seats = settings.TableSeats, MaxBetLimit = settings.MaxBetLimit, MaxBetRaises = settings.MaxBetRaises });

			if (settings.EnableStatistics) {
				if (settings.UseDb)
				{
					if (this.DB != null) this.DB.MigrateDb();
					texastable.Statistics(new TexasHoldEmStatistics(this.DB));
				}
				else { 
					texastable.Statistics(new TexasHoldEmStatistics(null));
				}
			}

			if (settings.Quiet) texastable.SleepTime = -1;
			else texastable.SleepTime = settings.SleepTime;


			texastable.Join(new TexasHoldEmPlayerDealer(new CardPlayerConfig() { Tokens = settings.Tokens }, this.IO));


			var AlwaysCallProfile = new GamePlayerProfile() { Randomness = 100, Defensive = 0, Offensive = 0 };
			var AlwaysRaiseProfile = new GamePlayerProfile() { Randomness = 100, Defensive = 0, Offensive = 100 };
			var RandomProfile = new GamePlayerProfile() { Randomness = 100, Defensive = 10, Offensive = 10 };
			var NeutralProfile = new GamePlayerProfile() { Randomness = 0, Defensive = 0, Offensive = 0 };

			int count = 0;
			while (++count <= settings.Players)
			{

				if (count == 5) texastable.Join(new TexasHoldEmPlayerHuman(new CardPlayerConfig() { Name = $"Human", Tokens = settings.Tokens }, IO));
//				else if (count == 2) texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RaiseRobot", Tokens = settings.Tokens, PlayerProfile = AlwaysRaiseProfile }));
//				else if (count == 3) texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RndRobot", Tokens = settings.Tokens, PlayerProfile = RandomProfile}));
//				else if (count == 6) texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RndRobot", Tokens = settings.Tokens, PlayerProfile = AlwaysCallProfile}));
//				else if (count == 8) texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RndRobot", Tokens = settings.Tokens, PlayerProfile = AlwaysCallProfile}));
//				else texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"Robot{count}", Tokens = settings.Tokens, PlayerProfile = NeutralProfile }));
				else  texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RndRobot", Tokens = settings.Tokens, PlayerProfile = AlwaysCallProfile}));

			}




			return texastable;
		}

		readonly ITexasHoldEmIO IO;
		readonly ITexasDb DB;

	}
}
