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


		protected Int64 IsRoyalStraightFlush(CList<Card> cards)
		{
			// Higest of all rank: required 10, J, Q, K, A in hearts - no need for individual value rank
			if (cards.FirstOrDefault(c => c.Suite == CardSuite.Heart && c.Rank == 10) == null) return 0;
			if (cards.FirstOrDefault(c => c.Suite == CardSuite.Heart && c.Rank == 11) == null) return 0;
			if (cards.FirstOrDefault(c => c.Suite == CardSuite.Heart && c.Rank == 12) == null) return 0;
			if (cards.FirstOrDefault(c => c.Suite == CardSuite.Heart && c.Rank == 13) == null) return 0;
			if (cards.FirstOrDefault(c => c.Suite == CardSuite.Heart && c.Rank == 14) == null) return 0;
			return 1;
		}


		protected Int64 IsStraightFlush(CList<Card> cards) {
			Int64 value = 0;
			int count = 0, flush1 = 0, flush2 = 0, flush3 = 0, flush4 = 0, flush5 = 0;
			CardSuite suite = CardSuite.Blank;

			if (cards == null) return value;
			cards.Sort(SortCardsOnSuiteRankFunc);

			foreach (var card in cards)	{
				if (suite == card.Suite) {
					count++;
					switch (count) {
						case 1: flush1 = card.Rank; break;
						case 2: flush2 = card.Rank; if (flush2 != flush1 - 1) { flush1 = card.Rank; count = 1; } break;
						case 3: flush3 = card.Rank; if (flush3 != flush2 - 1) { flush1 = card.Rank; count = 1; } break;
						case 4: flush4 = card.Rank; if (flush4 != flush3 - 1) { flush1 = card.Rank; count = 1; } break;
						case 5: flush5 = card.Rank; if (flush5 != flush4 - 1) { flush1 = card.Rank; count = 1; flush5 = 0; } break;
					}
					if (count == 5) break;
				}
				else { suite = card.Suite; flush1 = card.Rank; count = 1; }
			}
			if (flush5 > 0) value = RankCards(cards) | RankHand(flush1) | RankHand(flush2) | RankHand(flush3) | RankHand(flush4) | RankHand(flush5);



			if (value > 0)
			{
				Console.Write($"\n Straight Flush Rank (sorted): Value = {value,10}  ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write("\n");
			}


			return value;
		}

		protected Int64 IsFourOfAKind(CList<Card> cards) {
			Int64 value = RankCards(cards);
			Int64 four = RankFourOfAKind(value);

			if (four > 0) value |= four; else value = 0;



			if (value > 0)
			{
				cards.Sort(SortCardsOnRankFunc);
				Console.Write($"\n FourOfAKind Rank (sorted): Value = {value,10}  ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write("\n");
			}


			return value; 
		}


		protected Int64 IsFullHouse(CList<Card> cards) {
			Int64 value = RankCards(cards);
			Int64 fullhouse = RankFullHouse(value);

			if (fullhouse > 0) value |= fullhouse; else value = 0;



			if (value > 0)
			{
				cards.Sort(SortCardsOnRankFunc);
				Console.Write($"\n Full House Rank (sorted): Value = {value,10}  ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write("\n");
			}


			return value; 
		}


		protected Int64 IsFlush(CList<Card> cards) {
			Int64 value = 0;
			int count = 0, flush1 = 0, flush2 = 0, flush3 = 0, flush4 = 0, flush5 = 0;
			CardSuite suite = CardSuite.Blank;

			if (cards == null) return value;
			cards.Sort(SortCardsOnSuiteRankFunc);

			foreach (var card in cards) {
				if (suite == card.Suite) {
					count++;
					switch (count) {
						case 1: flush1 = card.Rank; break;
						case 2: flush2 = card.Rank; break;
						case 3: flush3 = card.Rank; break;
						case 4: flush4 = card.Rank; break;
						case 5: flush5 = card.Rank; break;
					}
					if (count == 5) break;
				}
				else { suite = card.Suite; flush1 = card.Rank; count = 1; }
			}
			if (flush5 > 0) value = RankCards(cards) | RankHand(flush1) | RankHand(flush2) | RankHand(flush3) | RankHand(flush4) | RankHand(flush5);



			if (value > 0)
			{
				Console.Write($"\n Flush Rank (sorted): Value = {value,10}  ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write("\n");
			}


			return value;
		}

		protected Int64 IsStraight(CList<Card> cards) {
			Int64 value = RankCards(cards);
			Int64 straight = RankStraight(value);

			if (straight > 0) value |= straight; else value = 0;



			if (value > 0)
			{
				cards.Sort(SortCardsOnRankFunc);
				Console.Write($"\n Straight Rank (sorted): Value = {value,10}  ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write("\n");
			}


			return value;
		}


		protected Int64 IsThreeOfAKind(CList<Card> cards) {
			Int64 value = RankCards(cards);
			Int64 three = RankThreeOfAKind(value);

			if (three > 0) value |= three; else value = 0;



			if (value > 0)
			{
				cards.Sort(SortCardsOnRankFunc);
				Console.Write($"\n ThreeOfAKind Rank (sorted): Value = {value,10}  ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write("\n");
			}


			return value;
		}



		protected Int64 IsTwoPair(CList<Card> cards)
		{
			Int64 value = RankCards(cards);
			Int64 twopair = RankTwoPair(value);

			if (twopair > 0) value |= twopair; else value = 0;



			if (value > 0)
			{
				cards.Sort(SortCardsOnRankFunc);
				Console.Write($"\n TwoPair Rank (sorted):      Value = {value,10}  ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write("\n");
			}


			return value;
		}

		protected Int64 IsPair(CList<Card> cards)
		{
			Int64 value = RankCards(cards);
			Int64 pair = RankPair(value);

			if (pair > 0) value |= pair; else value = 0;



			if (value > 0)
			{
				cards.Sort(SortCardsOnRankFunc);
				Console.Write($"\n Pair Rank (sorted):         Value = {value,10}  ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write("\n");
			}

			return value;
		}

		protected Int64 IsHighCard(CList<Card> cards)
		{
			Int64 value = RankCards(cards);



			if (value > 0)
			{
				cards.Sort(SortCardsOnRankFunc);
				Console.Write($"\n High Card Rank (sorted):    Value = {value,10}  ");
				foreach (var c in cards) { Console.Write($"  {c.Symbol}"); }
				Console.Write("\n");
			}

			return value;
		}





		// Return the card rank value of a set of cards.
		// Jokers are ranked as rank value set. Support up to 7 cards of the same rank
		private Int64 RankCards(CList<Card> cards) {
			Int64 rank = 0, cardrank = 0;
			if (cards == null) return rank;
			foreach (var card in cards) {
				if ((card != null) && (card.Suite != CardSuite.Blank)) {
					cardrank = 0x00000001;
					if ((card.Rank > 0) && (card.Rank < 15)) {
						cardrank <<= card.Rank * 3;
						rank += cardrank;
					}
					else if (card.Suite == CardSuite.Joker) rank += cardrank;
				}
			}
			return rank;
		}



		// find highest four of same kind of cards and return Hand Rank Value
		private Int64 RankFourOfAKind(Int64 cardrank)
		{
			Int64 mask = 0, mask7 = 0x00000007;
			int rank = 14, four = 0;

			while (rank > 0)
			{
				mask = (mask7 << (rank * 3)) & cardrank;
				mask = (mask >> (rank * 3)) & 0x00000007;
				if (mask > 3) { four = rank; break; }
				rank--;
			}
			if (four > 0) mask = RankHand(four);
			else mask = 0;
			return mask;
		}




		// find highest full house (3+2) and return Hand Rank Value
		private Int64 RankFullHouse(Int64 cardrank)
		{
			Int64 mask = 0, mask7 = 0x00000007;
			int rank = 14, three = 0, pair = 0;

			while (rank > 0) {
				mask = (mask7 << (rank * 3)) & cardrank;
				mask = (mask >> (rank * 3)) & 0x00000007;
				if (mask > 1) {
					if ((mask > 2) && (three == 0)) { three = rank; if (pair > 0) break; }
					else if (pair == 0) { pair = rank; if (three > 0) break; }
				}
				rank--;
			}
			if ((three > 0) && (pair > 0)) mask = RankHand(three) | RankHand(pair);
			else mask = 0;
			return mask;
		}


		// find highest straight of cards and return Hand Rank Value
		private Int64 RankStraight(Int64 cardrank)
		{
			Int64 mask = 0, mask7 = 0x00000007;
			int rank = 14, count = 0, straight = 0;

			while (rank > 0) {
				mask = (mask7 << (rank * 3)) & cardrank;
				mask = (mask >> (rank * 3)) & 0x00000007;
				if (mask > 0) { count++; if (count == 5) { straight = rank; break; } }
				else count = 0;
				rank--;
			}
			if (straight > 0) mask = RankHand(straight);
			else mask = 0;
			return mask;
		}



		// find highest three of same kind of cards and return Hand Rank Value
		private Int64 RankThreeOfAKind(Int64 cardrank) {
			Int64 mask = 0, mask7 = 0x00000007;
			int rank = 14, three = 0;

			while (rank > 0) {
				mask = (mask7 << (rank * 3)) & cardrank;
				mask = (mask >> (rank * 3)) & 0x00000007;
				if (mask > 2) { three = rank; break; }
				rank--;
			}
			if (three > 0) mask = RankHand(three);
			else mask = 0;
			return mask;
		}



		// find highest 2 pair of cards and return Hand Rank Value
		private Int64 RankTwoPair(Int64 cardrank) {
			Int64 mask = 0, mask7 = 0x00000007;
			int rank = 14, pair1 = 0, pair2 = 0;

			while (rank > 0) {
				mask = (mask7 << (rank * 3)) & cardrank;
				mask = (mask >> (rank * 3)) & 0x00000007;
				if (mask > 1) {
					if (pair1 == 0) pair1 = rank;
					else { pair2 = rank; break; }
				}
				rank--;
			}
			if (pair2 > 0) { mask = RankHand(pair1) | RankHand(pair2); }
			else mask = 0;
			return mask;
		}


		// find highest pair of cards and return Hand Rank Value
		private Int64 RankPair(Int64 cardrank) {
			Int64 mask = 0, mask7 = 0x00000007;
			int rank = 14, pair = 0;

			while (rank > 0) {
				mask = (mask7 << (rank * 3)) & cardrank;
				mask = (mask >> (rank * 3)) & 0x00000007;
				if (mask > 1) { pair = rank; break; }
				rank--;
			}
			if (pair > 0) mask = RankHand(pair); 
			else mask = 0;
			return mask;
		}

		// Return the Hand rank value based on card rank value (card rank expected to be 1-14, or 0 is returned)
		private Int64 RankHand(int cardrank) {
			int handrankstartbit = 45;
			Int64 rank = 1;
			if ((cardrank > 0) && (cardrank < 15))
			{
				rank <<= (cardrank - 1) + handrankstartbit;
			}
			else rank = 0;
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
			if ((card1.Suite == card2.Suite) && (card1.Rank < card2.Rank)) return true;
			return false;
		}




	}
}
