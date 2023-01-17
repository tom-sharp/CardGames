using CardGames;
using Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasQuery
{
	public class TexasQ
	{
		public void Run(string[] args) {
			new Texas(UI: new TexasHoldEmConIO(), new TexasDbContext())
				.Setup(arguments: args)
				.Run();
		}
	}
}
