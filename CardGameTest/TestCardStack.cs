using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syslib.Games.Card;

namespace CardGameTest
{
	[TestClass]
	public class TestCardStack
	{
		[TestMethod]
		public void CardDeck_Init_Init() {
			var deck = new PlayCardDeck();

			Assert.AreEqual(52, deck.CardsTotal);
			Assert.AreEqual(52, deck.CardsLeft);

		}

		[TestMethod]
		public void CardDeck_InitJokers_Init()
		{
			var deck = new PlayCardDeck(jokers: 2);

			Assert.AreEqual(54, deck.CardsTotal);
			Assert.AreEqual(54, deck.CardsLeft);

		}


		[TestMethod]
		public void CardDeck_InitInvalidJokers_Limited3Jokers()
		{
			var deck1 = new PlayCardDeck(jokers: 20);
			var deck2 = new PlayCardDeck(jokers: -4);

			Assert.AreEqual(55, deck1.CardsTotal);
			Assert.AreEqual(55, deck1.CardsLeft);

			Assert.AreEqual(52, deck2.CardsTotal);
			Assert.AreEqual(52, deck2.CardsLeft);

		}


		[TestMethod]
		public void CardDeck_DrawCard_CardsLeft()
		{
			var deck = new PlayCardDeck();
			IPlayCard card;

			int expectedcardsleft = 52;

			Assert.AreEqual(expectedcardsleft, deck.CardsLeft);

			while (expectedcardsleft > 0)
			{
				card = deck.NextCard();
				Assert.IsNotNull(card);

				expectedcardsleft--;
				Assert.AreEqual(expectedcardsleft, deck.CardsLeft);
			}
			card = deck.NextCard();
			Assert.IsNull(card);

		}

		[TestMethod]
		public void CardStack_InitDefault_Init() {
			var stack = new PlayCardStack(decks: 1);

			Assert.AreEqual(52, stack.CardsTotal);
			Assert.AreEqual(52, stack.CardsLeft);

		}

		[TestMethod]
		public void CardStack_InitWithTemplate_Init()
		{
			var deck = new PlayCardDeck(jokers: 2);
			var stack = new PlayCardStack(decks: 3, decktemplate: deck);

			Assert.AreEqual(162, stack.CardsTotal);
			Assert.AreEqual(162, stack.CardsLeft);

		}

		[TestMethod]
		public void CardStack_DrawCard_CardsLeft()
		{
			var deck = new PlayCardDeck(jokers: 2);
			var stack = new PlayCardStack(decks: 2, decktemplate: deck);
			IPlayCard card;

			int expectedcardsleft = 108;

			Assert.AreEqual(expectedcardsleft, stack.CardsLeft);

			while (expectedcardsleft > 0)
			{
				card = stack.NextCard();
				Assert.IsNotNull(card);

				expectedcardsleft--;
				Assert.AreEqual(expectedcardsleft, stack.CardsLeft);
			}
			card = stack.NextCard();
			Assert.IsNull(card);

		}


	}
}
