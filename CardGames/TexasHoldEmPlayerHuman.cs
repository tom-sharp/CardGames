using Syslib;
using Syslib.Games.Card;
using Syslib.Games;
using Syslib.Games.Card.TexasHoldEm;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayerHuman : CardPlayerHuman, ITexasHoldEmPlayer
	{
		public TexasHoldEmPlayerHuman(ICardPlayerConfig config, ITexasHoldEmIO inout):base(config)
		{
			this.IO = inout;
		}

		public PlayerAction Action { get; set; }
		public override bool GetReady()
		{
			this.Action = PlayerAction.NotSet;
			return base.GetReady();
		}

		public void PlaceBet(ITexasHoldEmPlayerTurnInfo info)
		{
			this.TableSeat.PlaceBet(info.TokensRequired);
		}

		public int BetRaiseCounter { get; set; }


		public void AskBet(ITexasHoldEmPlayerTurnInfo info)
		{
			int returnbet;

			if (CanRaise(info)) returnbet = this.IO.AskForBet(info.TokensRequest, info.TokensBetSize);
			else returnbet = this.IO.AskForBet(info.TokensRequest, -1);

			if (returnbet < 0) FoldBet();
			else if (returnbet == 0)
			{
				if (info.TokensRequest == 0) CheckBet();
				else CallBet(info);
			}
			else RaiseBet(returnbet + info.TokensRequest, info);
		}



		bool CanRaise(ITexasHoldEmPlayerTurnInfo info) {
			if (info.BetRaiseCountLimit > 0 && this.BetRaiseCounter >= info.BetRaiseCountLimit) return false;
			return true;
		}


		void FoldBet()
		{
			this.TableSeat.IsActive = false;
			this.TableSeat.Comment = $" - {this.Name} fold";
			this.Status = "Fold";
			this.Action = PlayerAction.Fold;
		}

		void CheckBet()
		{
			this.TableSeat.Comment = $" - {this.Name} check";
			this.Status = "Check";
			this.Action = PlayerAction.Check;
		}

		void CallBet(ITexasHoldEmPlayerTurnInfo info)
		{
			this.TableSeat.PlaceBet(info.TokensRequest);
			this.TableSeat.Comment = $" - {this.Name} call {info.TokensRequest} tokens";
			this.Status = "Call";
			this.Action = PlayerAction.Call;

		}

		void RaiseBet(int tokensplaced, ITexasHoldEmPlayerTurnInfo info)
		{
			BetRaiseCounter++;
			if (info.TokensBetLimit > 0 && (tokensplaced - info.TokensRequest > info.TokensBetLimit)) tokensplaced = info.TokensRequest + info.TokensBetLimit;
			this.TableSeat.PlaceBet(tokensplaced);
			if (info.TokensRequest > 0) this.TableSeat.Comment = $" - {this.Name} call {info.TokensRequest} and raise {tokensplaced - info.TokensRequest} tokens";
			else this.TableSeat.Comment = $" - {this.Name}  raise {tokensplaced} tokens";
			this.Status = "Raise";
			this.Action = PlayerAction.Raise;
		}


		ITexasHoldEmIO IO;

	}
}
