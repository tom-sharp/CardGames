namespace Games.Card
{
	public interface ICardGameTableSeat
	{

		bool IsFree { get; }

		bool IsActive { get; }

		bool Join(CardPlayer player);

		bool Leave();

		void NewRound();

		void PlaceBet(int tokens);

		void RaiseBet(int tokens);

		void Fold();

		int CollectBet();

		void RollbackBet();

		void WinTokens(int tokens);



		int Bets { get; }

		string Comment { get; }



		ICardGamePlayerCards PlayerCards { get; }

		public ICardPlayer Player { get; }


		//string PlayerName { get; }

		//int PlayerTokens { get; }


	}
}