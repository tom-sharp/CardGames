using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games.Card.TexasHoldEm;
using Syslib.Games.Card;


namespace CardGameTest
{
	[TestClass]
	public class TestTexasTable
	{
		[TestMethod]
		public void Texastable_nullInit_DefaultInit() {

			var table = new TexasHoldEmTable(tableConfig: null);
			int expectedseatcount = 0;
			int expectedplayercount = 0;
			int expectedativecount = 0;

			Assert.AreEqual(expectedseatcount, table.SeatCount);
			Assert.AreEqual(expectedplayercount, table.PlayerCount);
			Assert.AreEqual(expectedativecount, table.ActiveSeatCount);

			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNotNull(table.TableSeats);

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null,null)));

		}

		[TestMethod]
		public void Texastable_Init0Seat_DefaultInit()
		{

			var table = new TexasHoldEmTable(new CardTableConfig());
			int expectedseatcount = 0;
			int expectedplayercount = 0;
			int expectedativecount = 0;

			Assert.AreEqual(expectedseatcount, table.SeatCount);
			Assert.AreEqual(expectedplayercount, table.PlayerCount);
			Assert.AreEqual(expectedativecount, table.ActiveSeatCount);

			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNotNull(table.TableSeats);

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, null)));

		}

		[TestMethod]
		public void Texastable_InitNegativSeat_DefaultInit()
		{

			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = -1});
			int expectedseatcount = 0;
			int expectedplayercount = 0;
			int expectedativecount = 0;

			Assert.AreEqual(expectedseatcount, table.SeatCount);
			Assert.AreEqual(expectedplayercount, table.PlayerCount);
			Assert.AreEqual(expectedativecount, table.ActiveSeatCount);

			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNotNull(table.TableSeats);

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, null)));

		}


		[TestMethod]
		public void Texastable_Init1Seat_Init()
		{

			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 1 });
			int expectedseatcount = 1;
			int expectedplayercount = 0;
			int expectedativecount = 0;

			Assert.AreEqual(expectedseatcount, table.SeatCount);
			Assert.AreEqual(expectedplayercount, table.PlayerCount);
			Assert.AreEqual(expectedativecount, table.ActiveSeatCount);

			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNotNull(table.TableSeats);

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, null)));

			expectedplayercount = 1;
			expectedativecount = 0;

			Assert.AreEqual(expectedplayercount, table.PlayerCount);
			Assert.AreEqual(expectedativecount, table.ActiveSeatCount);


		}


		[TestMethod]
		public void Texastable_Init30Seat_Init()
		{

			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 30 });
			int expectedseatcount = 30;
			int expectedplayercount = 0;
			int expectedativecount = 0;

			Assert.AreEqual(expectedseatcount, table.SeatCount);
			Assert.AreEqual(expectedplayercount, table.PlayerCount);
			Assert.AreEqual(expectedativecount, table.ActiveSeatCount);

			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNotNull(table.TableSeats);

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, null)));

			expectedplayercount = 1;
			expectedativecount = 0;

			Assert.AreEqual(expectedplayercount, table.PlayerCount);
			Assert.AreEqual(expectedativecount, table.ActiveSeatCount);


		}

		[TestMethod]
		public void Texastable_JoinNull_false()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 5 });

			Assert.IsFalse(table.Join(null));

		}

		[TestMethod]
		public void Texastable_JoinPlayerFreeSeat_Success()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 5 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));

		}

		[TestMethod]
		public void Texastable_JoinPlayerNoFreeSeat_fail()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerRobot(null)));

		}


		[TestMethod]
		public void Texastable_JoinDealerPlayerNoSeats_Success()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 0 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null,null)));

		}

		[TestMethod]
		public void Texastable_JoinDealerNoFreeSeat_Success()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerRobot(null)));

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, null)));


		}


		[TestMethod]
		public void Texastable_JoinASecondDealer_Fail()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, null)));

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerDealer(null, null)));

		}


		[TestMethod]
		public void Texastable_PlayGameNoDealerNoPlayer_False()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 });

			Assert.IsFalse(table.PlayGame());

		}

		[TestMethod]
		public void Texastable_PlayGameNoPlayer_False()
		{
			var ui = new TexasHoldEmConIO();
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, ui)));

			Assert.IsFalse(table.PlayGame());

		}

		[TestMethod]
		public void Texastable_PlayGame1Player_False()
		{
			var ui = new TexasHoldEmConIO();
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, ui)));

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(config: null)));


			Assert.IsFalse(table.PlayGame());

		}

		[TestMethod]
		public void Texastable_PlayGame2Player_True()
		{
			var ui = new TexasHoldEmConIO();
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, ui)));

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(config: null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(config: null)));


			Assert.IsTrue(table.PlayGame());

		}


		[TestMethod]
		public void Texastable_PlayGameNoUI_False()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(config: null, UI: null)));

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(config: null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(config: null)));


			Assert.IsFalse(table.PlayGame());

		}


	}
}
