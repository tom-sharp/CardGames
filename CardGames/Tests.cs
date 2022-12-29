using Syslib;
using Syslib.Games.Card;
using System;
using Games.Card;
using Games.Card.TexasHoldEm;

namespace Games.Card.Test
{
	public class Tests
	{

		public int RunTests() {
			int result = 0;

			Console.WriteLine("\n---------------------------------------------- \n Running Tests..");

			result += RunHankRankTest();
			result += RunDeckTests();
			result += RunStackTests();
			result += RunJokerTests();

			Console.WriteLine("\n");
			if (result == 0) Console.WriteLine("\n All Tests Success"); else Console.WriteLine($"\n Tests result in {result} Error(s)");
			Console.WriteLine("----------------------------------------------");
			return result;
		}


		public int RunJokerTests() {
			int result = 0;

			IPlayCard joker = new PlayCardJoker();
			Console.WriteLine($"\nJoker test: Rank {joker.Rank}, Suite {joker.Suite}, Symbol {joker.Symbol} ");
			if (joker.Rank != 0) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (joker.Suite != PlayCardSuite.Joker) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (joker.Symbol != "**") { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			(joker as IPlayCardJoker).Rank = 14;
			(joker as IPlayCardJoker).Suite = PlayCardSuite.Heart;
			Console.WriteLine($"\nJoker test: Rank {joker.Rank}, Suite {joker.Suite}, Symbol {joker.Symbol} ");
			if (joker.Rank != 14) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (joker.Suite != PlayCardSuite.Heart) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (joker.Symbol != "**") { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			(joker as IPlayCardJoker).Rank = 0;
			(joker as IPlayCardJoker).Suite = PlayCardSuite.Joker;
			Console.WriteLine($"\nJoker test: Rank {joker.Rank}, Suite {joker.Suite}, Symbol {joker.Symbol} ");
			if (joker.Rank != 0) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (joker.Suite != PlayCardSuite.Joker) { Console.Write(" Fail "); result++; } else Console.Write(" OK ");
			if (joker.Symbol != "**") { Console.Write(" Fail "); result++; } else Console.Write(" OK ");

			return result;
		}

		public int RunDeckTests() {
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


		public int RunStackTests()
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



		public int RunHankRankTest() {
			int result = 0;

			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();

			playerhand.Clear();
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(12));
			playerhand.Add(new PlayCardHeart(10));
			playerhand.Add(new PlayCardHeart(14));
			playerhand.Add(new PlayCardHeart(11));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;
			if (hand1.Id != TexasHoldEmHand.RoyalStraightFlush) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardClub(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(7));
			playerhand.Add(new PlayCardClub(5));
			var hand2 = rank.RankHand(playerhand) as ITexasHandRank;
			if (hand2.Id != TexasHoldEmHand.StraightFlush) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");
			if (hand1.Value <= hand2.Value) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(3));
			var hand3 = rank.RankHand(playerhand) as ITexasHandRank;
			if (hand3.Id != TexasHoldEmHand.FourOfAKind) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");
           if (hand2.Value <= hand3.Value) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(8));
			playerhand.Add(new PlayCardSpade(13));
			playerhand.Add(new PlayCardDiamond(8));
			var hand4 = rank.RankHand(playerhand) as ITexasHandRank;
			if (hand4.Id != TexasHoldEmHand.FullHouse) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");
           if (hand3.Value <= hand4.Value) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(12));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(9));
			playerhand.Add(new PlayCardClub(5));
			var hand5 = rank.RankHand(playerhand) as ITexasHandRank;
			if (hand5.Id != TexasHoldEmHand.Flush) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");
			if (hand4.Value <= hand5.Value) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(7));
			playerhand.Add(new PlayCardDiamond(5));
			var hand6 = rank.RankHand(playerhand) as ITexasHandRank;
			if (hand6.Id != TexasHoldEmHand.Straight) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");
			if (hand5.Value <= hand6.Value) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			var hand7 = rank.RankHand(playerhand) as ITexasHandRank;
			if (hand7.Id != TexasHoldEmHand.ThreeOfAKind) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");
			if (hand6.Value <= hand7.Value) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(2));
			var hand8 = rank.RankHand(playerhand) as ITexasHandRank;
			if (hand8.Id != TexasHoldEmHand.TwoPair) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");
			if (hand7.Value <= hand8.Value) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			var hand9 = rank.RankHand(playerhand) as ITexasHandRank;
			if (hand9.Id != TexasHoldEmHand.Pair) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");
			if (hand8.Value <= hand9.Value) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(11));
			playerhand.Add(new PlayCardDiamond(4));
			var hand10 = rank.RankHand(playerhand) as ITexasHandRank;
			if (hand10.Id != TexasHoldEmHand.HighCard) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");
			if (hand9.Value <= hand10.Value) { Console.Write(" Failed "); result++; } else Console.Write(" OK ");

			return result;
		}
	}
}
