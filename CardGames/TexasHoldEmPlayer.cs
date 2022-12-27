using Syslib;
using Syslib.Games.Card;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayer : CardGamePlayer
	{
		public TexasHoldEmPlayer(ICardGameTable gametable, ITexasHoldEmIO inout)
		{
			this.gametable = gametable;
			this.seat = null;
			this.IO = inout;
		}



		// return true if accept or raise bet or false if fold
		public override bool AskBet(ICardGameTableSeat seat, int tokens)
		{
			if ((seat == null) || (seat.IsFree) || (!seat.IsActive)) return false;
			this.seat = seat;

			switch (this.seat.Player.PlayerType) {
				case CardPlayerType.Dealer: return AskBetDealer(tokens);
				case CardPlayerType.Human: return AskBetHuman(tokens);
				case CardPlayerType.Robot: return AskBetRobot(tokens);
			}
			return false;
		}

		bool AskBetHuman(int tokens) {

			this.IO.ShowPlayerCards(seat, this.gametable.DealerSeat);
			int returnbet = this.IO.AskForBet(tokens);
			if (returnbet < 0) FoldBet();
			else if (returnbet == 0) {
				if (tokens == 0) CheckBet(); 
				else CallBet(tokens);
			}
			else RaiseBet(tokens, returnbet + tokens);
			return seat.IsActive;
		}

		bool AskBetRobot(int tokens)
		{
			var mycards = seat.PlayerCards.GetPrivateCards();
			var dealercards = this.gametable.DealerSeat.PlayerCards.GetPublicCards();
			var hand = seat.PlayerCards.GetPrivateCards().Add(dealercards);
			int roundprogress = hand.Count();

//			this.IO.ShowPlayerCards(seat, this.gametable.DealerSeat);
			var myrank = new TexasHoldEmHandRank();
			var dealerrank = new TexasHoldEmHandRank();
			var totalrank = new TexasHoldEmHandRank();
			myrank.RankHand(mycards);

			// SOME DECISION MAKING HERE
			// weight: progress 2, 5, 6, 7 cards ?
			// weight: rank of two private cards
			// weight: rank of private and public cards (to normal distribution)
			// weight: rank of dealer cards (posibilites for opponents)
			// weight: playerprofile
			// weight: random weight in desicion ?



			if (CRandom.Random.RandomBool(seat.Player.PlayerProfile.Randomness)) {
				return RandomDecision(tokens);
			}


			// for now accept all requests
			seat.PlaceBet(tokens);
			return true;
		}

		bool AskBetDealer(int tokens)
		{
			// for now accept all requests
			seat.PlaceBet(tokens);
			return true;
		}

		bool RandomDecision(int tokens) {
			// Make a random decission
			// if requested tokens == 0 options are check or raise
			// if requested tokens > 0 optopns are fold, call or raise
			if (tokens > 0)
			{
				if (CRandom.Random.RandomBool(percentchance: 5)) FoldBet();
				else if (CRandom.Random.RandomBool(percentchance: 5)) RaiseBet(tokens, tokens + 1);
				else CallBet(tokens);
			}
			else {
				if (CRandom.Random.RandomBool(percentchance: 5)) RaiseBet(tokens, tokens + 1);
				else CheckBet();
			}
			return seat.IsActive;
		}






		void FoldBet()
		{
			this.seat.Inactivate();
			this.seat.Comment = $" - {seat.Player.Name} fold";
			this.IO.ShowProgressMessage(this.seat.Comment);
		}

		void CheckBet()
		{
			this.seat.Comment = $" - {seat.Player.Name} check";
			this.IO.ShowProgressMessage(this.seat.Comment);
		}

		void CallBet(int tokens)
		{
			this.seat.PlaceBet(tokens);
			this.seat.Comment = $" - {seat.Player.Name} call {tokens} tokens";
			this.IO.ShowProgressMessage(this.seat.Comment);
		}

		void RaiseBet(int tokensrequested, int tokensplaced)
		{
			this.seat.PlaceBet(tokensplaced);
			if (tokensrequested > 0) this.seat.Comment = $" - {seat.Player.Name} call {tokensrequested} and raise {tokensplaced - tokensrequested} tokens";
			else this.seat.Comment = $" - {seat.Player.Name}  raise {tokensplaced} tokens";
			this.IO.ShowProgressMessage(this.seat.Comment);
		}


		ITexasHoldEmIO IO;
		ICardGameTable gametable;
		ICardGameTableSeat seat;

	}
}
