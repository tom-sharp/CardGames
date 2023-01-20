using CardGames;
using Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Models;
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
			new Texas(ui: new TexasHoldEmConUI(), new TexasDbContext())
				.Setup(arguments: args)
				.Run();
		}
	}
}
