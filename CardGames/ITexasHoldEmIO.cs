using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games;
using Syslib.Games.Card;

namespace Games.Card.TexasHoldEm
{
	public interface ITexasHoldEmIO
	{
		bool Welcome();
		void ShowHelp();
		void ShowMsg(string msg);
		void ShowErrMsg(string msg);
		void ShowNewRound(ICardTable table);

		void ReDrawGameTable();
		void ShowProgressMessage(string msg);

		void ShowPlayerSeat(ICardTableSeat seat);
		void Finish();

		void ShowRoundSummary(ICardTable table, bool samepage);

		void ShowGameStatistics(TexasHoldEmStatistics statistics);
		void ShowGamePlayerStatistics(ICardTable table);


		int AskForBet(int tokens, int canraisetokens);
		bool AskPlayAnotherRound();

		bool SupressOutput { get; set; }
		bool SupressOverrideRoundSummary { get; set; }
		bool SupressOverrideStatistics { get; set; }

	}
}
