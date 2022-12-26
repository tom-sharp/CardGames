using System;


namespace Games.Card
{
	public class CardGameTableSeat : ICardGameTableSeat
	{
		public CardGameTableSeat()
		{
			this.seatwallet = new TokenWallet();
			this.playercards = new CardGamePlayerCards();
			this.player = null;
			this.IsActive = false;
			this.Comment = "";
		}

		public bool IsFree {
			get {
				if (this.player == null) return true;
				return false;
			}
		}

		public bool IsActive { get; private set; }


		public bool Join(CardPlayer player)
		{
			this.IsActive = false;
			if (player == null) return false;
			if (this.IsFree) { this.player = player; this.Comment = $"{this.player.Name} joined game"; return true; }
			return false;
		}

		public bool Leave()
		{
			this.IsActive = false;
			if (this.player == null) return true;
			this.Comment = $"{this.player.Name} left game";
			this.player = null;
			return true;
		}


		public void NewRound()
		{
			this.playercards.ClearHand();
			this.seatwallet.Clear();
			if (this.player != null) this.IsActive = true; else this.IsActive = false;
			if (this.IsActive) this.Comment = "Ready"; else this.Comment = "Free seat";
		}

		// Addd winnings to player wallet
		public void WinTokens(int tokens)
		{
			if (this.player == null) return;
			this.player.Wallet.AddTokens(tokens);
			this.Comment = $" - {this.player.Name} wins  {tokens} tokens";
		}

		// remove requested tokens from player wallet and add them to seat bet tokens
		public void PlaceBet(int tokens)
		{
			if ((this.player == null) || (!this.IsActive)) { this.IsActive = false; return; }
			if (tokens > 0)
			{
				this.seatwallet.AddTokens(this.player.Wallet.RemoveTokens(tokens));
				this.Comment = $" - {this.player.Name} called  {tokens}";
			}
			else this.Comment = $" - {this.player.Name} check";
		}

		// remove tokens from uplayer wallet and add them to table seat wallet
		public void RaiseBet(int tokens)
		{
			if ((this.player == null) || (!this.IsActive)) { this.IsActive = false; return; }
			this.seatwallet.AddTokens(this.player.Wallet.RemoveTokens(tokens));
			this.Comment = $" - {this.player.Name} raise  {tokens}";
		}

		public void Fold()
		{
			this.IsActive = false;
			this.Comment = $" - {this.player.Name} fold";
		}

		public int CollectBet()
		{
			return this.seatwallet.Clear();
		}

		public void RollbackBet()
		{
			if (this.player == null) return;
			this.player.Wallet.AddTokens(this.seatwallet.Clear());
		}


		public ICardGamePlayerCards PlayerCards { get { return this.playercards; } }



		public int Bets { get { return this.seatwallet.Tokens; } }

		public String Comment { get; private set; }



		public ICardPlayer Player { get { return this.player; } }

		CardPlayer player;
		ICardGamePlayerCards playercards;
		ITokenWallet seatwallet;
	}


}
