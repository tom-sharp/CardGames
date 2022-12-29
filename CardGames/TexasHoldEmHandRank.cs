using Syslib;
using Syslib.Games.Card;
using System;

namespace Games.Card.TexasHoldEm
{

	// Rank level of cards on hand.
	// 1. what kind of hadn pair flush or somthing.
	// 2. the value of the holding hand pair of knight is better than pair of eight for example
	// Value is needed to separate two or more with same type of hand.

	public class TexasHoldEmHandRank : CardGameRankHand
	{

		override public IPlayCardHandRank RankHand(CList<IPlayCard> cards)
		{
			ulong value = 0;
			if ((cards == null) || (cards.Count() == 0)) { return new TexasHandRankNothing(); }
			if ((value = IsRoyalStraightFlush(cards)) > 0) { return new TexasHandRankRoyalStraightFlush(value); }
			if ((value = IsStraightFlush(cards)) > 0) { return new TexasHandRankStraightFlush(value); }
			if ((value = IsFourOfAKind(cards)) > 0) { return new TexasHandRankFourOfAKind(value); }
			if ((value = IsFullHouse(cards)) > 0) { return new TexasHandRankFullHouse(value); }
			if ((value = IsFlush(cards)) > 0) { return new TexasHandRankFlush(value); }
			if ((value = IsStraight(cards)) > 0) { return new TexasHandRankStraight(value); }
			if ((value = IsThreeOfAKind(cards)) > 0) { return new TexasHandRankThreeOfAKind(value); }
			if ((value = IsTwoPair(cards)) > 0) { return new TexasHandRankTwoPair(value); }
			if ((value = IsPair(cards)) > 0) { return new TexasHandRankPair(value); }
			if ((value = IsHighCard(cards)) > 0) { return new TexasHandRankHighCard(value); }
			return new TexasHandRankNothing();
		}



	}
	
	public enum TexasHoldEmHand { Nothing = 0, HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalStraightFlush }


}
