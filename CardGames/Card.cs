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
	/// depending on the card game. There might also be jokers included in some
	/// cardgames and may be from 1-6 depending on the game requirements.
	/// 
	/// 2-10, J, Q, K, A
	/// </summary>
	public class Card
	{

		public Card(CardSuite suite, int rank)
		{
			this.jokercard = false;
			if (suite == CardSuite.Joker) 
			{
				this.jokercard = true;
				this.cardsuite = suite; this.cardrank = 0;					// Joker card
			}
			else if ((suite != CardSuite.Blank) && (rank > 1) && (rank <= 14))
			{
				this.cardsuite = suite; this.cardrank = rank;				// Valid card
			}
			else 
			{ 
				this.cardsuite = CardSuite.Blank; this.cardrank = 0;	   // Invalid card
			}
		}


		/// <summary>
		/// return the suite the card belongs to
		/// </summary>
		public CardSuite Suite { get { return this.cardsuite; } }


		/// <summary>
		/// return the card rank within the suite
		/// </summary>
		public int Rank { get { return this.cardrank; } }


		public Card SetJoker(CardSuite suite = CardSuite.Joker, int rank = 0) {
			if (this.jokercard) {
				this.cardsuite = suite;
				if ((rank >= 0) && (rank <= 14)) this.cardrank = rank;
				else this.cardrank = 0;
			}
			return this;
		}


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
				if (this.jokercard) return "**";
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
					case CardSuite.Heart: return $"{rank}H";
					case CardSuite.Diamond: return $"{rank}D";
					case CardSuite.Spade: return $"{rank}S";
					case CardSuite.Club: return $"{rank}C";
				}
				return $"--";	// blank card (invalid)
			}
		}

		bool jokercard;
		CardSuite cardsuite;
		int cardrank;

	}



}
