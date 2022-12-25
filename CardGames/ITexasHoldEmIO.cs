using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public interface ITexasHoldEmIO
	{
		void ShowMsg(string msg);

		void ShowNewRound(ICardGameTable table);

		void ShowTableSeatHands(ICardGameTable table);

		bool SupressOutput { get; set; }
	}
}
