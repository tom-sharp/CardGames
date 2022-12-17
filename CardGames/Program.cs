using System;
using Games.Card;

namespace CardGames
{
	class Program
	{
		static void Main(string[] args)
		{
			var carddeck = new CardDeck();
			Card card = null;

			Console.WriteLine("CardGames - A Deck of Cards");
			Console.WriteLine($"Deck Total cards = {carddeck.CardsTotal} Undrawn = {carddeck.CardsLeft}");
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
			Console.WriteLine($"Deck Total cards = {carddeck.CardsTotal} Undrawn = {carddeck.CardsLeft}");

			var cardstack = new CardStack(decks: 5);
			Console.WriteLine("CardGames - A Stack of 5 decks");
			Console.WriteLine($"Stack Total cards = {cardstack.CardsTotal} Undrawn = {cardstack.CardsLeft}");
			while (true)
			{
				card = cardstack.NextCard();
				if (card == null) break;
				Console.Write($"{card.Symbol}  ");
			}
			Console.WriteLine("\nShuffle...");
			cardstack.ShuffleCards();
			while (true)
			{
				card = cardstack.NextCard();
				if (card == null) break;
				Console.Write($"{card.Symbol}  ");
			}
			Console.WriteLine("\nEnd");
			Console.WriteLine($"Stack Total cards = {cardstack.CardsTotal} Undrawn = {cardstack.CardsLeft}");

			// TEXAS
			var texastable = new TexasHoldEmTable(tableseats: 8);
			texastable.JoinTable(new CardPlayer(name: "Player1", tokens: 1000));
			texastable.JoinTable(new CardPlayer(name: "Player2", tokens: 1000));
			texastable.JoinTable(new CardPlayer(name: "Player3", tokens: 1000));
			texastable.JoinTable(new CardPlayer(name: "Player4", tokens: 1000));
			texastable.JoinTable(new CardPlayer(name: "Player5", tokens: 1000));
			texastable.LeaveTable(seat: 2);
			texastable.Run();


		}
	}
}
