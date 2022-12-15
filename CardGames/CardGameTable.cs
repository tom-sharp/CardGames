using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syslib;

namespace Games.Card
{

	/// <summary>
	/// CardGameTable provide a plattform to put togeather players and a card game
	/// The dealer control card game flow and what type of card game to play
	/// Seat position 0 is reserved for the dealer if requireed to participate in game
	/// </summary>
	class CardGameTable
	{
		public CardGameTable(int tableseats, ICardGameDealer dealer)
		{
			this.carddealer = dealer;
			if (tableseats >= 0) this.cardplayers = new ICardGamePlayer[tableseats + 1];
			else this.cardplayers = new ICardGamePlayer[1];
		}

		public bool AddPlayer(ICardGamePlayer player) {
			int seat = 1;
			if (player == null) return false;
			while (seat < this.cardplayers.Length) {
				if (this.cardplayers[seat] == null) {
					this.cardplayers[seat] = player;
					this.cardplayers[seat].Active = true;
					return true;
				}
				seat++;
			}
			return false;
		}

		public void RemovePlayer(int seat) {
			if ((seat < 1) || (seat > this.cardplayers.Length)) return;     // do not remove dealer
			this.cardplayers[seat] = null;
		}

		public void Run() {
			if (this.carddealer == null) return;
			while (this.carddealer.Run(cardplayers)) { 
				// Allow here to  opt in or out new players after each round
				// Run method alwas return true unless ther is a problem to continue
			}
		}

		public int TokenPot { get; private set; }


		ICardGameDealer carddealer = null; 
		ICardGamePlayer[] cardplayers = null;
	}
}
