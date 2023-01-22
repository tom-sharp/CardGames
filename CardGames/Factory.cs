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
	internal static class Factory
	{

		public static void Setup(ITexasHoldEmIO ui, ITexasDb db, ITexasHoldEmAi ai, ITexasHoldEmSettings settings)
		{
			UI = ui;
			AI = ai;
			DB = db;
			Settings = settings;
		}


		public static TexasHoldEmTable TexasTable()
		{
			TexasHoldEmTable texastable;
			UI.ShowMsg($"Playing {Settings.RoundsToPlay} rounds with {Settings.Players} players having {Settings.Tokens} tokens each at table with {Settings.TableSeats} seats ");
			UI.SupressOutput = Settings.Quiet;
			UI.SupressOverrideRoundSummary = Settings.QuietNotSummary;
			UI.SupressOverrideStatistics = Settings.QuietNotStatistics;


			texastable = new TexasHoldEmTable(new CardTableConfig() { Seats = Settings.TableSeats, MaxBetLimit = Settings.MaxBetLimit, MaxBetRaises = Settings.MaxBetRaises });

			if (Settings.EnableStatistics) {
				if (Settings.UseDb)
				{
					texastable.Statistics(new TexasHoldEmStatistics(DB , AI));
				}
				else { 
					texastable.Statistics(new TexasHoldEmStatistics(null, AI));
				}
			}

			if (Settings.Quiet) texastable.SleepTime = -1;
			else texastable.SleepTime = Settings.SleepTime;


			texastable.Join(new TexasHoldEmPlayerDealer(new CardPlayerConfig() { Tokens = Settings.Tokens }, UI));


			int count = 0;
			while (++count <= Settings.Players)
			{

				if (count == 5) texastable.Join(TexasHumanPlayer(name: "Human", Settings));
				else if (count == 1) texastable.Join(TexasCallRobotPlayer(name: $"CallRobot", Settings));
				else if (count == 2) texastable.Join(TexasRaiseRobotPlayer(name: $"RaiseRobot", Settings));
				else if (count == 3) texastable.Join(TexasRandomRobotPlayer(name: $"RndRobot", Settings));
				else if (count == 4) texastable.Join(TexasBalancedRobotPlayer(name: $"Robot {count}", Settings));
				else texastable.Join(TexasAIPlayer(name: $"Ai {count}", Settings));



			}



			return texastable;
		}


		public enum PlayerProfileType { Balanced, Random, AlwaysCall, AlwaysRaise }


		public static GamePlayerProfile Profile(PlayerProfileType profiletype)
		{
			switch (profiletype) {
				case PlayerProfileType.Balanced: return new GamePlayerProfile() { Randomness = 0, Defensive = 0, Offensive = 0 };
				case PlayerProfileType.Random: return new GamePlayerProfile() { Randomness = 100, Defensive = 10, Offensive = 10 };
				case PlayerProfileType.AlwaysCall: return new GamePlayerProfile() { Randomness = 100, Defensive = 0, Offensive = 0 };
				case PlayerProfileType.AlwaysRaise: return new GamePlayerProfile() { Randomness = 100, Defensive = 0, Offensive = 100 };
			}
			return new GamePlayerProfile() { Randomness = 0, Defensive = 0, Offensive = 0 };
		}


		public static ITexasHoldEmPlayer GamePlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerHuman(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens }, UI);
		}



		public static ITexasHoldEmPlayer TexasHumanPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerHuman(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens }, UI);
		}

		public static ITexasHoldEmPlayer TexasCallRobotPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens, PlayerProfile = Profile(PlayerProfileType.AlwaysCall) });
		}

		public static ITexasHoldEmPlayer TexasRaiseRobotPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens, PlayerProfile = Profile(PlayerProfileType.AlwaysRaise) });
		}

		public static ITexasHoldEmPlayer TexasRandomRobotPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens, PlayerProfile = Profile(PlayerProfileType.Random) });
		}

		public static ITexasHoldEmPlayer TexasBalancedRobotPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens, PlayerProfile = Profile(PlayerProfileType.Balanced) });
		}


		public static ITexasHoldEmPlayer TexasAIPlayer(string name, ITexasHoldEmSettings settings)
		{
			return new TexasHoldEmPlayerAi(new CardPlayerConfig() { Name = name, Tokens = settings.Tokens, PlayerProfile = Profile(PlayerProfileType.Balanced) }, AI);
		}


		static ITexasHoldEmSettings Settings;
		static ITexasHoldEmIO UI;
		static ITexasHoldEmAi AI;
		static ITexasDb DB;

	}
}
