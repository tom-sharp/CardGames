using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games.Card.TexasHoldEm;
using Syslib.Games;
using Syslib.Games.Card.TexasHoldEm;
using Syslib;

namespace CardGameTest
{


	class MockUI : ITexasHoldEmUI
	{
		public bool SupressOutput { get; set; }

		public bool SupressOverrideRoundSummary { get; set; }
		public bool SupressOverrideStatistics { get; set; }

		public int AskForBet(int tokens, int canraisetokens)
		{
			return 0;
		}

		public int AskMainMenu(IForEach<Syslib.BaseInterfaces.ISelectItem> list)
		{
			return 0;
		}

		public bool AskPlayNext()
		{
			return true;
		}

		public void DealFlop()
		{
			return;
		}

		public void DealRiver()
		{
			return;
		}

		public void DealShowDown()
		{
			return;
		}

		public void DealTurn()
		{
			return;
		}

		public void Finish()
		{
			return;
		}

		public void ReDrawGameTable()
		{
			return;
		}

		public void ShowErrMsg(string msg)
		{
			return;
		}

		public void ShowGamePlayerStatistics(TexasHoldEmTable table)
		{
			return;
		}

		public void ShowGameStatistics(TexasHoldEmStatistics statistics)
		{
			return;
		}

		public void ShowHelp()
		{
			return;
		}

		public void ShowMsg(string msg)
		{
			return;
		}

		public void ShowNewRound(TexasHoldEmTable table)
		{
			return;
		}

		public void ShowPlayerAction(ITexasHoldEmSeat seat)
		{
			return;
		}

		public void ShowPlayerSeat(ITexasHoldEmSeat seat)
		{
			return;
		}

		public void ShowPlayerSummary()
		{
			return;
		}

		public void ShowProgress(double progress, double complete)
		{
			return;
		}

		public bool ShowRoundSummary(TexasHoldEmTable table)
		{
			return true;
		}

		public bool Welcome()
		{
			return true;
		}
	}

	[TestClass]
	public class TestTexasTable
	{
		[TestMethod]
		public void Texastable_nullInit_Default1SeatInit() {

			var table = new TexasHoldEmTable(null);

			var expectedseatcount = 1;
			var expectedfreeseatcount = 1;
			var expectedplayercount = 0;
			var expectedativecount = 0;

			var actualseatcount = table.SeatCount;
			var actualfreeseatcount = table.FreeSeatCount;
			var actualplayercount = table.PlayerCount;
			var actualativecount = table.ActiveSeatCount;


			Assert.AreEqual(expectedseatcount, actualseatcount);
			Assert.AreEqual(expectedfreeseatcount, actualfreeseatcount);
			Assert.AreEqual(expectedplayercount, actualplayercount);
			Assert.AreEqual(expectedativecount, actualativecount);


			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNull(table.NextActivePlayerSeat(null));
			Assert.IsNotNull(table.NextSeat(null));
		}


		[TestMethod]
		public void Texastable_Init0Seats_Default1SeatInit()
		{

			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 0 });

			var expectedseatcount = 1;
			var expectedfreeseatcount = 1;
			var expectedplayercount = 0;
			var expectedativecount = 0;

			var actualseatcount = table.SeatCount;
			var actualfreeseatcount = table.FreeSeatCount;
			var actualplayercount = table.PlayerCount;
			var actualativecount = table.ActiveSeatCount;


			Assert.AreEqual(expectedseatcount, actualseatcount);
			Assert.AreEqual(expectedfreeseatcount, actualfreeseatcount);
			Assert.AreEqual(expectedplayercount, actualplayercount);
			Assert.AreEqual(expectedativecount, actualativecount);


			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNull(table.NextActivePlayerSeat(null));
			Assert.IsNotNull(table.NextSeat(null));
		}


		[TestMethod]
		public void Texastable_InitNegativeSeats_Default1SeatInit()
		{

			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = -1 });

			var expectedseatcount = 1;
			var expectedfreeseatcount = 1;
			var expectedplayercount = 0;
			var expectedativecount = 0;

			var actualseatcount = table.SeatCount;
			var actualfreeseatcount = table.FreeSeatCount;
			var actualplayercount = table.PlayerCount;
			var actualativecount = table.ActiveSeatCount;


			Assert.AreEqual(expectedseatcount, actualseatcount);
			Assert.AreEqual(expectedfreeseatcount, actualfreeseatcount);
			Assert.AreEqual(expectedplayercount, actualplayercount);
			Assert.AreEqual(expectedativecount, actualativecount);


			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNull(table.NextActivePlayerSeat(null));
			Assert.IsNotNull(table.NextSeat(null));
		}

		[TestMethod]
		public void Texastable_Init8Seats_SeatInit()
		{

			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 8 });

			var expectedseatcount = 9;
			var expectedfreeseatcount = 9;
			var expectedplayercount = 0;
			var expectedativecount = 0;

			var actualseatcount = table.SeatCount;
			var actualfreeseatcount = table.FreeSeatCount;
			var actualplayercount = table.PlayerCount;
			var actualativecount = table.ActiveSeatCount;


			Assert.AreEqual(expectedseatcount, actualseatcount);
			Assert.AreEqual(expectedfreeseatcount, actualfreeseatcount);
			Assert.AreEqual(expectedplayercount, actualplayercount);
			Assert.AreEqual(expectedativecount, actualativecount);


			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNull(table.NextActivePlayerSeat(null));
			Assert.IsNotNull(table.NextSeat(null));
		}

		[TestMethod]
		public void Texastable_Init30Seats_SeatInit()
		{

			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 30 });

			var expectedseatcount = 31;
			var expectedfreeseatcount = 31;

			var actualseatcount = table.SeatCount;
			var actualfreeseatcount = table.FreeSeatCount;

			Assert.AreEqual(expectedseatcount, actualseatcount);
			Assert.AreEqual(expectedfreeseatcount, actualfreeseatcount);
		}




		[TestMethod]
		public void Texastable_InvalidUserJoinTable_Fail()
		{

			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 8 });
			var player = new TexasHoldEmPlayerAi(null, null);

			var expectedseatcount = 9;
			var expectedfreeseatcount = 9;
			var expectedplayercount = 0;
			var expectedativecount = 0;
			var expectedjoinresult = false;

			var actualjoinresult = table.Join(player);
			var actualseatcount = table.SeatCount;
			var actualfreeseatcount = table.FreeSeatCount;
			var actualplayercount = table.PlayerCount;
			var actualativecount = table.ActiveSeatCount;


			Assert.AreEqual(expectedjoinresult, actualjoinresult);
			Assert.AreEqual(expectedseatcount, actualseatcount);
			Assert.AreEqual(expectedfreeseatcount, actualfreeseatcount);
			Assert.AreEqual(expectedplayercount, actualplayercount);
			Assert.AreEqual(expectedativecount, actualativecount);


			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNull(table.NextActivePlayerSeat(null));
			Assert.IsNotNull(table.NextSeat(null));
		}

		[TestMethod]
		public void Texastable_InCompatibleUserJoinTable_Success()
		{

			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 8 });
			var player = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Human });

			var expectedseatcount = 9;
			var expectedfreeseatcount = 8;
			var expectedplayercount = 1;
			var expectedativecount = 0;
			var expectedjoinresult = true;

			var actualjoinresult = table.Join(player);
			var actualseatcount = table.SeatCount;
			var actualfreeseatcount = table.FreeSeatCount;
			var actualplayercount = table.PlayerCount;
			var actualativecount = table.ActiveSeatCount;


			Assert.AreEqual(expectedjoinresult, actualjoinresult);
			Assert.AreEqual(expectedseatcount, actualseatcount);
			Assert.AreEqual(expectedfreeseatcount, actualfreeseatcount);
			Assert.AreEqual(expectedplayercount, actualplayercount);
			Assert.AreEqual(expectedativecount, actualativecount);


			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNull(table.NextActivePlayerSeat(null));
			Assert.IsNotNull(table.NextSeat(null));
		}

		[TestMethod]
		public void Texastable_ValidUserJoinTable_Success()
		{

			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 8 });
			var player = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Ai });

			var expectedseatcount = 9;
			var expectedfreeseatcount = 8;
			var expectedplayercount = 1;
			var expectedativecount = 0;
			var expectedjoinresult = true;

			var actualjoinresult = table.Join(player);
			var actualseatcount = table.SeatCount;
			var actualfreeseatcount = table.FreeSeatCount;
			var actualplayercount = table.PlayerCount;
			var actualativecount = table.ActiveSeatCount;


			Assert.AreEqual(expectedjoinresult, actualjoinresult);
			Assert.AreEqual(expectedseatcount, actualseatcount);
			Assert.AreEqual(expectedfreeseatcount, actualfreeseatcount);
			Assert.AreEqual(expectedplayercount, actualplayercount);
			Assert.AreEqual(expectedativecount, actualativecount);


			Assert.IsNull(table.NextActiveSeat(null));
			Assert.IsNull(table.NextActivePlayerSeat(null));
			Assert.IsNotNull(table.NextSeat(null));
		}

		[TestMethod]
		public void Texastable_ActivateInvalidUser_Fail()
		{

			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 8 });
			var player1 = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Ai });
			var player2 = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Human });
			var player3 = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Ai });
			var player4 = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Ai });

			var expectedseatcount = 9;
			var expectedfreeseatcount = 5;
			var expectedplayercount = 4;
			var expectedativecount = 3;

			table.Join(player1);
			table.Join(player2);
			table.Join(player3);
			table.Join(player4);

			table.ForEach(seat=> seat.GetReady());

			var actualseatcount = table.SeatCount;
			var actualfreeseatcount = table.FreeSeatCount;
			var actualplayercount = table.PlayerCount;
			var actualativecount = table.ActiveSeatCount;


			Assert.AreEqual(expectedseatcount, actualseatcount);
			Assert.AreEqual(expectedfreeseatcount, actualfreeseatcount);
			Assert.AreEqual(expectedplayercount, actualplayercount);
			Assert.AreEqual(expectedativecount, actualativecount);


			Assert.IsNotNull(table.NextActiveSeat(null));
			Assert.IsNotNull(table.NextActivePlayerSeat(null));
			Assert.IsNotNull(table.NextSeat(null));
		}

		[TestMethod]
		public void Texastable_ActivateUsers_Success()
		{

			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 8 });
			var player1 = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Ai });
			var player2 = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Ai });
			var player3 = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Ai });
			var player4 = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Ai });

			var expectedseatcount = 9;
			var expectedfreeseatcount = 5;
			var expectedplayercount = 4;
			var expectedativecount = 4;

			table.Join(player1);
			table.Join(player2);
			table.Join(player3);
			table.Join(player4);

			table.ForEach(seat => seat.GetReady());

			var actualseatcount = table.SeatCount;
			var actualfreeseatcount = table.FreeSeatCount;
			var actualplayercount = table.PlayerCount;
			var actualativecount = table.ActiveSeatCount;


			Assert.AreEqual(expectedseatcount, actualseatcount);
			Assert.AreEqual(expectedfreeseatcount, actualfreeseatcount);
			Assert.AreEqual(expectedplayercount, actualplayercount);
			Assert.AreEqual(expectedativecount, actualativecount);


			Assert.IsNotNull(table.NextActiveSeat(null));
			Assert.IsNotNull(table.NextActivePlayerSeat(null));
			Assert.IsNotNull(table.NextSeat(null));
		}












		[TestMethod]
		public void Texastable_JoinNull_false()
		{
			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 5 });

			Assert.IsFalse(table.Join(null));

		}

		[TestMethod]
		public void Texastable_JoinPlayerFreeSeat_Success()
		{
			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 5 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerAi(null, new Player() { Type = GamePlayerType.Ai})));

		}

		[TestMethod]
		public void Texastable_JoinPlayerFreeSeatNoDefaultPlayer_fail()
		{
			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 3 });

			var expectedfreeset = 1;

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerAi(null, new Player() { Type = GamePlayerType.Ai })));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerAi(null, new Player() { Type = GamePlayerType.Ai })));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerAi(null, new Player() { Type = GamePlayerType.Ai })));
			Assert.IsFalse(table.Join(new TexasHoldEmPlayerAi(null, new Player() { Type = GamePlayerType.Ai })));

			var actualfreeseat = table.FreeSeatCount;

			Assert.AreEqual(expectedfreeset, actualfreeseat);
		}


		[TestMethod]
		public void Texastable_JoinDefaultPlayerNoSeats_Success()
		{
			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 0 });

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerAi(null, new Player() { Type = GamePlayerType.Ai })));
			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDefault(null, null, null)));

		}




		[TestMethod]
		public void Texastable_JoinASecondDefaultPlayer_Fail()
		{
			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 3 });

			Assert.IsTrue(table.Join(new TexasHoldEmPlayerDefault(null, null, null)));

			Assert.IsFalse(table.Join(new TexasHoldEmPlayerDefault(null, null, null)));

		}


		[TestMethod]
		public void Texastable_PlayGameNoDealerNoPlayer_False()
		{
			var table = new TexasHoldEmTable(new TexasHoldEmConfig() { Seats = 3 , RoundsToPlay = 1});

			Assert.IsFalse(table.PlayGame());

		}



		[TestMethod]
		public void Texastable_PlayGameNoPlayer_False()
		{
			var ui = new MockUI();
			var config = new TexasHoldEmConfig() { Seats = 3, RoundsToPlay = 1 };
			var table = new TexasHoldEmTable(config);
			var defaultplayer = new TexasHoldEmPlayerDefault(table,config,ui);

			Assert.IsTrue(table.Join(defaultplayer));

			Assert.IsFalse(table.PlayGame());

		}

		[TestMethod]
		public void Texastable_PlayGame1Player_False()
		{
			var ui = new MockUI();
			var config = new TexasHoldEmConfig() { Seats = 3, RoundsToPlay = 1 };
			var table = new TexasHoldEmTable(config);
			var defaultplayer = new TexasHoldEmPlayerDefault(table, config, ui);
			var player1 = new TexasHoldEmPlayerAi(new TexasHoldEmAi(), new Player() { Type = GamePlayerType.Ai});

			Assert.IsTrue(table.Join(defaultplayer));
			Assert.IsTrue(table.Join(player1));

			Assert.IsFalse(table.PlayGame());

		}

		[TestMethod]
		public void Texastable_PlayGame3Player_Success()
		{
			var ui = new MockUI();
			var ai = new TexasHoldEmAi();
			var config = new TexasHoldEmConfig() { Seats = 3, RoundsToPlay = 1 };
			var table = new TexasHoldEmTable(config);
			var defaultplayer = new TexasHoldEmPlayerDefault(table, config, ui);
			var player1 = new TexasHoldEmPlayerAi(ai, new Player() { Type = GamePlayerType.Ai });
			var player2 = new TexasHoldEmPlayerAi(ai, new Player() { Type = GamePlayerType.Ai });
			var player3 = new TexasHoldEmPlayerAi(ai, new Player() { Type = GamePlayerType.Ai });

			Assert.IsTrue(table.Join(defaultplayer));
			Assert.IsTrue(table.Join(player1));
			Assert.IsTrue(table.Join(player2));
			Assert.IsTrue(table.Join(player3));

			Assert.IsTrue(table.PlayGame());

		}




	}
}
