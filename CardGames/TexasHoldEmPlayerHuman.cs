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


		public void PlaceBet(int requiredtokens, ICardTable table)
		{
			this.TableSeat.PlaceBet(requiredtokens);
		}

		public int BetRaiseCounter { get; private set; }
		public override bool GetReady()
		{
			this.Cards.ClearHand();
			this.Status = "Ready";
			BetRaiseCounter = 0;
			return true;
		}


		public void AskBet(int requestedtokens, ICardTable table)
		{
			int betsize = 2;
			if (this.BetRaiseCounter >= table.MaxBetRaises) betsize = -1;

			int returnbet = this.IO.AskForBet(requestedtokens, betsize);
			if (returnbet < 0) FoldBet();
			else if (returnbet == 0)
			{
				if (requestedtokens == 0) CheckBet();
				else CallBet(requestedtokens);
			}
			else RaiseBet(requestedtokens, returnbet + requestedtokens);
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
			BetRaiseCounter++;
			this.TableSeat.PlaceBet(tokensplaced);
			if (tokensrequested > 0) this.TableSeat.Comment = $" - {this.Name} call {tokensrequested} and raise {tokensplaced - tokensrequested} tokens";
			else this.TableSeat.Comment = $" - {this.Name}  raise {tokensplaced} tokens";
			this.Status = "Raise";
		}


		ITexasHoldEmIO IO;

	}
}
