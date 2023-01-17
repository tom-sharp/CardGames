using Syslib;
using Syslib.Games.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm.Misc
{
	public static class UpdatePlayCardSymbols
	{
		public static void SetNewAsciiCardSymbols(IPlayCardStack cardstack) {

			IPlayCard card = cardstack.NextCard(firstcard: true);
			while (card != null) {
				PlayCard.SetSymbolUTF8Suit(card);
				card = cardstack.NextCard();
			}

		}
	}
}
