using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class CardPlayer
	{
		public CardPlayer(CardPlayerType playertype = CardPlayerType.Robot, string name = null, int tokens = 0)
		{
			if (name != null) Name = name; else Name = "Player";
			TableSeat = -1;
			Tokens = tokens;
			PlayerType = playertype;
		}

		// Update token wallet, using + to add tokens to wallet or - to withdraw tokens from wallet
		public void UpdateTokenWallet(int tokens) {
			Tokens += tokens;
		}

		// table seat number that player is using (used only join/leave table)
		public int TableSeat { get; set; }

		public string Name { get; }

		public int Tokens { get; private set; }

		public CardPlayerType PlayerType { get; }
	}

}
