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

			var str = new CStr();
			IPlayCard card = cardstack.NextCard(firstcard: true);
			while (card != null && card.Symbol != null && card.Symbol.Length >= 2) {
				if (card.Suit == PlayCardSuit.Heart) card.Symbol = str.Str(card.Symbol).Set(1, 3).ToString();
				else if (card.Suit == PlayCardSuit.Diamond) card.Symbol = str.Str(card.Symbol).Set(1, 4).ToString();
				else if (card.Suit == PlayCardSuit.Spade) card.Symbol = str.Str(card.Symbol).Set(1, 6).ToString();
				else if (card.Suit == PlayCardSuit.Club) card.Symbol = str.Str(card.Symbol).Set(1, 5).ToString();
				card = cardstack.NextCard();
			}

		}
	}
}
