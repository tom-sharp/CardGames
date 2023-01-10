using Syslib;
using Syslib.Games.Card;
using System;

namespace Games.Card.TexasHoldEm
{

	public class TexasRankHand : PlayCardsRank
	{
		public override IPlayCardsSignature GetSignature(IPlayCards playercards)
		{
			var rank = rankOn5Cards(playercards);
			return rank;

		}

		IPlayCardsSignature rankOn5Cards(IPlayCards playercards)
		{
			ulong value;
			if ((value = IsRoyalStraightFlush((int)PokerHand.RoyalStraightFlush, playercards)) > 0) return new TexasRankRoyalStraightFlush(value,this);
			if ((value = IsStraightFlush((int)PokerHand.StraightFlush, playercards)) > 0) return new TexasRankStraightFlush(value, this);
			if ((value = IsFourOfAKind((int)PokerHand.FourOfAKind, playercards)) > 0) return new TexasRankFourOfAKind(value, this);
			if ((value = IsFullHouse((int)PokerHand.FullHouse, playercards)) > 0) return new TexasRankFullHouse(value, this);
			if ((value = IsFlush((int)PokerHand.Flush, playercards)) > 0) return new TexasRankFlush(value, this);
			if ((value = IsStraight((int)PokerHand.Straight, playercards)) > 0) return new TexasRankStraight(value, this);
			if ((value = IsThreeOfAKind((int)PokerHand.ThreeOfAKind, playercards)) > 0) return new TexasRankThreeOfAKind(value, this);
			if ((value = IsTwoPair((int)PokerHand.TwoPair, playercards)) > 0) return new TexasRankTwoPair(value, this);
			if ((value = IsPair((int)PokerHand.Pair, playercards)) > 0) return new TexasRankPair(value, this);
			if ((value = IsHighCard((int)PokerHand.HighCard, playercards)) > 0) return new TexasRankHighCard(value, this);
			return new TexasRankNothing();
		}


		//public override IPlayCardHandRank HandSignature(IPlayCards playercards)
		//{
		//	ulong value = 0;
		//	if ((value = IsRoyalStraightFlush(playercards)) > 0) { return new TexasHandRankRoyalStraightFlush(value); }
		//	if ((value = IsStraightFlush(playercards)) > 0) { return new TexasHandRankStraightFlush(value); }
		//	if ((value = IsFourOfAKind(playercards)) > 0) { return new TexasHandRankFourOfAKind(value); }
		//	if ((value = IsFullHouse(playercards)) > 0) { return new TexasHandRankFullHouse(value); }
		//	if ((value = IsFlush(playercards)) > 0) { return new TexasHandRankFlush(value); }
		//	if ((value = IsStraight(playercards)) > 0) { return new TexasHandRankStraight(value); }
		//	if ((value = IsThreeOfAKind(playercards)) > 0) { return new TexasHandRankThreeOfAKind(value); }
		//	if ((value = IsTwoPair(playercards)) > 0) { return new TexasHandRankTwoPair(value); }
		//	if ((value = IsPair(playercards)) > 0) { return new TexasHandRankPair(value); }
		//	if ((value = IsHighCard(playercards)) > 0) { return new TexasHandRankHighCard(value); }
		//	return new TexasHandRankNothing();
		//}

		//override public IPlayCardHandRank RankHand(IPlayCards cards)
		//{
		//	ulong value = 0;
		//	if ((cards == null) || (cards.Count() == 0)) { return new TexasHandRankNothing(); }
		//	if ((value = IsRoyalStraightFlush2(cards)) > 0) { return new TexasHandRankRoyalStraightFlush(value); }
		//	if ((value = IsStraightFlush2(cards)) > 0) { return new TexasHandRankStraightFlush(value); }
		//	if ((value = IsFourOfAKind2(cards)) > 0) { return new TexasHandRankFourOfAKind(value); }
		//	if ((value = IsFullHouse2(cards)) > 0) { return new TexasHandRankFullHouse(value); }
		//	if ((value = IsFlush2(cards)) > 0) { return new TexasHandRankFlush(value); }
		//	if ((value = IsStraight2(cards)) > 0) { return new TexasHandRankStraight(value); }
		//	if ((value = IsThreeOfAKind2(cards)) > 0) { return new TexasHandRankThreeOfAKind(value); }
		//	if ((value = IsTwoPair2(cards)) > 0) { return new TexasHandRankTwoPair(value); }
		//	if ((value = IsPair2(cards)) > 0) { return new TexasHandRankPair(value); }
		//	if ((value = IsHighCard2(cards)) > 0) { return new TexasHandRankHighCard(value); }
		//	return new TexasHandRankNothing();
		//}

	}


}
