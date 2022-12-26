using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	class CardPlayerDealer : CardPlayer
	{
		public CardPlayerDealer(string name = null, ITokenWallet wallet = null) : base(CardPlayerType.Dealer, null)
		{
			if (name != null) this.Name = name; else this.Name = "Dealer";
			if (wallet != null) this.Wallet = wallet;
		}

	}
}
