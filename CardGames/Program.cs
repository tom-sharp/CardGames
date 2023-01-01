using Games.Card.TexasHoldEm;

namespace CardGames
{
	class Program
	{
		static void Main(string[] args)
		{
			var ui = new TexasHoldEmConIO();
			new Texas(ui).Run(args);
		}
	}
}
