using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{

	// Rank level of cards on hand.
	// 1. what kind of hadn pair flush or somthing.
	// 2. the value of the holding hand pair of knight is better than pair of eight for example
	// Value is needed to separate two or more with same type of hand.

	public class TexasHoldEmRank
	{
		public TexasHoldEmRank() {
			TableSeat = null;
			Hand = TexasHoldEmHand.Nothing;
			Value = 0;
		}
		public TexasHoldEmRank(TexasHoldEmHand hand, int value)
		{
			TableSeat = null;
			Hand = hand;
			Value = value;
		}
		public TexasHoldEmRank(CardGameTableSeat tableseat, TexasHoldEmHand hand, int value)
		{
			TableSeat = tableseat;
			Hand = hand;
			Value = value;
		}

		public CardGameTableSeat TableSeat { get; set; }

		public TexasHoldEmHand Hand { get; set; }

		public int Value { get; set; }


	}

	public enum TexasHoldEmHand { Nothing = 0, HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalStraightFlush }


}
