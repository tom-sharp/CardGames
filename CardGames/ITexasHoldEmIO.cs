using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games.Card;

namespace Games.Card.TexasHoldEm
{
	public interface ITexasHoldEmIO
	{
		void ShowHelp();

		void ShowProgressMessage(string msg);

		void ShowNewRound(ICardGameTable table);

		void ShowRoundSummary(ICardGameTable table);

		void ShowGameStatistics(TexasHoldEmStatistics statistics);
		void ShowGamePlayerStatistics(CList<ICardPlayer> playerlist);

		void ShowPlayerCards(ICardGameTableSeat playerseat, ICardGameTableSeat dealerset);

		void ReDrawGameTable(ICardGameTable table);

		int AskForBet(int tokens);

		bool SupressOutput { get; set; }
		bool SupressOverrideRoundSummary { get; set; }
		bool SupressOverrideStatistics { get; set; }

	}
}
