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
			if ((cards == null) || (cards.Count() == 0)) { return new PlayCardHandRankNothing((int)TexasHoldEmHand.Nothing); }
			if ((value = IsRoyalStraightFlush(cards)) > 0) { return new PlayCardHandRankRoyalStraightFlush(value, (int)TexasHoldEmHand.RoyalStraightFlush); }
			if ((value = IsStraightFlush(cards)) > 0) { return new PlayCardHandRankStraightFlush(value, (int)TexasHoldEmHand.StraightFlush); }
			if ((value = IsFourOfAKind(cards)) > 0) { return new PlayCardHandRankFourOfAKind(value, (int)TexasHoldEmHand.FourOfAKind); }
			if ((value = IsFullHouse(cards)) > 0) { return new PlayCardHandRankFullHouse(value, (int)TexasHoldEmHand.FullHouse); }
			if ((value = IsFlush(cards)) > 0) { return new PlayCardHandRankFlush(value, (int)TexasHoldEmHand.Flush); }
			if ((value = IsStraight(cards)) > 0) { return new PlayCardHandRankStraight(value, (int)TexasHoldEmHand.Straight); }
			if ((value = IsThreeOfAKind(cards)) > 0) { return new PlayCardHandRankThreeOfAKind(value, (int)TexasHoldEmHand.ThreeOfAKind); }
			if ((value = IsTwoPair(cards)) > 0) { return new PlayCardHandRankTwoPair(value, (int)TexasHoldEmHand.TwoPair); }
			if ((value = IsPair(cards)) > 0) { return new PlayCardHandRankPair(value, (int)TexasHoldEmHand.Pair); }
			if ((value = IsHighCard(cards)) > 0) { return new PlayCardHandRankHighCard(value, (int)TexasHoldEmHand.HighCard); }
			return new PlayCardHandRankNothing((int)TexasHoldEmHand.Nothing);
		}



	}
	
	public enum TexasHoldEmHand { Nothing = 0, HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalStraightFlush }


}
