

namespace Games.Card
{
	public abstract class CardPlayer : ICardPlayer
	{
		public CardPlayer(CardPlayerType playertype, ICardPlayerProfile profile)
		{
			this.Name = "Player";
			this.Wallet = new TokenWallet();
			this.PlayerType = playertype;
			if (profile != null) this.PlayerProfile = profile; else this.PlayerProfile = new CardPlayerProfile();
		}


		public bool JoinTable(ICardGameTable table)
		{
			if (this.gametable != null) LeaveTable();
			if (table != null) this.tableseat = table.JoinTable(this); else this.tableseat = 0;
			if (this.tableseat > 0) return true;
			return false;
		}

		public void LeaveTable()
		{
			if ((this.gametable != null) && (this.tableseat > 0)) this.gametable.LeaveTable(this.tableseat);
			this.gametable = null;
		}


		public string Name { get; protected set; }


		public ITokenWallet Wallet { get; protected set; }


		public CardPlayerType PlayerType { get; }

		public ICardPlayerProfile PlayerProfile { get; }


		int tableseat = 0;
		ICardGameTable gametable = null;
	}

}
