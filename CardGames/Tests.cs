using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Games.Card;
using Games.Card.TexasHoldEm;

namespace Games.Card.Test
{
	public static class Tests
	{

		public static int RunTests() {
			int result = 0;

			Console.WriteLine("\n---------------------------------------------- \n Running Tests..");

			result += RunHankRankTest();
			result += RunDeckTests();
			result += RunStackTests();

			Console.WriteLine("\n");
			if (result == 0) Console.WriteLine("\n All Tests Success"); else Console.WriteLine($"\n Tests result in {result} Error(s)");
			Console.WriteLine("----------------------------------------------");
			return result;
		}

		public static int RunDeckTests() {
			int result = 0;

			var deck = new CardDeck(jokers: 2);
			Console.Write($"\nNew Card deck with 2 jokers ");
			if (deck.CardsTotal != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (deck.CardsLeft != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");

			Card card = deck.NextCard();
			while (card != null) { Console.Write($"{card.Symbol} "); card = deck.NextCard(); }
			if (deck.CardsTotal != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (deck.CardsLeft != 0) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");


			deck.ShuffleCards();
			Console.Write($"\nShuffled deck with 2 jokers ");
			if (deck.CardsTotal != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (deck.CardsLeft != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");

			card = deck.NextCard();
			while (card != null) { Console.Write($"{card.Symbol} "); card = deck.NextCard(); }
			if (deck.CardsTotal != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (deck.CardsLeft != 0) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");


			deck.SortCards();
			Console.Write($"\nSorted deck with 2 jokers ");
			if (deck.CardsTotal != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (deck.CardsLeft != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");

			card = deck.NextCard();
			while (card != null) { Console.Write($"{card.Symbol} "); card = deck.NextCard(); }
			if (deck.CardsTotal != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (deck.CardsLeft != 0) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");


			return result;
		}


		public static int RunStackTests()
		{
			int result = 0;

			var stack = new CardStack(decks: 5, new CardDeck(jokers: 2));
			Console.Write($"\nNew Stack of cards based on 5 Decks with 2 jokers ");
			if (stack.CardsTotal != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (stack.CardsLeft != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");

			Card card = stack.NextCard();
			while (card != null) { Console.Write($"{card.Symbol} "); card = stack.NextCard(); }
			if (stack.CardsTotal != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (stack.CardsLeft != 0) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");


			stack.ShuffleCards();
			Console.Write($"\nShuffled Stack with 10 jokers ");
			if (stack.CardsTotal != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (stack.CardsLeft != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");

			card = stack.NextCard();
			while (card != null) { Console.Write($"{card.Symbol} "); card = stack.NextCard(); }
			if (stack.CardsTotal != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (stack.CardsLeft != 0) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");


			stack.SortCards();
			Console.Write($"\nSorted stack with 10 jokers ");
			if (stack.CardsTotal != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (stack.CardsLeft != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");

			card = stack.NextCard();
			while (card != null) { Console.Write($"{card.Symbol} "); card = stack.NextCard(); }
			if (stack.CardsTotal != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (stack.CardsLeft != 0) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");


			return result;
		}



		public static int RunHankRankTest() {
			int result = 0;

			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<Card>();

			playerhand.Clear();
			playerhand.Add(new Card(CardSuite.Heart, 13));
			playerhand.Add(new Card(CardSuite.Heart, 12));
			playerhand.Add(new Card(CardSuite.Heart, 10));
			playerhand.Add(new Card(CardSuite.Heart, 14));
			playerhand.Add(new Card(CardSuite.Heart, 11));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.RoyalStraightFlush) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new Card(CardSuite.Club, 3));
			playerhand.Add(new Card(CardSuite.Club, 4));
			playerhand.Add(new Card(CardSuite.Club, 6));
			playerhand.Add(new Card(CardSuite.Club, 7));
			playerhand.Add(new Card(CardSuite.Club, 5));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.StraightFlush) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new Card(CardSuite.Club, 12));
			playerhand.Add(new Card(CardSuite.Club, 2));
			playerhand.Add(new Card(CardSuite.Club, 6));
			playerhand.Add(new Card(CardSuite.Club, 9));
			playerhand.Add(new Card(CardSuite.Club, 5));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.Flush) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new Card(CardSuite.Club, 3));
			playerhand.Add(new Card(CardSuite.Heart, 4));
			playerhand.Add(new Card(CardSuite.Club, 6));
			playerhand.Add(new Card(CardSuite.Spade, 7));
			playerhand.Add(new Card(CardSuite.Diamond, 5));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.Straight) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new Card(CardSuite.Club, 3));
			playerhand.Add(new Card(CardSuite.Heart, 4));
			playerhand.Add(new Card(CardSuite.Club, 6));
			playerhand.Add(new Card(CardSuite.Spade, 3));
			playerhand.Add(new Card(CardSuite.Diamond, 5));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.Pair) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new Card(CardSuite.Club, 3));
			playerhand.Add(new Card(CardSuite.Heart, 4));
			playerhand.Add(new Card(CardSuite.Club, 3));
			playerhand.Add(new Card(CardSuite.Spade, 3));
			playerhand.Add(new Card(CardSuite.Diamond, 5));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.ThreeOfAKind) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new Card(CardSuite.Club, 3));
			playerhand.Add(new Card(CardSuite.Heart, 4));
			playerhand.Add(new Card(CardSuite.Club, 3));
			playerhand.Add(new Card(CardSuite.Spade, 3));
			playerhand.Add(new Card(CardSuite.Diamond, 3));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.FourOfAKind) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new Card(CardSuite.Club, 3));
			playerhand.Add(new Card(CardSuite.Heart, 4));
			playerhand.Add(new Card(CardSuite.Club, 2));
			playerhand.Add(new Card(CardSuite.Spade, 3));
			playerhand.Add(new Card(CardSuite.Diamond, 2));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.TwoPair) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new Card(CardSuite.Club, 13));
			playerhand.Add(new Card(CardSuite.Heart, 8));
			playerhand.Add(new Card(CardSuite.Club, 8));
			playerhand.Add(new Card(CardSuite.Spade, 13));
			playerhand.Add(new Card(CardSuite.Diamond, 8));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.FullHouse) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");


			playerhand.Clear();
			playerhand.Add(new Card(CardSuite.Club, 13));
			playerhand.Add(new Card(CardSuite.Heart, 8));
			playerhand.Add(new Card(CardSuite.Club, 3));
			playerhand.Add(new Card(CardSuite.Spade, 11));
			playerhand.Add(new Card(CardSuite.Diamond, 4));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.HighCard) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			return result;
		}
	}
}
