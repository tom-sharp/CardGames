using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{


	public static class TestRank
	{
		public static void Run() {
			var rankit = new TexasRankOn5Cards();
			var cards = new PlayCards();
			int linebr = 0;


			Console.Write($"\n Rank of Two cards\n");
			for (int i = 2; i < 15; i++)
			{
				for (int j = 2; j < 15; j++)
				{
					cards.Add(new PlayCardHeart(i));
					cards.Add(new PlayCardDiamond(j));
					cards.RankCards(rankit);
					Console.Write($"{cards.First().Symbol},{cards.Next().Symbol},{cards.RankSignature.Rank,-10}\n");
					cards.Clear();
					linebr++;
					//					if (linebr % 5 == 0) Console.Write("\n");
				}

				for (int j = 2; j < 15; j++)
				{
					cards.Add(new PlayCardHeart(i));
					cards.Add(new PlayCardSpade(j));
					cards.RankCards(rankit);
					Console.Write($"{cards.First().Symbol},{cards.Next().Symbol},{cards.RankSignature.Rank,-10}\n");
					cards.Clear();
				}

				for (int j = 2; j < 15; j++)
				{
					cards.Add(new PlayCardHeart(i));
					cards.Add(new PlayCardClub(j));
					cards.RankCards(rankit);
					Console.Write($"{cards.First().Symbol},{cards.Next().Symbol},{cards.RankSignature.Rank,-10}\n");
					cards.Clear();
				}

				for (int j = 2; j < 15; j++)
				{
					cards.Add(new PlayCardHeart(i));
					cards.Add(new PlayCardHeart(j));
					cards.RankCards(rankit);
					Console.Write($"{cards.First().Symbol},{cards.Next().Symbol},{cards.RankSignature.Rank,-10}\n");
					cards.Clear();
				}
			}

			for (int i = 2; i < 15; i++)
			{
				for (int j = 2; j < 15; j++)
				{
					cards.Add(new PlayCardDiamond(i));
					cards.Add(new PlayCardDiamond(j));
					cards.RankCards(rankit);
					Console.Write($"{cards.First().Symbol},{cards.Next().Symbol},{cards.RankSignature.Rank,-10}\n");
					cards.Clear();
					linebr++;
					//					if (linebr % 5 == 0) Console.Write("\n");
				}

				for (int j = 2; j < 15; j++)
				{
					cards.Add(new PlayCardDiamond(i));
					cards.Add(new PlayCardSpade(j));
					cards.RankCards(rankit);
					Console.Write($"{cards.First().Symbol},{cards.Next().Symbol},{cards.RankSignature.Rank,-10}\n");
					cards.Clear();
				}

				for (int j = 2; j < 15; j++)
				{
					cards.Add(new PlayCardDiamond(i));
					cards.Add(new PlayCardClub(j));
					cards.RankCards(rankit);
					Console.Write($"{cards.First().Symbol},{cards.Next().Symbol},{cards.RankSignature.Rank,-10}\n");
					cards.Clear();
				}
			}

			for (int i = 2; i < 15; i++)
			{
				for (int j = 2; j < 15; j++)
				{
					cards.Add(new PlayCardSpade(i));
					cards.Add(new PlayCardSpade(j));
					cards.RankCards(rankit);
					Console.Write($"{cards.First().Symbol},{cards.Next().Symbol},{cards.RankSignature.Rank,-10}\n");
					cards.Clear();
				}

				for (int j = 2; j < 15; j++)
				{
					cards.Add(new PlayCardSpade(i));
					cards.Add(new PlayCardClub(j));
					cards.RankCards(rankit);
					Console.Write($"{cards.First().Symbol},{cards.Next().Symbol},{cards.RankSignature.Rank,-10}\n");
					cards.Clear();
				}
			}

			for (int i = 2; i < 15; i++)
			{
				for (int j = 2; j < 15; j++)
				{
					cards.Add(new PlayCardClub(i));
					cards.Add(new PlayCardClub(j));
					cards.RankCards(rankit);
					Console.Write($"{cards.First().Symbol},{cards.Next().Symbol},{cards.RankSignature.Rank,-10}\n");
					cards.Clear();
				}
			}

			return;




		}
	}
}
