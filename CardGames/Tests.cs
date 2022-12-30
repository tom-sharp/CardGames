using Syslib;
using Syslib.Games.Card;
using System;
using Games.Card;
using Games.Card.TexasHoldEm;

namespace Games.Card.Test
{
	public class Tests
	{

		public int RunTests() {
			int result = 0;

			Console.WriteLine("\n---------------------------------------------- \n Running Tests..");


			Console.WriteLine("\n");
			if (result == 0) Console.WriteLine("\n All Tests Success"); else Console.WriteLine($"\n Tests result in {result} Error(s)");
			Console.WriteLine("----------------------------------------------");
			return result;
		}
	}
}
