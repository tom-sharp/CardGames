using Syslib;
using Syslib.Games.Card;
using System;
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

			var deck = new PlayCardDeck(jokers: 2);
			Console.Write($"\nNew Card deck with 2 jokers ");
			if (deck.CardsTotal != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (deck.CardsLeft != 54) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");

			var card = deck.NextCard();
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

			var stack = new PlayCardStack(decks: 5, new PlayCardDeck(jokers: 2));
			Console.Write($"\nNew Stack of cards based on 5 Decks with 2 jokers ");
			if (stack.CardsTotal != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (stack.CardsLeft != 270) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");

			var card = stack.NextCard();
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
			var playerhand = new CList<IPlayCard>();

			playerhand.Clear();
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(12));
			playerhand.Add(new PlayCardHeart(10));
			playerhand.Add(new PlayCardHeart(14));
			playerhand.Add(new PlayCardHeart(11));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.RoyalStraightFlush) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardClub(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(7));
			playerhand.Add(new PlayCardClub(5));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.StraightFlush) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(12));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(9));
			playerhand.Add(new PlayCardClub(5));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.Flush) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(7));
			playerhand.Add(new PlayCardDiamond(5));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.Straight) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.Pair) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.ThreeOfAKind) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(3));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.FourOfAKind) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(2));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.TwoPair) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(8));
			playerhand.Add(new PlayCardSpade(13));
			playerhand.Add(new PlayCardDiamond(8));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.FullHouse) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(11));
			playerhand.Add(new PlayCardDiamond(4));
			rank.RankHand(playerhand);
			if (rank.Hand != TexasHoldEmHand.HighCard) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			return result;
		}
	}
}
