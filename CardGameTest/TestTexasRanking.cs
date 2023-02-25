using Games.Card.TexasHoldEm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;

namespace CardGameTest
{
	[TestClass]
	public class TestTexasRanking
	{

		[TestMethod]
		public void Ranking_RoyalStraightFlush5Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(12));
			playerhand.Add(new PlayCardHeart(10));
			playerhand.Add(new PlayCardHeart(14));
			playerhand.Add(new PlayCardHeart(11));

			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreNotEqual(expectedNotvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.RoyalStraightFlush);
		}



		[TestMethod]
		public void Ranking_StraightFlush5Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardClub(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(7));
			playerhand.Add(new PlayCardClub(5));

			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreNotEqual(expectedNotvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.StraightFlush);
		}





		[TestMethod]
		public void Ranking_FourOfAKind5Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(3));
			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreNotEqual(expectedNotvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.FourOfAKind);
		}


		[TestMethod]
		public void Ranking_FullHouse5Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(8));
			playerhand.Add(new PlayCardSpade(13));
			playerhand.Add(new PlayCardDiamond(8));
			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreNotEqual(expectedNotvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.FullHouse);
		}



		[TestMethod]
		public void Ranking_Flush5Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(12));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(9));
			playerhand.Add(new PlayCardClub(5));
			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreNotEqual(expectedNotvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.Flush);
		}



		[TestMethod]
		public void Ranking_Straight5Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(7));
			playerhand.Add(new PlayCardDiamond(5));
			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreNotEqual(expectedNotvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.Straight);
		}


		[TestMethod]
		public void Ranking_ThreeOfAKind5Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreNotEqual(expectedNotvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.ThreeOfAKind);
		}


		[TestMethod]
		public void Ranking_TwoPair5Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(2));
			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreNotEqual(expectedNotvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.TwoPair);

		}




		[TestMethod]
		public void Ranking_Pair5Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreNotEqual(expectedNotvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.Pair);

		}




		[TestMethod]
		public void Ranking_HighCard5Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(11));
			playerhand.Add(new PlayCardDiamond(4));
			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreNotEqual(expectedNotvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.HighCard);

		}


		[TestMethod]
		public void Ranking_NoCards_Nothing()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedvalue = 0;

			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(expectedvalue, hand1);
			Assert.AreEqual(rankid, (byte)PokerHand.Nothing);

		}


		[TestMethod]
		public void Ranking_NullArg_Nothing()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			ulong expectedvalue = 0;


			var hand1 = rank.GetSignature(null);
			var rankid = hand1.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.Nothing);
			Assert.AreEqual(expectedvalue, hand1.Signature);

		}







		[TestMethod]
		public void Ranking_RoyalStraightFlush7Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();
			ulong expectedNotvalue = 0;

			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(12));
			playerhand.Add(new PlayCardHeart(10));
			playerhand.Add(new PlayCardHeart(14));
			playerhand.Add(new PlayCardHeart(11));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(5));

			playerhand.RankCards(rank);
			var hand1 = playerhand.RankSignature.Signature;
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.RoyalStraightFlush);
			Assert.AreNotEqual(expectedNotvalue, hand1);
		}



		[TestMethod]
		public void Ranking_StraightFlush7Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardClub(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(7));
			playerhand.Add(new PlayCardClub(5));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.StraightFlush);
		}





		[TestMethod]
		public void Ranking_FourOfAKind7Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(3));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.FourOfAKind);
		}


		[TestMethod]
		public void Ranking_FullHouse7Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(8));
			playerhand.Add(new PlayCardSpade(13));
			playerhand.Add(new PlayCardDiamond(8));
			playerhand.Add(new PlayCardHeart(10));
			playerhand.Add(new PlayCardHeart(14));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.FullHouse);
		}



		[TestMethod]
		public void Ranking_Flush7Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(12));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(9));
			playerhand.Add(new PlayCardClub(5));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.Flush);
		}



		[TestMethod]
		public void Ranking_Straight7Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(7));
			playerhand.Add(new PlayCardDiamond(5));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.Straight);
		}


		[TestMethod]
		public void Ranking_ThreeOfAKind7Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.ThreeOfAKind);
		}


		[TestMethod]
		public void Ranking_TwoPair7Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(2));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.TwoPair);

		}




		[TestMethod]
		public void Ranking_Pair7Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(14));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.Pair);

		}




		[TestMethod]
		public void Ranking_HighCard7Cards_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();


			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(11));
			playerhand.Add(new PlayCardDiamond(4));
			playerhand.Add(new PlayCardHeart(6));
			playerhand.Add(new PlayCardHeart(2));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual(rankid, (byte)PokerHand.HighCard);

		}


		[TestMethod]
		public void Ranking_Straightd7CardsRankOn5_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();

			playerhand.Clear();
			playerhand.Add(new PlayCardHeart(6));
			playerhand.Add(new PlayCardClub(4));
			playerhand.Add(new PlayCardClub(10));
			playerhand.Add(new PlayCardSpade(8));
			playerhand.Add(new PlayCardDiamond(7));
			playerhand.Add(new PlayCardClub(5));
			playerhand.Add(new PlayCardSpade(5));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual((byte)PokerHand.Straight, rankid);
		}

		[TestMethod]
		public void Ranking_StraightWithAce_Success()
		{
			var rank = new TexasHoldEmRankOn5Cards();
			var playerhand = new PlayCards();


			playerhand.Clear();
			playerhand.Add(new PlayCardDiamond(13));
			playerhand.Add(new PlayCardDiamond(5));
			playerhand.Add(new PlayCardSpade(2));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardClub(14));
			playerhand.Add(new PlayCardClub(4));
			playerhand.Add(new PlayCardSpade(4));

			playerhand.RankCards(rank);
			var rankid = playerhand.RankSignature.RankId;

			Assert.AreEqual((byte)PokerHand.Straight, rankid);

		}













	}
}
