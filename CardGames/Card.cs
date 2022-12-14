using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{

	/// <summary>
	/// A card can have one of four suits (hearts, diamonds, spades and clubs)
	/// Each suite consists of 13 cards ranked from 2-10, knight, queen, king and ace.
	/// Where knight is valued as 11, qeen as 12, king as 13 and Ace may be 1 or 14,
	/// depending on the card game. There migt also be jokers included in some
	/// cardgames and may be from 1-6 depending on the game requirements.
	/// 
	/// 2-10, J, Q, K, A
	/// </summary>
	public class Card
	{

		public Card(CardSuite suite, int rank)
		{
			if ((suite != CardSuite.Joker) && (rank > 1) && (rank <= 14))
			{
				this.Suite = suite; this.Rank = rank;	// Valid card
			}
			else
			{
				this.Suite = 0; this.Rank = 0;		// Joker or invalid card
			}
		}


		/// <summary>
		/// return the suite the card belongs to
		/// </summary>
		public CardSuite Suite { get; }


		/// <summary>
		/// return the card rank within the suite
		/// </summary>
		public int Rank { get; }


		/// <summary>
		/// return a two character string representing the symbol name for the card
		/// Ex: H4 = 4 of Hearts,	D0 = 10 of diamonds,	SJ = Knight of spades
		///		CQ = Qeen of clubs,	HK = King of hearts,	DA = Ace of diamonds
		///		** = joker
		/// </summary>
		public string Symbol
		{
			get
			{
				string rank;
				switch (this.Rank)
				{
					case 10: rank = "0"; break;
					case 11: rank = "J"; break;
					case 12: rank = "Q"; break;
					case 13: rank = "K"; break;
					case 14: rank = "A"; break;
					default: rank = this.Rank.ToString(); break;
				}
				switch (this.Suite)
				{
					case CardSuite.Heart: return $"H{rank}";
					case CardSuite.Diamond: return $"D{rank}";
					case CardSuite.Spade: return $"S{rank}";
					case CardSuite.Club: return $"C{rank}";
				}
				return $"**";
			}
		}

	}



}
