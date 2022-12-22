using System;
using Games.Card;
using Games.Card.Test;

namespace CardGames
{
	class Program
	{
		static void Main(string[] args)
		{

			var texas = new Texas();
			texas.Run(args);

			// TESTS
//			Tests.RunTests();

		}
	}
}
