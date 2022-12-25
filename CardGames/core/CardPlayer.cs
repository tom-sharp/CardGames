using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class CardPlayer : ICardPlayer
	{
		public CardPlayer(ICardPlayerProfile playerprofile = null, string name = null, ITokenWallet wallet = null)
		{
			if (playerprofile == null) this.PlayerProfile = new CardPlayerProfileHuman(); else this.PlayerProfile = playerprofile;
			if (name != null) Name = name; else Name = "Player";
			if (wallet != null) this.Wallet = wallet; else this.Wallet = new TokenWallet();
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


		public string Name { get; }


		public ITokenWallet Wallet { get; }

		public ICardPlayerProfile PlayerProfile { get; }


		int tableseat = 0;
		ICardGameTable gametable = null;
	}

}
