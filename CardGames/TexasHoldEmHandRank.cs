using Syslib;
using Syslib.Games.Card;
using System;

namespace Games.Card.TexasHoldEm
{

	// Rank level of cards on hand.
	// 1. what kind of hadn pair flush or somthing.
	// 2. the value of the holding hand pair of knight is better than pair of eight for example
	// Value is needed to separate two or more with same type of hand.

	public class TexasHoldEmHandRank : CardGameHandRank
	{
		public TexasHoldEmHandRank() {
			TableSeat = null;
			Hand = TexasHoldEmHand.Nothing;
			Value = 0;
		}

		public TexasHoldEmHandRank(TexasHoldEmHand hand, int value)
		{
			TableSeat = null;
			Hand = hand;
			Value = value;
		}

		public TexasHoldEmHandRank(CardGameTableSeat tableseat, TexasHoldEmHand hand, int value)
		{
			TableSeat = tableseat;
			Hand = hand;
			Value = value;
		}

		//  return highest TexasHoldEm Rank of hand cards. if there is no cards 0 is returned
		// NOTE! Jokers are not supported here
		override public void RankHand(CList<IPlayCard> cards)
		{
			Int64 value = 0;
			if ((cards == null) || (cards.Count() == 0)) { this.Hand = TexasHoldEmHand.Nothing; this.Value = 0; return; }
			if ((value = IsRoyalStraightFlush(cards)) > 0) { this.Hand = TexasHoldEmHand.RoyalStraightFlush; this.Value = value; return; }
			if ((value = IsStraightFlush(cards)) > 0) { this.Hand = TexasHoldEmHand.StraightFlush; this.Value = value; return; }
			if ((value = IsFourOfAKind(cards)) > 0) { this.Hand = TexasHoldEmHand.FourOfAKind; this.Value = value; return; }
			if ((value = IsFullHouse(cards)) > 0) { this.Hand = TexasHoldEmHand.FullHouse; this.Value = value; return; }
			if ((value = IsFlush(cards)) > 0) { this.Hand = TexasHoldEmHand.Flush; this.Value = value; return; }
			if ((value = IsStraight(cards)) > 0) { this.Hand = TexasHoldEmHand.Straight; this.Value = value; return; }
			if ((value = IsThreeOfAKind(cards)) > 0) { this.Hand = TexasHoldEmHand.ThreeOfAKind; this.Value = value; return; }
			if ((value = IsTwoPair(cards)) > 0) { this.Hand = TexasHoldEmHand.TwoPair; this.Value = value; return; }
			if ((value = IsPair(cards)) > 0) { this.Hand = TexasHoldEmHand.Pair; this.Value = value; return; }
			if ((value = IsHighCard(cards)) > 0) { this.Hand = TexasHoldEmHand.HighCard; this.Value = value; return; }
			this.Hand = TexasHoldEmHand.Nothing;
			this.Value = 0;
		}



		public ICardGameTableSeat TableSeat { get; set; }

		public TexasHoldEmHand Hand { get; set; }

		public Int64 Value { get; set; }


	}
	
	public enum TexasHoldEmHand { Nothing = 0, HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalStraightFlush }


}
