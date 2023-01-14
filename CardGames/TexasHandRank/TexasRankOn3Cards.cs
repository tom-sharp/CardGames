using Syslib;
using Syslib.Games.Card;
using System;

namespace Games.Card.TexasHoldEm
{

	public class TexasRankOn3Cards : PlayCardsRank
	{
		public override IPlayCardsRankSignature GetSignature(IPlayCards playercards)
		{
			ulong value;
			if ((value = IsRoyalStraightFlush((int)PokerHand.RoyalStraightFlush, playercards, 3)) > 0) return new TexasRankRoyalStraightFlush(value);
			if ((value = IsStraightFlush((int)PokerHand.StraightFlush, playercards, 3)) > 0) return new TexasRankStraightFlush(value);
			if ((value = IsFourOfAKind((int)PokerHand.FourOfAKind, playercards, 3)) > 0) return new TexasRankFourOfAKind(value);
			if ((value = IsFullHouse((int)PokerHand.FullHouse, playercards, 3)) > 0) return new TexasRankFullHouse(value);
			if ((value = IsFlush((int)PokerHand.Flush, playercards, 3)) > 0) return new TexasRankFlush(value);
			if ((value = IsStraight((int)PokerHand.Straight, playercards, 3)) > 0) return new TexasRankStraight(value);
			if ((value = IsThreeOfAKind((int)PokerHand.ThreeOfAKind, playercards, 3)) > 0) return new TexasRankThreeOfAKind(value);
			if ((value = IsTwoPair((int)PokerHand.TwoPair, playercards, 3)) > 0) return new TexasRankTwoPair(value);
			if ((value = IsPair((int)PokerHand.Pair, playercards, 3)) > 0) return new TexasRankPair(value);
			if ((value = IsHighCard((int)PokerHand.HighCard, playercards, 3)) > 0) return new TexasRankHighCard(value);
			return new TexasRankNothing();
		}


	}


}
