using Syslib;
using Syslib.Games.Card;
using Syslib.Games;
using System.Threading;
using Syslib.Games.Card.TexasHoldEm;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayerAi : CardPlayerAi, ITexasHoldEmPlayer
	{

		public TexasHoldEmPlayerAi(ICardPlayerConfig config, ITexasHoldEmAi ai) : base(config)
		{
			this.AI = ai;
		}

		public void PlaceBet(int tokens, ICardTable table)
		{
			this.TableSeat.PlaceBet(tokens);
		}

		public int BetRaiseCounter { get; private set; }
		public override bool GetReady()
		{
			this.Cards.ClearHand();
			this.Status = "Ready";
			BetRaiseCounter = 0;
			return true;
		}


		// return true if accept or raise bet or false if fold
		public void AskBet(int tokens, ICardTable table)
		{
			this.maxbetraises = table.MaxBetRaises;

//			this.AI.Learn(rounds: 50, numberofplayers: table.PlayerCount);


			if (CRandom.Random.RandomBool(this.Profile.Randomness))
			{
				RandomDecision(tokens);
				return;
			}



			var mycards = this.Cards.GetCards();
			IPlayCards dealercards = null;
			foreach (var seat in table.TableSeats) { if (seat.IsActive && (seat.Player.Type == GamePlayerType.Default)) dealercards = seat.Player.Cards.GetCards(); break; }


			// Ask AI how to do
			var AiSay = this.AI.AskRate(table.PlayerCount, mycards, dealercards);
			AiSay += this.Profile.Weight;

			if (AiSay < 6) { if (tokens > 0) FoldBet(); else CheckBet(); }
			else if (AiSay < 15) { if (tokens > 0) CallBet(tokens); else CheckBet(); }
			else if (AiSay < 30) { if (tokens > 0) CallBet(tokens); else RaiseBet(tokens, 1); }
			else if (AiSay < 50) { if (tokens > 0) CallBet(tokens); else RaiseBet(tokens, 3); }
			else RaiseBet(tokens, 5);

			var played = this.AI.AskPlayed(table.PlayerCount, mycards, dealercards);
			if (played == 0) this.Status = "**NEVER**";
			else this.Status = $"{played,4}:{AiSay}%";

			return;
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
		ITexasHoldEmAi AI;

	}
}
