using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;
using CardGames;

namespace Games.Card.TexasHoldEm
{
	public interface ITexasHoldEmUI : ICardGamesMenuUI
	{
		void ShowHelp();
		void ShowNewRound(TexasHoldEmTable table);
		void ShowProgress(double progress, double complete);
		void ReDrawGameTable();

		void ShowPlayerSeat(ITexasHoldEmSeat seat);
		void ShowPlayerAction(ITexasHoldEmSeat seat, int msdelay);
		void ShowPlayerSummary();

		void DealFlop();
		void DealRiver();
		void DealTurn();
		void DealShowDown();



		bool ShowRoundSummary(TexasHoldEmTable table);

		void ShowGameStatistics(TexasHoldEmStatistics statistics);
		void ShowGamePlayerStatistics(TexasHoldEmTable table);


		int AskForBet(int tokens, int canraisetokens);
		bool AskPlayNext();


		bool SupressOutput { get; set; }
		bool SupressOverrideRoundSummary { get; set; }
		bool SupressOverrideStatistics { get; set; }

	}
}
