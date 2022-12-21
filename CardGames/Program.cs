using System;
using Games.Card;

namespace CardGames
{
	class Program
	{
		static void Main(string[] args)
		{

			// TEXAS
			var texastable = new TexasHoldEmTable(tableseats: 8);
			texastable.JoinTable(new CardPlayer(name: "Player1", tokens: 1000));
			texastable.JoinTable(new CardPlayer(name: "Player2", tokens: 1000));
			texastable.JoinTable(new CardPlayer(name: "Player3", tokens: 1000));
			texastable.JoinTable(new CardPlayer(name: "Player4", tokens: 1000));
			texastable.JoinTable(new CardPlayer(name: "Player5", tokens: 1000));
			texastable.LeaveTable(seat: 2);
			texastable.Run();

			// TESTS
			Tests.RunTests();

		}
	}
}
