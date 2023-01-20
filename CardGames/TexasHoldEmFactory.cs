using Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Models;
using Syslib.Games;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
	class TexasHoldEmFactory
	{
		public TexasHoldEmFactory(ITexasHoldEmIO UI, ITexasDb DB, ITexasHoldEmAi AI)
		{
			this.IO = UI;
			this.AI = AI;
			this.DB = DB;
			this.settings = null;
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
					texastable.Statistics(new TexasHoldEmStatistics(this.DB, this.AI));
				}
				else { 
					texastable.Statistics(new TexasHoldEmStatistics(null, this.AI));
				}
			}

			if (settings.Quiet) texastable.SleepTime = -1;
			else texastable.SleepTime = settings.SleepTime;


			texastable.Join(new TexasHoldEmPlayerDealer(new CardPlayerConfig() { Tokens = settings.Tokens }, this.IO));


			int count = 0;
			while (++count <= settings.Players)
			{

				if (count == 5) texastable.Join(TexasHumanPlayer(name: "Human", settings));
				else if (count == 1) texastable.Join(TexasCallRobotPlayer(name: $"CallRobot", settings));
				else if (count == 2) texastable.Join(TexasRaiseRobotPlayer(name: $"RaiseRobot", settings));
				else if (count == 3) texastable.Join(TexasRandomRobotPlayer(name: $"RndRobot", settings));
				else if (count == 4) texastable.Join(TexasRobotPlayer(name: $"Robot {count}", settings));
				else texastable.Join(TexasAIPlayer(name: $"Ai {count}", settings));



//				else if (count == 2) texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RaiseRobot", Tokens = settings.Tokens, PlayerProfile = AlwaysRaiseProfile }));
//				else if (count == 3) texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RndRobot", Tokens = settings.Tokens, PlayerProfile = RandomProfile}));
//				else if (count == 6) texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RndRobot", Tokens = settings.Tokens, PlayerProfile = AlwaysCallProfile}));
//				else if (count == 8) texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RndRobot", Tokens = settings.Tokens, PlayerProfile = AlwaysCallProfile}));
//				else texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"Robot{count}", Tokens = settings.Tokens, PlayerProfile = NeutralProfile }));
//				else  texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"RndRobot", Tokens = settings.Tokens, PlayerProfile = AlwaysCallProfile}));

			}



			return texastable;
		}


		public enum PlayerProfileType { Neutral, Random, AlwaysCall, AlwaysRaise }


		public GamePlayerProfile Profile(PlayerProfileType profiletype)
		{
			switch (profiletype) {
				case PlayerProfileType.Neutral: return new GamePlayerProfile() { Randomness = 0, Defensive = 0, Offensive = 0 };
				case PlayerProfileType.Random: return new GamePlayerProfile() { Randomness = 100, Defensive = 10, Offensive = 10 };
				case PlayerProfileType.AlwaysCall: return new GamePlayerProfile() { Randomness = 100, Defensive = 0, Offensive = 0 };
				case PlayerProfileType.AlwaysRaise: return new GamePlayerProfile() { Randomness = 100, Defensive = 0, Offensive = 100 };
			}
			return new GamePlayerProfile() { Randomness = 0, Defensive = 0, Offensive = 0 };
		}


		public ITexasHoldEmPlayer GamePlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerHuman(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens }, this.IO);
		}



		public ITexasHoldEmPlayer TexasHumanPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerHuman(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens }, this.IO);
		}

		public ITexasHoldEmPlayer TexasCallRobotPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens, PlayerProfile = Profile(PlayerProfileType.AlwaysCall) });
		}

		public ITexasHoldEmPlayer TexasRaiseRobotPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens, PlayerProfile = Profile(PlayerProfileType.AlwaysRaise) });
		}

		public ITexasHoldEmPlayer TexasRandomRobotPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens, PlayerProfile = Profile(PlayerProfileType.Random) });
		}

		public ITexasHoldEmPlayer TexasRobotPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens, PlayerProfile = Profile(PlayerProfileType.Neutral) });
		}


		public ITexasHoldEmPlayer TexasAIPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerAi(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens, PlayerProfile = Profile(PlayerProfileType.Neutral) }, this.AI);
		}


		readonly TexasHoldEmSettings settings;
		readonly ITexasHoldEmIO IO;
		readonly ITexasHoldEmAi AI;
		readonly ITexasDb DB;

	}
}
