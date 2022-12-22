using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syslib;

namespace Games.Card
{
	public class CardGameTableSeat
	{
		public CardGameTableSeat()
		{
			PrivateCards = new CList<Card>();
			PublicCards = new CList<Card>();
			Player = null;
			Active = false;
			Comment = "";
		}

		public bool IsFree() {
			if (Player == null) return true;
			return false;
		}

		public bool Join(CardPlayer player) {
			if (player == null) return false;
			Active = false;
			if (IsFree()) { Player = player; Comment = $"{Name} joined game"; return true; }
			return false;
		}

		public bool Leave() {
			if (this.Player == null) return true;
			Comment = $"{Name} left game";
			Active = false;
			Player = null;
			return true;
		}


		public void NewRound() {
			PrivateCards.Clear();
			PublicCards.Clear();
			Comment = "Ready";
			Active = true;
			Bets = 0;
		}


		public bool PlaceBet(int tokens) {
			if (this.Player == null) return false;
			Bets += tokens;
			this.Player.UpdateTokenWallet(-tokens);
			return true;
		}

		public int CollectBet() {
			int bet = Bets;
			Bets = 0;
			return bet;
		}

		public bool AskBet(int tokens) {

			// Temporary function - this should be handled in TexasPlayer class
			// more evaluatin and possible to raise / fold. Here ONLY accept request, all users call

			if (this.Player == null) return false;
			Bets += tokens;
			this.Player.UpdateTokenWallet(-tokens);
			return true;
		}

		public void ReturnBet() {
			if (this.Player == null) return;
			this.Player.UpdateTokenWallet(Bets);
			Bets = 0;
		}

		public void PayOutWinningPot(int tokens)
		{
			if (this.Player == null) return;
			this.Player.UpdateTokenWallet(tokens);
		}

		public void TakePrivateCard(Card card) {
			if (card == null) return;
			this.PrivateCards.Add(card);
		}

		public void TakePublicCard(Card card) {
			if (card == null) return;
			this.PublicCards.Add(card);
		}

		public CList<Card> ShowCards() {
			return new CList<Card>().Add(this.PrivateCards).Add(this.PublicCards);
		}

		public bool Active { get; private set; }

		public String Comment { get; private set; }

		public int Bets { get; private set; }

		public string Name { get { if (this.Player != null) return this.Player.Name; else return ""; } }

		public int Tokens { get { if (this.Player != null) return this.Player.Tokens; else return 0; } }



		CList<Card> PrivateCards;
		CList<Card> PublicCards;
		CardPlayer Player;
	}


}
