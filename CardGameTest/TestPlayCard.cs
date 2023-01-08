using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syslib.Games.Card;

namespace CardGameTest
{
	[TestClass]
	public class TestPlayCard
	{


		[TestMethod]
		public void PlayCard_InitProps_InitProps()
		{
			IPlayCard Heart14 = new PlayCardHeart(14);
			IPlayCard Diamond14 = new PlayCardDiamond(14);
			IPlayCard Spade14 = new PlayCardSpade(14);
			IPlayCard Club14 = new PlayCardClub(14);
			IPlayCard Joker = new PlayCardJoker();
			IPlayCard Invalid1 = new PlayCardSpade(15);
			IPlayCard Invalid2 = new PlayCardSpade(1);


			Assert.AreEqual(14, Heart14.Rank);
			Assert.AreEqual(PlayCardSuit.Heart, Heart14.Suit);
			Assert.AreEqual("AH", Heart14.Symbol);

			Assert.AreEqual(14, Diamond14.Rank);
			Assert.AreEqual(PlayCardSuit.Diamond, Diamond14.Suit);
			Assert.AreEqual("AD", Diamond14.Symbol);

			Assert.AreEqual(14, Spade14.Rank);
			Assert.AreEqual(PlayCardSuit.Spade, Spade14.Suit);
			Assert.AreEqual("AS", Spade14.Symbol);

			Assert.AreEqual(14, Club14.Rank);
			Assert.AreEqual(PlayCardSuit.Club, Club14.Suit);
			Assert.AreEqual("AC", Club14.Symbol);

			Assert.AreEqual(0, Joker.Rank);
			Assert.AreEqual(PlayCardSuit.Joker, Joker.Suit);
			Assert.AreEqual("**", Joker.Symbol);

			Assert.AreEqual(0, Invalid1.Rank);
			Assert.AreEqual(PlayCardSuit.Blank, Invalid1.Suit);
			Assert.AreEqual("--", Invalid1.Symbol);

			Assert.AreEqual(0, Invalid2.Rank);
			Assert.AreEqual(PlayCardSuit.Blank, Invalid1.Suit);
			Assert.AreEqual("--", Invalid1.Symbol);

		}


		[TestMethod]
		public void PlayCardJoker_ChangeProps_ChangeProps()
		{
			IPlayCard joker = new PlayCardJoker();

			(joker as IPlayCardJoker).Suit = PlayCardSuit.Heart;
			(joker as IPlayCardJoker).Rank = 14;
			joker.Symbol = "Joker";

			Assert.AreEqual(14, joker.Rank);
			Assert.AreEqual(PlayCardSuit.Heart, joker.Suit);
			Assert.AreEqual("Joker", joker.Symbol);

			(joker as IPlayCardJoker).Clear();

			Assert.AreEqual(0, joker.Rank);
			Assert.AreEqual(PlayCardSuit.Joker, joker.Suit);
			Assert.AreEqual("Joker", joker.Symbol);

		}


	}
}
