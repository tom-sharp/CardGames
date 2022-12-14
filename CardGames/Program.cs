using System;
using Games.Card;

namespace CardGames
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("CardGames");
			var carddeck = new CardDeck();
			Card card = null;
			while (true) {
				card = carddeck.NextCard();
				if (card == null) break;
				Console.Write($"{card.Symbol}  ");
			}
			Console.WriteLine("\nShuffle...");
			carddeck.ShuffleCards();
			while (true)
			{
				card = carddeck.NextCard();
				if (card == null) break;
				Console.Write($"{card.Symbol}  ");
			}
			Console.WriteLine("\nEnd");
		}
	}
}
