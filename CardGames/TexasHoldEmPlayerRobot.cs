using Syslib;
using Syslib.Games.Card;
using Syslib.Games;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayerRobot : CardPlayerRobot
	{
		public TexasHoldEmPlayerRobot(ICardPlayerConfig config, ITexasHoldEmIO inout) : base(config)
		{
			this.IO = inout;
		}



		// return true if accept or raise bet or false if fold
		public override void AskBet(int tokens)
		{
			var mycards = this.TableSeat.PlayerCards.GetPrivateCards();
			var dealercards = this.gametable.DefaultSeat.PlayerCards.GetPublicCards();
			var hand = this.TableSeat.PlayerCards.GetPrivateCards().Add(dealercards);
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



			if (CRandom.Random.RandomBool(this.Profile.Randomness))
			{
				RandomDecision(tokens);
				return;
			}


			// for now accept all requests
			this.TableSeat.PlaceBet(tokens);

		}




		void RandomDecision(int tokens) {
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
		}






		void FoldBet()
		{
			this.TableSeat.IsActive = false;
			this.TableSeat.Comment = $" - {this.Name} fold";
			this.IO.ShowProgressMessage(this.TableSeat.Comment);
		}

		void CheckBet()
		{
			this.TableSeat.Comment = $" - {this.Name} check";
			this.IO.ShowProgressMessage(this.TableSeat.Comment);
		}

		void CallBet(int tokens)
		{
			this.TableSeat.PlaceBet(tokens);
			this.TableSeat.Comment = $" - {this.Name} call {tokens} tokens";
			this.IO.ShowProgressMessage(this.TableSeat.Comment);
		}

		void RaiseBet(int tokensrequested, int tokensplaced)
		{
			this.TableSeat.PlaceBet(tokensplaced);
			if (tokensrequested > 0) this.TableSeat.Comment = $" - {this.Name} call {tokensrequested} and raise {tokensplaced - tokensrequested} tokens";
			else this.TableSeat.Comment = $" - {this.Name}  raise {tokensplaced} tokens";
			this.IO.ShowProgressMessage(this.TableSeat.Comment);
		}


		ITexasHoldEmIO IO;
	}
}
