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
	class TexasSetup
	{
		public TexasSetup(ITexasHoldEmIO UI, ITexasDb db)
		{
			this.IO = UI;
			this.DB = db;
		}


		public ICardTable TexasTable(TexasSettings settings)
		{
			CardTable texastable;
			this.IO.ShowMsg($"Playing {settings.RoundsToPlay} rounds with {settings.Players} players having {settings.Tokens} tokens each at table with {settings.TableSeats} seats ");
			this.IO.SupressOutput = settings.Quiet;
			this.IO.SupressOverrideRoundSummary = settings.QuietNotSummary;
			this.IO.SupressOverrideStatistics = settings.QuietNotStatistics;


			texastable = new TexasHoldEmTable(new CardTableConfig() { Seats = settings.TableSeats });

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

			int count = 0;
			while (++count <= settings.Players)
			{

				if (count == 5) texastable.Join(new TexasHoldEmPlayerHuman(new CardPlayerConfig() { Name = $"Human", Tokens = settings.Tokens }, IO));
				else if (count == 2) texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RaiseRobot", Tokens = settings.Tokens, PlayerProfile = AlwaysRaiseProfile }));
				else if (count == 3) texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RndRobot", Tokens = settings.Tokens, PlayerProfile = RandomProfile}));
				else texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"CallRobot{count}", Tokens = settings.Tokens, PlayerProfile = AlwaysCallProfile}));
//				if (count == 2) playerlist.Add(new CardPlayerRobot(name: $"Player{count} rnd", tokens, new GamePlayerProfileRandom()));
				////				else if (count == 3) playerlist.Add(new CardPlayerHuman(name: $"Human", new TokenWallet(tokens: tokens)));
				//				else playerlist.Add(new CardPlayerRobot(name: $"Player{count}", tokens));

			}


			return texastable;
		}


		ITexasHoldEmIO IO;
		ITexasDb DB;

	}
}
