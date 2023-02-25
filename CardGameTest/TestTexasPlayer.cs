using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Card.TexasHoldEm;
using Syslib.Games.Card.TexasHoldEm;
using Syslib.Games;


namespace CardGameTest
{
	[TestClass]
	public class TestTexasPlayer
	{
		[TestMethod]
		public void TexasPlayerAi_NullInit_InTurnResponseFalse() {

			var texasplayer = new TexasHoldEmPlayerAi(ai: null, player: null);

			var expectedType = GamePlayerType.Invalid;
			var expectedTokens = 0;
			var expectedprofilename = "Default";
			var expectedname = "Player";
			var expectedInTurResponse = false;

			var actualtype = texasplayer.Type;
			var actualid = texasplayer.Id;
			var actualname = texasplayer.Name;
			var actualtokens = texasplayer.Tokens;
			var actualprofilename = texasplayer.Profile.Name;
			var actualcards = texasplayer.Cards;
			var actualInTurResponse = texasplayer.InTurn(new TexasHoldEmTurnInfo() );


			Assert.AreEqual(expectedType, actualtype);
			Assert.IsNotNull(actualid);
			Assert.IsNotNull(actualcards);

			Assert.AreEqual(expectedname, actualname);
			Assert.AreEqual(expectedTokens, actualtokens);
			Assert.AreEqual(expectedprofilename, actualprofilename);
			Assert.AreEqual(expectedInTurResponse, actualInTurResponse);

		}

		[TestMethod]
		public void TexasPlayerAi_PlayerAiInit_InTurnResponseTrue()
		{
			var player = new Player() { Type = GamePlayerType.Ai, Name = "AiPlayer", Tokens = 10 };
			var ai = new TexasHoldEmAi();
			var texasplayer = new TexasHoldEmPlayerAi(ai: ai, player: player);

			var expectedType = GamePlayerType.Ai;
			var expectedTokens = 10;
			var expectedprofilename = "Default";
			var expectedname = "AiPlayer";
			var expectedInTurnResponse = true;

			var actualtype = texasplayer.Type;
			var actualid = texasplayer.Id;
			var actualname = texasplayer.Name;
			var actualtokens = texasplayer.Tokens;
			var actualprofilename = texasplayer.Profile.Name;
			var actualcards = texasplayer.Cards;
			var actualInTurnResponse = texasplayer.InTurn(new TexasHoldEmTurnInfo());

			Assert.AreEqual(expectedType, actualtype);
			Assert.IsNotNull(actualid);
			Assert.IsNotNull(actualcards);

			Assert.AreEqual(expectedname, actualname);
			Assert.AreEqual(expectedTokens, actualtokens);
			Assert.AreEqual(expectedprofilename, actualprofilename);
			Assert.AreEqual(expectedInTurnResponse, actualInTurnResponse);

		}


		[TestMethod]
		public void TexasPlayerAi_NotAiPlayer_InTurnResponseFalse()
		{
			var player = new Player() { Type = GamePlayerType.Human, Name = "AiPlayer", Tokens = 10 };
			var ai = new TexasHoldEmAi();
			var texasplayer = new TexasHoldEmPlayerAi(ai: ai, player: player);

			var expectedType = GamePlayerType.Human;
			var expectedTokens = 10;
			var expectedprofilename = "Default";
			var expectedname = "AiPlayer";
			var expectedInTurResponse = false;

			var actualtype = texasplayer.Type;
			var actualid = texasplayer.Id;
			var actualname = texasplayer.Name;
			var actualtokens = texasplayer.Tokens;
			var actualprofilename = texasplayer.Profile.Name;
			var actualcards = texasplayer.Cards;
			var actualInTurResponse = texasplayer.InTurn(new TexasHoldEmTurnInfo());

			Assert.AreEqual(expectedType, actualtype);
			Assert.IsNotNull(actualid);
			Assert.IsNotNull(actualcards);

			Assert.AreEqual(expectedname, actualname);
			Assert.AreEqual(expectedTokens, actualtokens);
			Assert.AreEqual(expectedprofilename, actualprofilename);
			Assert.AreEqual(expectedInTurResponse, actualInTurResponse);

		}


	}
}
