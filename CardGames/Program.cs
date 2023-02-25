using Games.Card.TexasHoldEm.Models;
using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Data;
using System.Threading.Tasks;

namespace CardGames
{
	class Program
	{
		static void Main(string[] args)
		{
			new Menu(ui: new TexasHoldEmConUI()).Run();
		}
	}
}
