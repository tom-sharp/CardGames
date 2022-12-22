using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class CardPlayer
	{
		public CardPlayer(ICardPlayerProfile playerprofile = null, string name = null, int tokens = 0)
		{
			if (playerprofile == null) this.PlayerProfile = new CardPlayerProfileHuman(); else this.PlayerProfile = playerprofile;
			if (name != null) Name = name; else Name = "Player";
			Tokens = tokens;
		}


		public bool JoinTable(ICardGameTable table) {
			if (this.gametable != null) LeaveTable();
			if (table != null) this.tableseat = table.JoinTable(this); else this.tableseat = 0;
			if (this.tableseat > 0) return true;
			return false;
		}

		public void LeaveTable() {
			if ((this.gametable != null) &&(this.tableseat > 0)) this.gametable.LeaveTable(this.tableseat);
			this.gametable = null;
		}

		// Update token wallet, using + to add tokens to wallet or - to withdraw tokens from wallet
		public void UpdateTokenWallet(int tokens) {
			Tokens += tokens;
		}

		public string Name { get; }

		public int Tokens { get; private set; }

		public ICardPlayerProfile PlayerProfile { get; }


		int tableseat = 0;
		ICardGameTable gametable = null;
	}

}
