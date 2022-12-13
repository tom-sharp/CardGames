using System;
using Games.Card;

namespace CardGames
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("CardGames");
			var carddeck = new Deck();
			Card card = null;
			while (true) {
				card = carddeck.Next();
				if (card == null) break;
				Console.Write($"{card.Symbol}  ");
			}
			Console.WriteLine("\nShuffle...");
			carddeck.Shuffle();
			while (true)
			{
				card = carddeck.Next();
				if (card == null) break;
				Console.Write($"{card.Symbol}  ");
			}
			Console.WriteLine("\nEnd");
		}
	}
}
