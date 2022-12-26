namespace Games.Card
{
	public interface ICardPlayer
	{
		bool JoinTable(ICardGameTable table);

		void LeaveTable();

		string Name { get; }

		ITokenWallet Wallet { get; }

		public CardPlayerType PlayerType { get; }

		ICardPlayerProfile PlayerProfile { get; }

	}
}