using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syslib;

namespace Games.Card
{
	public abstract class CardGameHandRank : ICardGameHandRank
	{

		// PokerHand (returned rank value):
		//		Rank value using Bit 0-14 + bit 15-27 (the higher rank the better hand)
		//		Rank value is used only to evaluate wich hand is best of two of the same kind (Ex pair/pair).
		//		Rank value can not be used to evaluate the better hand of different types (Ex pair/flush)
		//		NOTE! joker are not supported in this ranking
		//		bit 0-14 indicate High card Value
		//		bit 15-27 indicate branded value such as pair of 9 is higher than pair of 3

		public abstract void RankHand(CList<Card> cards);


		protected int IsRoyalStraightFlush(CList<Card> cards)
		{
			// Higest of all rank: required 10, J, Q, K, A in hearts - no need for individual value rank
			if (cards.FirstOrDefault(c => c.Suite == CardSuite.Heart && c.Rank == 10) == null) return 0;
			if (cards.FirstOrDefault(c => c.Suite == CardSuite.Heart && c.Rank == 11) == null) return 0;
			if (cards.FirstOrDefault(c => c.Suite == CardSuite.Heart && c.Rank == 12) == null) return 0;
			if (cards.FirstOrDefault(c => c.Suite == CardSuite.Heart && c.Rank == 13) == null) return 0;
			if (cards.FirstOrDefault(c => c.Suite == CardSuite.Heart && c.Rank == 14) == null) return 0;
			return 1;
		}


		protected int IsStraightFlush(CList<Card> cards) { return 0; }
		protected int IsFourOfAKind(CList<Card> cards) { return 0; }
		protected int IsFullHouse(CList<Card> cards) { return 0; }

		protected int IsFlush(CList<Card> cards) {
			int value = 0, rank = 0, count = 0;
			CardSuite suite = CardSuite.Blank;

			cards.Sort(SortCardsOnSuiteRankFunc);

			foreach (var card in cards)
			{
				value |= (0x0001 << card.Rank);
				if (suite == card.Suite) { rank |= (0x0001 << card.Rank); if (++count == 5) suite = CardSuite.Blank; }
				else { if (count < 5) { suite = card.Suite; count = 1; rank = (0x0001 << card.Rank); } }
			}
			if (count == 5) { rank <<= 13; value |= rank; } else value = 0;


			if (value > 0)
			{
				Console.Write("\n Flush Rank (sorted): ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write($"  Value = {value} (top cards + flush )\n");
			}


			return value;
		}

		protected int IsStraight(CList<Card> cards) {
			int value = 0, rank = 0, count = 0;

			cards.Sort(SortCardsOnRankFunc);

			foreach (var card in cards)
			{
				value |= (0x0001 << card.Rank);
				if (rank == card.Rank) { if (++count == 5) rank += 13; else rank = card.Rank - 1; } 
				else { if (rank < 15) { rank = card.Rank - 1; count = 1; } }
			}
			if (rank > 14) { value |= (0x0001 << rank); } else value = 0;


			if (value > 0)
			{
				Console.Write("\n Straight Rank (sorted): ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write($"  Value = {value} (top cards + straight )\n");
			}


			return value;
		}


		protected int IsThreeOfAKind(CList<Card> cards) {
			int value = 0, rank = 0, count = 0;

			cards.Sort(SortCardsOnRankFunc);

			foreach (var card in cards)
			{
				value |= (0x0001 << card.Rank);
				if (rank == card.Rank) { if (++count == 3) rank += 13; } 
				else { if (rank < 15) { rank = card.Rank; count = 1; } }
			}
			if (rank > 14) { value |= (0x0001 << rank); } else value = 0;


			if (value > 0)
			{
				Console.Write("\n ThreeOfAKind Rank (sorted): ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write($"  Value = {value} (top cards + threeofakind )\n");
			}


			return value;
		}



		protected int IsTwoPair(CList<Card> cards)
		{
			// using bit 0-14 to rank high card and bit and bit 15-27 to rank pair.
			int value = 0, pair1 = 0, pair2 = 0;

			cards.Sort(SortCardsOnRankFunc);

			foreach (var card in cards)
			{
				value |= (0x0001 << card.Rank);
				if (pair1 > 14)
				{
					if ((pair2 == card.Rank) && (pair2 < 15)) { pair2 += 13; } 
					else { if (pair2 < 15) pair2 = card.Rank; }
				}
				if ((pair1 == card.Rank) && (pair1 < 15)) { pair1 += 13; } 
				else { if (pair1 < 15) pair1 = card.Rank; }
			}

			if ((pair1 > 14) && (pair2 > 14))
			{
				value |= (0x0001 << pair1);
				value |= (0x0001 << pair2);
			}
			else value = 0;

			if (value > 0)
			{
				Console.Write("\n TwoPair Rank (sorted): ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write($"  Value = {value} (top cards + two pair)\n");
			}


			return value;
		}

		protected int IsPair(CList<Card> cards)
		{
			int value = 0, rank = 0, count = 0;

			cards.Sort(SortCardsOnRankFunc);

			foreach (var card in cards)
			{
				value |= (0x0001 << card.Rank);
				if (rank == card.Rank) { if (++count == 2) rank += 13; } 
				else { if (rank < 15) { rank = card.Rank; count = 1; } }
			}
			if (rank > 14) { value |= (0x0001 << rank); } else value = 0;




			if (value > 0)
			{
				Console.Write("\n Pair Rank (sorted): ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write($"  Value = {value} (top cards + pair )\n");
			}


			return value;
		}

		protected Int64 IsHighCard(CList<Card> cards)
		{
			Int64 value = RankCards(cards);

			cards.Sort(SortCardsOnRankFunc);

			if (value > 0)
			{
				Console.Write("\n High Card Rank (sorted): ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write($"  Value = {value} (top cards)\n");
			}

			return value;
		}

		// Return the card rank value of a set of cards.
		// Jokers are supported and ranks as Ace. Support up to 7 cards of the same rank
		// 14 - 1 * 3 = 13 *3 = 39.40.41 42
		private Int64 RankCards(CList<Card> cards) {
			Int64 rank = 0;
			if (cards == null) return rank;
			foreach (var card in cards) { 
				if ((card != null) && (card.Rank > 0) && (card.Rank < 15)) {
					if (card.Rank > 0) rank += (0x00000001 << ((card.Rank - 1) * 3));
					else if (card.Suite == CardSuite.Joker) rank += (0x00000001 << 39);	// jokers are ranked as ace
				}
			}
			return rank;
		}

		// Return the Hand rank value based on card rank value (card rank expected to be 2-14)
		private Int64 RankHand(int cardrank) {
			Int64 rank = 0;
			if ((cardrank > 0) && (cardrank < 15)) rank = (0x00000001 << (((cardrank - 1) * 3) + handbitstart));
			return rank;
		}



		private bool SortCardsOnRankFunc(Card card1, Card card2)
		{
			if (card1.Rank < card2.Rank) return true;
			return false;
		}

		private bool SortCardsOnSuiteRankFunc(Card card1, Card card2)
		{
			if (card1.Suite < card2.Suite) return true;
			if (card1.Rank < card2.Rank) return true;
			return false;
		}


		int handbitstart = 42;


	}
}
