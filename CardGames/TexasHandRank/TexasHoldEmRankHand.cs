using Syslib;
using Syslib.Games.Card;
using System;

namespace Games.Card.TexasHoldEm
{

	public class TexasHoldEmRankHand : CardGameRankHand
	{
		public override IPlayCardHandRank HandSignature(IPlayCards playercards)
		{
			ulong value = 0;
			if ((value = IsRoyalStraightFlush(playercards)) > 0) { return new TexasHandRankRoyalStraightFlush(value); }
			if ((value = IsStraightFlush(playercards)) > 0) { return new TexasHandRankStraightFlush(value); }
			if ((value = IsFourOfAKind(playercards)) > 0) { return new TexasHandRankFourOfAKind(value); }
			if ((value = IsFullHouse(playercards)) > 0) { return new TexasHandRankFullHouse(value); }
			if ((value = IsFlush(playercards)) > 0) { return new TexasHandRankFlush(value); }
			if ((value = IsStraight(playercards)) > 0) { return new TexasHandRankStraight(value); }
			if ((value = IsThreeOfAKind(playercards)) > 0) { return new TexasHandRankThreeOfAKind(value); }
			if ((value = IsTwoPair(playercards)) > 0) { return new TexasHandRankTwoPair(value); }
			if ((value = IsPair(playercards)) > 0) { return new TexasHandRankPair(value); }
			if ((value = IsHighCard(playercards)) > 0) { return new TexasHandRankHighCard(value); }
			return new TexasHandRankNothing();
		}

		override public IPlayCardHandRank RankHand(IPlayCards cards)
		{
			ulong value = 0;
			if ((cards == null) || (cards.Count() == 0)) { return new TexasHandRankNothing(); }
			if ((value = IsRoyalStraightFlush2(cards)) > 0) { return new TexasHandRankRoyalStraightFlush(value); }
			if ((value = IsStraightFlush2(cards)) > 0) { return new TexasHandRankStraightFlush(value); }
			if ((value = IsFourOfAKind2(cards)) > 0) { return new TexasHandRankFourOfAKind(value); }
			if ((value = IsFullHouse2(cards)) > 0) { return new TexasHandRankFullHouse(value); }
			if ((value = IsFlush2(cards)) > 0) { return new TexasHandRankFlush(value); }
			if ((value = IsStraight2(cards)) > 0) { return new TexasHandRankStraight(value); }
			if ((value = IsThreeOfAKind2(cards)) > 0) { return new TexasHandRankThreeOfAKind(value); }
			if ((value = IsTwoPair2(cards)) > 0) { return new TexasHandRankTwoPair(value); }
			if ((value = IsPair2(cards)) > 0) { return new TexasHandRankPair(value); }
			if ((value = IsHighCard2(cards)) > 0) { return new TexasHandRankHighCard(value); }
			return new TexasHandRankNothing();
		}

	}
	
	public enum TexasHoldEmHand { Nothing = 0, HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalStraightFlush }


}
