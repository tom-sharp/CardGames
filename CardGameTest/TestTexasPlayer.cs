using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Card.TexasHoldEm;
using Syslib.Games.Card;
using Syslib.Games;


namespace CardGameTest
{
	[TestClass]
	public class TestTexasPlayer
	{
		[TestMethod]
		public void TexasPlayer_NullInit_DefaultInit() {

			var player = new TexasHoldEmPlayerRobot(null);

			string expectedName = "Robot";
			GamePlayerType expectedType = GamePlayerType.Robot;
			int expectedTokens = 0;

			string expectedprofilename = "Default";
			int expectedDefensive = 0;
			int expectedOffensive = 0;
			int expectedRandom = 0;

			Assert.AreEqual(expectedName, player.Name);
			Assert.AreEqual(expectedType, player.Type);
			Assert.IsNotNull(player.Cards);
			Assert.IsNotNull(player.Profile);
			Assert.AreEqual(expectedTokens, player.Tokens);

			Assert.AreEqual(expectedprofilename, player.Profile.Name);
			Assert.AreEqual(expectedDefensive, player.Profile.Defensive);
			Assert.AreEqual(expectedOffensive, player.Profile.Offensive);
			Assert.AreEqual(expectedRandom, player.Profile.Randomness);

		}

		[TestMethod]
		public void TexasPlayer_DefaultInit_DefaultInit()
		{

			var config = new CardPlayerConfig();
			var player = new TexasHoldEmPlayerRobot(config);

			string expectedName = "Robot";
			GamePlayerType expectedType = GamePlayerType.Robot;
			int expectedTokens = 0;

			string expectedprofilename = "Default";
			int expectedDefensive = 0;
			int expectedOffensive = 0;
			int expectedRandom = 0;


			Assert.AreEqual(expectedName, player.Name);
			Assert.AreEqual(expectedType, player.Type);
			Assert.IsNotNull(player.Cards);
			Assert.IsNotNull(player.Profile);
			Assert.AreEqual(expectedTokens, player.Tokens);

			Assert.AreEqual(expectedprofilename, player.Profile.Name);
			Assert.AreEqual(expectedDefensive, player.Profile.Defensive);
			Assert.AreEqual(expectedOffensive, player.Profile.Offensive);
			Assert.AreEqual(expectedRandom, player.Profile.Randomness);

		}

		[TestMethod]
		public void TexasPlayer_DefaultProfileInit_DefaultProfileInit()
		{

			var config = new CardPlayerConfig() {Name = "Player",Tokens = 10 };
			var player = new TexasHoldEmPlayerRobot(config);

			string expectedName = "Player";
			GamePlayerType expectedType = GamePlayerType.Robot;
			int expectedTokens = 10;

			string expectedprofilename = "Default";
			int expectedDefensive = 0;
			int expectedOffensive = 0;
			int expectedRandom = 0;

			Assert.AreEqual(expectedName, player.Name);
			Assert.AreEqual(expectedType, player.Type);
			Assert.IsNotNull(player.Cards);
			Assert.IsNotNull(player.Profile);
			Assert.AreEqual(expectedTokens, player.Tokens);

			Assert.AreEqual(expectedprofilename, player.Profile.Name);
			Assert.AreEqual(expectedDefensive, player.Profile.Defensive);
			Assert.AreEqual(expectedOffensive, player.Profile.Offensive);
			Assert.AreEqual(expectedRandom, player.Profile.Randomness);

		}

		[TestMethod]
		public void TexasPlayer_NullProfileInit_DefaultProfileInit()
		{

			var config = new CardPlayerConfig() { Name = "Player", Tokens = 10, PlayerProfile = null };
			var player = new TexasHoldEmPlayerRobot(config);

			string expectedName = "Player";
			GamePlayerType expectedType = GamePlayerType.Robot;
			int expectedTokens = 10;

			string expectedprofilename = "Default";
			int expectedDefensive = 0;
			int expectedOffensive = 0;
			int expectedRandom = 0;

			Assert.AreEqual(expectedName, player.Name);
			Assert.AreEqual(expectedType, player.Type);
			Assert.IsNotNull(player.Cards);
			Assert.IsNotNull(player.Profile);
			Assert.AreEqual(expectedTokens, player.Tokens);

			Assert.AreEqual(expectedprofilename, player.Profile.Name);
			Assert.AreEqual(expectedDefensive, player.Profile.Defensive);
			Assert.AreEqual(expectedOffensive, player.Profile.Offensive);
			Assert.AreEqual(expectedRandom, player.Profile.Randomness);

		}


		[TestMethod]
		public void TexasPlayer_CustomProfileInit_CustomProfileInit()
		{
			var profile = new GamePlayerProfile() { Defensive = 40, Offensive = 50, Randomness = 60 };
			var config = new CardPlayerConfig() { Name = "Player", Tokens = 10, PlayerProfile = profile };
			var player = new TexasHoldEmPlayerRobot(config);

			string expectedName = "Player";
			GamePlayerType expectedType = GamePlayerType.Robot;
			int expectedTokens = 10;

			string expectedprofilename = "Custom";
			int expectedDefensive = 40;
			int expectedOffensive = 50;
			int expectedRandom = 60;

			Assert.AreEqual(expectedName, player.Name);
			Assert.AreEqual(expectedType, player.Type);
			Assert.IsNotNull(player.Cards);
			Assert.IsNotNull(player.Profile);
			Assert.AreEqual(expectedTokens, player.Tokens);

			Assert.AreEqual(expectedprofilename, player.Profile.Name);
			Assert.AreEqual(expectedDefensive, player.Profile.Defensive);
			Assert.AreEqual(expectedOffensive, player.Profile.Offensive);
			Assert.AreEqual(expectedRandom, player.Profile.Randomness);

		}

		[TestMethod]
		public void TexasPlayer_InvalidInit_DefaultInit()
		{
			var profile = new GamePlayerProfile() { Defensive = -4000, Offensive = 5000, Randomness = -6000 };
			var config = new CardPlayerConfig() { Name = null, Tokens = -10, PlayerProfile = profile };
			var player = new TexasHoldEmPlayerRobot(config);

			string expectedName = "Robot";
			GamePlayerType expectedType = GamePlayerType.Robot;
			int expectedTokens = -10;

			string expectedprofilename = "Custom";
			int expectedDefensive = 0;
			int expectedOffensive = 100;
			int expectedRandom = 0;

			Assert.AreEqual(expectedName, player.Name);
			Assert.AreEqual(expectedType, player.Type);
			Assert.IsNotNull(player.Cards);
			Assert.IsNotNull(player.Profile);
			Assert.AreEqual(expectedTokens, player.Tokens);

			Assert.AreEqual(expectedprofilename, player.Profile.Name);
			Assert.AreEqual(expectedDefensive, player.Profile.Defensive);
			Assert.AreEqual(expectedOffensive, player.Profile.Offensive);
			Assert.AreEqual(expectedRandom, player.Profile.Randomness);

		}


		[TestMethod]
		public void TexasPlayer_ConfigReferenceSharing_NoSharing()
		{
			var profile = new GamePlayerProfile() { Defensive = 40, Offensive = 50, Randomness = 0 };
			var config = new CardPlayerConfig() { Name = "Robot", Tokens = 10, PlayerProfile = profile };

			var player1 = new TexasHoldEmPlayerRobot(config);
			var player2 = new TexasHoldEmPlayerRobot(config);
			Assert.IsFalse(ReferenceEquals(player1.Profile, player2.Profile));
		}



	}
}
