using Syslib;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;
using Syslib.Games;
using System.Threading;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayerRobot : CardPlayerRobot, ITexasHoldEmPlayer
	{
		public TexasHoldEmPlayerRobot(ICardPlayerConfig config) : base(config)
		{
		}

		public void PlaceBet(int tokens, ICardTable table)
		{
			this.TableSeat.PlaceBet(tokens);
		}

		public int BetRaiseCounter { get; set; }
		public override bool GetReady()
		{
			this.Cards.ClearHand();
			this.Status = "Ready";
			BetRaiseCounter = 0;
			return true;
		}


		// return true if accept or raise bet or false if fold
		// need to know here is common cards, active players
		public void AskBet(int tokens, ICardTable table)
		{
			this.maxbetraises = table.MaxBetRaises;
			int betsize = 2;

			if (CRandom.Random.RandomBool(this.Profile.Randomness))
			{
				RandomDecision(tokens);
				return;
			}

			var mycards = this.Cards.GetCards();
			IPlayCards dealercards = null;
			foreach (var seat in table.TableSeats) { if (seat.IsActive && (seat.Player.Type == GamePlayerType.Default)) dealercards = seat.Player.Cards.GetCards(); break; }
			var hand = this.Cards.GetCards().Add(dealercards);

			// find out: 2 cards, 2+3, 2+4, 2+5 (flop, turn, river, showdown)
			switch (hand.Count()) {
				case 2: flop(tokens, mycards, table.ActiveSeatCount); return;
				case 5: turn(tokens, mycards, dealercards, hand, table.ActiveSeatCount); break;
				case 6: river(tokens, mycards, dealercards, hand, table.ActiveSeatCount); break;
				case 7: showdown(tokens, mycards, dealercards, hand, table.ActiveSeatCount); break;
			}


			// for now accept all requests
			if (tokens > 0) this.CallBet(tokens);
			else CheckBet();

		}

		// 2 cards
		void flop(int tokens, IPlayCards mycards, int players) {

			var rank2card = mycards.RankCards(new TexasRankOn2Cards()).RankSignature.Rank;
			int result = new EvaluateTexasHand().EvaluateFlop(rank2card, this.Profile.Weight, players);

			if (result < 0) { if (tokens > 0) FoldBet(); }
			else if (result == 0) { if (tokens > 0) CallBet(tokens); else CheckBet(); }
			else RaiseBet(tokens, tokens + result);




		}

		// 5 cards
		void river(int tokens, IPlayCards mycards, IPlayCards dealercards, IPlayCards allcards, int players) { 
		}

		// 6 cards
		void turn(int tokens, IPlayCards mycards, IPlayCards dealercards, IPlayCards allcards, int players) { 
		}

		// 7 cards
		void showdown(int tokens, IPlayCards mycards, IPlayCards dealercards, IPlayCards allcards, int players) { 
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
			if (this.maxbetraises > 0 && maxbetraises <= BetRaiseCounter++) { if (tokensrequested > 0) CallBet(tokensrequested); else CheckBet(); return; }
			this.TableSeat.PlaceBet(tokensplaced);
			if (tokensrequested > 0) this.TableSeat.Comment = $" - {this.Name} call {tokensrequested} and raise {tokensplaced - tokensrequested} tokens";
			else this.TableSeat.Comment = $" - {this.Name}  raise {tokensplaced} tokens";
			this.Status = "Raise";
		}


		int maxbetraises;

	}
}
