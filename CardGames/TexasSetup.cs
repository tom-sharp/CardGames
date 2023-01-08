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
		public TexasSetup(ITexasHoldEmIO UI)
		{
			this.IO = UI;
		}


		public ICardTable TexasTable(TexasSettings settings)
		{
			CardTable texastable;
			this.IO.ShowProgressMessage($"Playing {settings.RoundsToPlay} rounds with {settings.Players} players having {settings.Tokens} tokens each at table with {settings.TableSeats} seats ");
			this.IO.SupressOutput = settings.Quiet;
			this.IO.SupressOverrideRoundSummary = settings.QuietNotSummary;
			this.IO.SupressOverrideStatistics = settings.QuietNotStatistics;


			texastable = new TexasHoldEmTable(new CardTableConfig() { Seats = settings.TableSeats });

			if (settings.EnableStatistics) texastable.Statistics(new TexasHoldEmStatistics());
			if (settings.Quiet) texastable.SleepTime = -1;

			texastable.Join(new TexasHoldEmPlayerDealer(new CardPlayerConfig() { Tokens = settings.Tokens }, this.IO));

			int count = 0;
			while (++count <= settings.Players)
			{

				if (count == 5) texastable.Join(new TexasHoldEmPlayerHuman(new CardPlayerConfig() { Name = $"Human", Tokens = settings.Tokens }, IO));
				else texastable.Join(new TexasHoldEmPlayerRobot(new CardPlayerConfig() { Name = $"Player{count}", Tokens = settings.Tokens }));
				//				if (count == 2) playerlist.Add(new CardPlayerRobot(name: $"Player{count} rnd", tokens, new GamePlayerProfileRandom()));
				////				else if (count == 3) playerlist.Add(new CardPlayerHuman(name: $"Human", new TokenWallet(tokens: tokens)));
				//				else playerlist.Add(new CardPlayerRobot(name: $"Player{count}", tokens));

			}


			return texastable;
		}


		ITexasHoldEmIO IO;
		

	}
}
