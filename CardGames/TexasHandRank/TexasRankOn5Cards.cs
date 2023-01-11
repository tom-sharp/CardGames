using Syslib;
using Syslib.Games.Card;
using System;

namespace Games.Card.TexasHoldEm
{

	public class TexasRankOn5Cards : PlayCardsRank
	{
		public override IPlayCardsSignature GetSignature(IPlayCards playercards)
		{
			ulong value;
			if ((value = IsRoyalStraightFlush((int)PokerHand.RoyalStraightFlush, playercards)) > 0) return new TexasRankRoyalStraightFlush(value, this);
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


	}


}
