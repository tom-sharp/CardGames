using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games;
using Syslib.Games.Card;

namespace Games.Card.TexasHoldEm
{
	public interface ITexasHoldEmIO
	{
		void ShowHelp();
		void ShowMsg(string msg);
		void ShowNewRound(ICardTable table);

		void ReDrawGameTable();
		void ShowProgressMessage(string msg);

		void ShowPlayerSeat(ICardTableSeat seat);
		void ShowPlayerActiveSeat(ICardTableSeat seat);

		void Finish();

		void ShowRoundSummary(ICardTable table);

		void ShowGameStatistics(TexasHoldEmStatistics statistics);
		void ShowGamePlayerStatistics(ICardTable table);

		void ShowPlayerCards(ICardTableSeat playerseat, ICardTableSeat dealerset);


		int AskForBet(int tokens);

		bool SupressOutput { get; set; }
		bool SupressOverrideRoundSummary { get; set; }
		bool SupressOverrideStatistics { get; set; }

	}
}
