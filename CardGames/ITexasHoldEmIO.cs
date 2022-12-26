using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Card.TexasHoldEm;
using Syslib;

namespace Games.Card
{
	public interface ITexasHoldEmIO
	{
		void ShowHelp();

		void ShowProgressMessage(string msg);

		void ShowNewRound(ICardGameTable table);

		void ShowRoundSummary(ICardGameTable table);

		void ShowGameStatistics(TexasHoldEmStatistics statistics);
		void ShowGamePlayerStatistics(CList<CardPlayer> playerlist);

		void ShowPlayerCards(ICardGameTableSeat playerseat, ICardGameTableSeat dealerset);

		bool SupressOutput { get; set; }
		bool SupressOverrideRoundSummary { get; set; }
		bool SupressOverrideStatistics { get; set; }

	}
}
