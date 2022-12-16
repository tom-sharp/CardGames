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
			Comment = "";
		}

		public bool IsFree() {
			if (Player == null) return true;
			return false;
		}

		public bool Join(CardPlayer player) {
			if (player == null) return false;
			if (IsFree()) { Player = player; Comment = $"{Name} joined game"; return true; }
			return false;
		}

		public bool Leave() {
			Comment = $"{Name} left game";
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


		public bool RequiredBet(int tokens) {
			if (this.Player == null) return false;
			Bets += tokens;
			this.Player.UpdateTokenWallet(-tokens);
			return true;
		}

		public void TakePrivateCard(Card card) {
			if (card == null) return;
			this.PrivateCards.Add(card);
		}

		public void TakePublicCard(Card card) {
			if (card == null) return;
			this.PublicCards.Add(card);
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
