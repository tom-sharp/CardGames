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
			texastable = new TexasHoldEmTable(new CardTableConfig() { Seats = Settings.TableSeats, MaxBetLimit = Settings.MaxBetLimit, MaxBetRaises = Settings.MaxBetRaises });
			texastable.Join(new TexasHoldEmPlayerDealer(new CardPlayerConfig() {Name = "Common cards", Tokens = Settings.Tokens }, UI));
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
