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
using Games.Card.TexasHoldEm.Data;

namespace CardGames
{
	internal static class Factory
	{

		public static void Setup(ITexasHoldEmUI ui, ITexasDb db, ITexasHoldEmAi ai, ITexasHoldEmSettings settings)
		{
			UI = ui;
			AI = ai;
			DB = db;
			Settings = settings;
		}


		public static IPlayer Player(GamePlayerType type, IGamePlayerProfile profile) {
			switch (type) {
				case GamePlayerType.Ai: return new Player() { Type = GamePlayerType.Ai , Tokens = Settings.Tokens };
				case GamePlayerType.Human: return new Player() { Type = GamePlayerType.Human, Tokens = Settings.Tokens };
				case GamePlayerType.Default: return new Player() { Type = GamePlayerType.Default, Tokens = Settings.Tokens };
			}
			return new Player() { Type = GamePlayerType.Invalid };
		}



		public static IGamePlayerProfile PlayerProfile() => new GamePlayerProfile();
		public static IGamePlayerProfile PlayerProfile(string name, int defensive, int offensive, int randomness, int bluffer) => new GamePlayerProfile(name, defensive, offensive, randomness, bluffer);
		public static IGamePlayerProfile PlayerProfileBalanced() => new GamePlayerProfile("Balanced", defensive: 0, offensive: 0, randomness: 0, bluffer: 0);
		public static IGamePlayerProfile PlayerProfileRandom() => new GamePlayerProfile("Random", defensive: 10, offensive:10, randomness: 100, bluffer: 0);
		public static IGamePlayerProfile PlayerProfileAlwaysCall() => new GamePlayerProfile("AlwaysCall", defensive: 0, offensive: 0, randomness: 100, bluffer: 0);
		public static IGamePlayerProfile PlayerProfileAlwaysRaise() => new GamePlayerProfile("AlwaysRaise", defensive: 0, offensive: 100, randomness: 100, bluffer: 0);
		public static IGamePlayerProfile PlayerProfileOffensive() => new GamePlayerProfile("Offensive", defensive: 0, offensive: 5, randomness: 0, bluffer: 0);
		public static IGamePlayerProfile PlayerProfileDefensive() => new GamePlayerProfile("Defensive", defensive: 5, offensive: 0, randomness: 0, bluffer: 0);



		public static ITexasHoldEmPlayer TexasPlayer(IPlayer player)
		{
			switch (player.Type)
			{
				case GamePlayerType.Ai: return new TexasHoldEmPlayerAi(AI, player);
				case GamePlayerType.Human: return new TexasHoldEmPlayerHuman(UI, player);
			}
			return null;
		}


		public static TexasHoldEmTable TexasTable()
		{
			var texastable = new TexasHoldEmTable(Settings);
			texastable.Join(new TexasHoldEmPlayerDefault(texastable, Settings, UI));

			int players = 0;
			while (players++ < Settings.Players)
			{
				IPlayer player = new Player() { Tokens = Settings.Tokens };
				if (players == 1) { player.Type = GamePlayerType.Human; player.Name = "Human"; }
				else if (players == 5) { player.Type = GamePlayerType.Ai; player.PlayerProfile = Factory.PlayerProfileDefensive(); player.Name = $"Ai{players} (def)"; }
				else if (players == 6) { player.Type = GamePlayerType.Ai; player.PlayerProfile = Factory.PlayerProfileOffensive(); player.Name = $"Ai{players} (off)"; }
				else { player.Type = GamePlayerType.Ai; player.PlayerProfile = Factory.PlayerProfileBalanced(); player.Name = $"Ai{players} (bal)";  }

				texastable.Join(Factory.TexasPlayer(player));

			}
			return texastable;
		}



		static ITexasHoldEmSettings Settings;
		static ITexasHoldEmUI UI;
		static ITexasHoldEmAi AI;
		static ITexasDb DB;

	}
}
