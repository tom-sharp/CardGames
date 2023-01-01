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
		public void Ranking_RoyalStraightFlush_Success()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedNotvalue = 0;

			playerhand.Add(new PlayCardHeart(13));
			playerhand.Add(new PlayCardHeart(12));
			playerhand.Add(new PlayCardHeart(10));
			playerhand.Add(new PlayCardHeart(14));
			playerhand.Add(new PlayCardHeart(11));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.RoyalStraightFlush);
			Assert.AreNotEqual(expectedNotvalue, hand1.Value);
		}



		[TestMethod]
		public void Ranking_StraightFlush_Success()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardClub(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(7));
			playerhand.Add(new PlayCardClub(5));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.StraightFlush);
			Assert.AreNotEqual(expectedNotvalue, hand1.Value);
		}





		[TestMethod]
		public void Ranking_FourOfAKind_Success()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(3));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.FourOfAKind);
			Assert.AreNotEqual(expectedNotvalue, hand1.Value);
		}


		[TestMethod]
		public void Ranking_FullHouse_Success()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(8));
			playerhand.Add(new PlayCardSpade(13));
			playerhand.Add(new PlayCardDiamond(8));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.FullHouse);
			Assert.AreNotEqual(expectedNotvalue, hand1.Value);
		}



		[TestMethod]
		public void Ranking_Flush_Success()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(12));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardClub(9));
			playerhand.Add(new PlayCardClub(5));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.Flush);
			Assert.AreNotEqual(expectedNotvalue, hand1.Value);
		}



		[TestMethod]
		public void Ranking_Straight_Success()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(7));
			playerhand.Add(new PlayCardDiamond(5));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.Straight);
			Assert.AreNotEqual(expectedNotvalue, hand1.Value);
		}


		[TestMethod]
		public void Ranking_ThreeOfAKind_Success()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.ThreeOfAKind);
			Assert.AreNotEqual(expectedNotvalue, hand1.Value);
		}


		[TestMethod]
		public void Ranking_TwoPair_Success()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(2));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(2));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.TwoPair);
			Assert.AreNotEqual(expectedNotvalue, hand1.Value);

		}




		[TestMethod]
		public void Ranking_Pair_Success()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardHeart(4));
			playerhand.Add(new PlayCardClub(6));
			playerhand.Add(new PlayCardSpade(3));
			playerhand.Add(new PlayCardDiamond(5));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.Pair);
			Assert.AreNotEqual(expectedNotvalue, hand1.Value);

		}




		[TestMethod]
		public void Ranking_HighCard_Success()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedNotvalue = 0;

			playerhand.Clear();
			playerhand.Add(new PlayCardClub(13));
			playerhand.Add(new PlayCardHeart(8));
			playerhand.Add(new PlayCardClub(3));
			playerhand.Add(new PlayCardSpade(11));
			playerhand.Add(new PlayCardDiamond(4));
			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.HighCard);
			Assert.AreNotEqual(expectedNotvalue, hand1.Value);

		}


		[TestMethod]
		public void Ranking_NoCards_Nothing()
		{
			var rank = new TexasHoldEmHandRank();
			var playerhand = new CList<IPlayCard>();
			ulong expectedvalue = 0;

			var hand1 = rank.RankHand(playerhand) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.Nothing);
			Assert.AreEqual(expectedvalue, hand1.Value);

		}


		[TestMethod]
		public void Ranking_NullArg_Nothing()
		{
			var rank = new TexasHoldEmHandRank();
			ulong expectedvalue = 0;

			var hand1 = rank.RankHand(null) as ITexasHandRank;

			Assert.AreEqual(hand1.Id, TexasHoldEmHand.Nothing);
			Assert.AreEqual(expectedvalue, hand1.Value);

		}





	}
}
