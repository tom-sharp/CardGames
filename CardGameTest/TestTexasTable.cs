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
			int expectedseatcount = -1;
			int expectedplayercount = -1;
			int expectedativecount = -1;

			Assert.AreEqual(expectedseatcount, table.SeatCount);
			Assert.AreEqual(expectedplayercount, table.PlayerCount);
			Assert.AreEqual(expectedativecount, table.ActiveSeatCount);

			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNotNull(table.TableSeats);

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerRobot(null)));

		}



	}
}
