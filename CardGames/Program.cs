using Games.Card.TexasHoldEm;
using Syslib;

namespace CardGames
{
	class Program
	{
		static void Main(string[] args)
		{
			CSyslib.MinVersion(1,0,3);
			new Texas(UI: new TexasHoldEmConIO(), new TexasDbContext()).Run(args);
//			TestRank.Run();
		}
	}
}
