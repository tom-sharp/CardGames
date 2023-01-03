using Games.Card.TexasHoldEm;

namespace CardGames
{
	class Program
	{
		static void Main(string[] args)
		{
			new Texas(UI: new TexasHoldEmConIO()).Run(args);
		}
	}
}
