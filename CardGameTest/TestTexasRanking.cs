using Games.Card.TexasHoldEm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syslib.Games.Card;
using Syslib;

namespace CardGameTest
{
	[TestClass]
	public class TestTexasRanking
	{

		[TestMethod]
		public void Ranking_RoyalStraightFlush5Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(12));
			playerhand.Add(new PlayCardHeart(10));
			playerhand.Add(new PlayCardHeart(14));
			playerhand.Add(new PlayCardHeart(11));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.RoyalStraightFlush);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}



		[TestMethod]
		public void Ranking_StraightFlush5Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardClub(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(7));
			playerhand.Add(new PlayCardClub(5));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.StraightFlush);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}





		[TestMethod]
		public void Ranking_FourOfAKind5Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(3));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.FourOfAKind);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}


		[TestMethod]
		public void Ranking_FullHouse5Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(8));
			playerhand.Add(new PlayCardSpade(13));
			playerhand.Add(new PlayCardDiamond(8));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.FullHouse);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}



		[TestMethod]
		public void Ranking_Flush5Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(12));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(9));
			playerhand.Add(new PlayCardClub(5));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.Flush);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}



		[TestMethod]
		public void Ranking_Straight5Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(7));
			playerhand.Add(new PlayCardDiamond(5));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.Straight);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}


		[TestMethod]
		public void Ranking_ThreeOfAKind5Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.ThreeOfAKind);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}


		[TestMethod]
		public void Ranking_TwoPair5Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(2));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.TwoPair);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);

		}




		[TestMethod]
		public void Ranking_Pair5Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.Pair);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);

		}




		[TestMethod]
		public void Ranking_HighCard5Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(11));
			playerhand.Add(new PlayCardDiamond(4));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.HighCard);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);

		}


		[TestMethod]
		public void Ranking_NoCards_Nothing()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedvalue = 0;

			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.Nothing);
			Assert.AreEqual(expectedvalue, hand1.Signature);

		}


		[TestMethod]
		public void Ranking_NullArg_Nothing()
		{
			var rank = new TexasRankOn5Cards();
			ulong expectedvalue = 0;

			var hand1 = rank.GetSignature(null);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.Nothing);
			Assert.AreEqual(expectedvalue, hand1.Signature);

		}







		[TestMethod]
		public void Ranking_RoyalStraightFlush7Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(12));
			playerhand.Add(new PlayCardHeart(10));
			playerhand.Add(new PlayCardHeart(14));
			playerhand.Add(new PlayCardHeart(11));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(5));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.RoyalStraightFlush);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}



		[TestMethod]
		public void Ranking_StraightFlush7Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardClub(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(7));
			playerhand.Add(new PlayCardClub(5));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.StraightFlush);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}





		[TestMethod]
		public void Ranking_FourOfAKind7Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(3));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.FourOfAKind);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}


		[TestMethod]
		public void Ranking_FullHouse7Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(8));
			playerhand.Add(new PlayCardSpade(13));
			playerhand.Add(new PlayCardDiamond(8));
			playerhand.Add(new PlayCardHeart(10));
			playerhand.Add(new PlayCardHeart(14));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.FullHouse);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}



		[TestMethod]
		public void Ranking_Flush7Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(12));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(9));
			playerhand.Add(new PlayCardClub(5));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.Flush);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}



		[TestMethod]
		public void Ranking_Straight7Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(7));
			playerhand.Add(new PlayCardDiamond(5));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.Straight);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}


		[TestMethod]
		public void Ranking_ThreeOfAKind7Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.ThreeOfAKind);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);
		}


		[TestMethod]
		public void Ranking_TwoPair7Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(2));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.TwoPair);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);

		}




		[TestMethod]
		public void Ranking_Pair7Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.Pair);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);

		}




		[TestMethod]
		public void Ranking_HighCard7Cards_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(11));
			playerhand.Add(new PlayCardDiamond(4));
			playerhand.Add(new PlayCardHeart(6));
			playerhand.Add(new PlayCardHeart(2));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual(rankid, (byte)PokerHand.HighCard);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);

		}


		[TestMethod]
		public void Ranking_Straightd7CardsRankOn5_Success()
		{
			var rank = new TexasRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardHeart(6));
			playerhand.Add(new PlayCardClub(4));
			playerhand.Add(new PlayCardClub(10));
			playerhand.Add(new PlayCardSpade(8));
			playerhand.Add(new PlayCardDiamond(7));
			playerhand.Add(new PlayCardClub(5));
			playerhand.Add(new PlayCardSpade(5));
			var hand1 = rank.GetSignature(playerhand);
			var rankid = PlayCards.RankId(hand1.Signature);

			Assert.AreEqual((byte)PokerHand.Straight, rankid);
			Assert.AreNotEqual(expectedNotvalue, hand1.Signature);

		}














	}
}
