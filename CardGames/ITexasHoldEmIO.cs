using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games;
using Syslib.Games.Card;

namespace Games.Card.TexasHoldEm
{
	public interface ITexasHoldEmIO
	{
		void ShowHelp();

		void ShowProgressMessage(string msg);

		void ShowNewRound(ICardTable table);

		void ShowRoundSummary(ICardTable table);

		void ShowGameStatistics(TexasHoldEmStatistics statistics);
		void ShowGamePlayerStatistics(ICardTable table);

		void ShowPlayerCards(ICardTableSeat playerseat, ICardTableSeat dealerset);

		void ReDrawGameTable(ICardTable table);

		int AskForBet(int tokens);

		bool SupressOutput { get; set; }
		bool SupressOverrideRoundSummary { get; set; }
		bool SupressOverrideStatistics { get; set; }

	}
}
