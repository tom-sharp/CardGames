using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games.Card.TexasHoldEm;
using Syslib.Games.Card;


namespace CardGameTest
{
	[TestClass]
	public class TestTexasTable
	{
		[TestMethod]
		public void Texastable_nullInit_Init() {

			var table = new TexasHoldEmTable(null,null);
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
		public void Texastable_InitDeafult0Seat_Init()
		{

			var table = new TexasHoldEmTable(new CardTableConfig(), null);
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
		public void Texastable_InitNegativSeat_Init()
		{

			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = -1}, null);
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

			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 1 }, null);
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

			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 30 }, null);
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
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 5 }, null);

			Assert.IsFalse(table.Join(null));

		}

		[TestMethod]
		public void Texastable_JoinPlayerFreeSeat_Success()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 5 }, null);

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));

		}

		[TestMethod]
		public void Texastable_JoinPlayerNoFreeSeat_fail()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 }, null);

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerRobot(null)));

		}


		[TestMethod]
		public void Texastable_JoinDealerPlayerNoSeats_Success()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 0 }, null);

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null,null)));

		}

		[TestMethod]
		public void Texastable_JoinDealerNoFreeSeat_Success()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 }, null);

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerRobot(null)));

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerRobot(null)));

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, null)));


		}


		[TestMethod]
		public void Texastable_JoinASecondDealer_Fail()
		{
			var table = new TexasHoldEmTable(new CardTableConfig() { Seats = 3 }, null);

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDealer(null, null)));

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerDealer(null, null)));

		}






	}
}
