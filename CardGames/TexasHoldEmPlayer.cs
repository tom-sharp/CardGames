using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class TexasHoldEmPlayer : CardGamePlayer, ICardGamePlayer
	{
		public TexasHoldEmPlayer(CardGameTable gametable)
		{
			this.gametable = gametable;
		}



		public void Reset()
		{
		}


		public void Run()
		{

		}

		//  return highest TexasHoldEm Rank of hand cards. if there is no cards 0 is returned
		// rank goes from 2 (high card) up to roysal straight flush
		// NOTE! Jokers are not supported here
		public TexasHoldEmRank RankHand(CList<Card> cards) {
			int value = 0;
			if ((cards == null) || (cards.Count() == 0)) return new TexasHoldEmRank(TexasHoldEmHand.Nothing, 0);

			if ((value = IsRoyalStraightFlush(cards)) > 0) return new TexasHoldEmRank(TexasHoldEmHand.RoyalStraightFlush, value);

			
			
			// CODE HERE - More evaluations



			if ((value = IsHighCard(cards)) > 0) return new TexasHoldEmRank(TexasHoldEmHand.HighCard, value);
			return new TexasHoldEmRank(TexasHoldEmHand.Nothing, 0);

		}

		private int IsRoyalStraightFlush(CList<Card> cards) {
			// Higest of all rank: required 10, J, Q, K, A in hearts - no need for individual value rank
			bool match = false;
			foreach (var card in cards) if ((card.Suite == CardSuite.Heart) && (card.Rank == 10)) { match = true; break; }
			if (!match) return 0; else match = false;
			foreach (var card in cards) if ((card.Suite == CardSuite.Heart) && (card.Rank == 11)) { match = true; break; }
			if (!match) return 0; else match = false;
			foreach (var card in cards) if ((card.Suite == CardSuite.Heart) && (card.Rank == 12)) { match = true; break; }
			if (!match) return 0; else match = false;
			foreach (var card in cards) if ((card.Suite == CardSuite.Heart) && (card.Rank == 13)) { match = true; break; }
			if (!match) return 0; else match = false;
			foreach (var card in cards) if ((card.Suite == CardSuite.Heart) && (card.Rank == 14)) { match = true; break; }
			if (!match) return 0;
			return 1;
		}

		private int IsHighCard(CList<Card> cards) {
			// All cards have different rank, rank top five cards  (2, 3, 4, 5, A  is higher than  9, 10, J, Q, K)
			// 2 = 1, 3 = 2, 4 = 8, 5 = 16
			cards.Sort(SortCardsOnRankFunc);
			int value = 0, count = 0;

			foreach (var card in cards) {
				value |= (0x0001 << card.Rank);
				if (++count >= 5) break;
			}

			Console.Write("\n High Card Rank (sorted): ");
			foreach (var c in cards)
			{
				Console.Write($"  {c.Symbol}");
			}

			Console.Write($"  Value={value}\n (five firdst cards)");
			return value;
		}

		private bool SortCardsOnRankFunc(Card card1, Card card2) {
			if (card1.Rank < card2.Rank) return true;
			return false;
		}

		CardGameTable gametable = null;

	}
}
