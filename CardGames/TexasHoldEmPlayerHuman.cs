using Syslib;
using Syslib.Games.Card;
using Syslib.Games;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayerHuman : CardPlayerHuman, ITexasHoldEmPlayer
	{
		public TexasHoldEmPlayerHuman(ICardPlayerConfig config, ITexasHoldEmIO inout):base(config)
		{
			this.IO = inout;
		}


		public override void PlaceBet(int tokens, ICardTable table)
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


		public override void AskBet(int tokens, ICardTable table)
		{
			this.maxbetraises = table.MaxBetRaises;

			int returnbet = this.IO.AskForBet(tokens);
			if (returnbet < 0) FoldBet();
			else if (returnbet == 0)
			{
				if (tokens == 0) CheckBet();
				else CallBet(tokens);
			}
			else RaiseBet(tokens, returnbet + tokens);
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
			this.Status = "Raised";
		}

		int maxbetraises;

		ITexasHoldEmIO IO;

	}
}
