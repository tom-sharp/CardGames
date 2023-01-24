using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm.Models
{
	public class TexasTableRoundInfo
	{
		public int NumberOfPlayers { get; }
		public int ActivePlayers { get; }
		public int TokensToCall { get; }
		public int TokensMaxRaise { get; }

	}
}
