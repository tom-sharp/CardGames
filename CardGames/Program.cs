using Games.Card.TexasHoldEm.Models;
using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Data;

namespace CardGames
{
	class Program
	{
		static void Main(string[] args)
		{
			CSyslib.MinVersion(1,0,3);
			new Texas(ui: new TexasHoldEmConUI(), new TexasDbContext())
				.Setup(arguments: args)
				.Run();
		}
	}
}
