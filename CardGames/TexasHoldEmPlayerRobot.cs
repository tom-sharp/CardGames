using Syslib;
using Syslib.Games.Card;
using Syslib.Games;
using System.Threading;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayerRobot : CardPlayerRobot
	{
		public TexasHoldEmPlayerRobot(ICardPlayerConfig config) : base(config)
		{
		}

		public override void PlaceBet(int tokens, ICardTable table)
		{
			this.TableSeat.PlaceBet(tokens);
		}

		public override bool GetReady()
		{
			this.Cards.ClearHand();
			this.Status = "Ready";
			return true;
		}


		// return true if accept or raise bet or false if fold
		public override void AskBet(int tokens, ICardTable table)
		{
			var mycardsrank = this.Cards.GetCards();
			IPlayCards dealercards = null;
			foreach (var seat in table.TableSeats) { if (seat.IsActive && (seat.Player.Type == GamePlayerType.Default)) dealercards = seat.Player.Cards.GetCards(); break; }
			var hand = this.Cards.GetCards().Add(dealercards);
			int roundprogress = hand.Count;

			var myrank = new TexasRankOn5Cards();
			var dealerrank = new TexasRankOn5Cards();
			var totalrank = new TexasRankOn5Cards();


			// SOME DECISION MAKING HERE
			// weight: progress 2, 5, 6, 7 cards ?
			// weight: rank of two private cards
			// weight: rank of private and public cards (to normal distribution)
			// weight: rank of dealer cards (posibilites for opponents)
			// weight: playerprofile
			// weight: random weight in decision ?


			
			if (CRandom.Random.RandomBool(this.Profile.Randomness))
			{
				RandomDecision(tokens);
				return;
			}


			// for now accept all requests
			if (tokens > 0) this.CallBet(tokens);
			else CheckBet();

		}




		void RandomDecision(int tokens) {
			// Make a random decision
			// if requested tokens == 0 options are check or raise
			// if requested tokens > 0 optopns are fold, call or raise
			if (tokens > 0)
			{
				if (CRandom.Random.RandomBool( percentchance: this.Profile.Defensive)) FoldBet();
				else if (CRandom.Random.RandomBool(percentchance: this.Profile.Offensive)) RaiseBet(tokens, tokens + 1);
				else CallBet(tokens);
			}
			else {
				if (CRandom.Random.RandomBool(percentchance: this.Profile.Offensive)) RaiseBet(tokens, tokens + 1);
				else CheckBet();
			}
		}






		void FoldBet()
		{
			this.TableSeat.IsActive = false;
			this.TableSeat.Comment = $" - {this.Name} fold";
			this.Status = "Fold";
		}

		void CheckBet()
		{
			this.TableSeat.Comment = $" - {this.Name} check";
			this.Status = "Check";
		}

		void CallBet(int tokens)
		{
			this.TableSeat.PlaceBet(tokens);
			this.TableSeat.Comment = $" - {this.Name} call {tokens} tokens";
			this.Status = "Call";
		}

		void RaiseBet(int tokensrequested, int tokensplaced)
		{
			this.TableSeat.PlaceBet(tokensplaced);
			if (tokensrequested > 0) this.TableSeat.Comment = $" - {this.Name} call {tokensrequested} and raise {tokensplaced - tokensrequested} tokens";
			else this.TableSeat.Comment = $" - {this.Name}  raise {tokensplaced} tokens";
			this.Status = "Raise";
		}

	}
}
