using Syslib;
using Syslib.Games.Card;
using Syslib.Games;
using System.Threading;
using Syslib.Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Models;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayerAi : CardPlayerAi, ITexasHoldEmPlayer
	{

		public TexasHoldEmPlayerAi(ICardPlayerConfig config, ITexasHoldEmAi ai) : base(config)
		{
			this.AI = ai;
		}

		public void PlaceBet(ITexasHoldEmPlayerTurnInfo info)
		{
			this.TableSeat.PlaceBet(info.TokensRequired);
		}

		public int BetRaiseCounter { get; set; }		


		public void AskBet(ITexasHoldEmPlayerTurnInfo info)
		{

			if (CRandom.Random.RandomBool(this.Profile.Randomness))	{ RandomDecision(info); return; }

			var mycards = this.Cards.GetCards();
			var AiSay = this.AI.AskRate(info.NumberOfPlayers, mycards, info.CommonCards);

			AiSay += this.Profile.Weight;
			AiSay += this.Profile.Bluffer;

			if (AiSay < 6) { if (info.TokensRequest > 0) FoldBet(); else CheckBet(); }
			else if (AiSay < 15) { if (info.TokensRequest > 0) CallBet(info); else CheckBet(); }
			else if (AiSay < 30) { if (info.TokensRequest > 0) CallBet(info); else RaiseBet(info.TokensRequest + info.TokensBetSize, info); }
			else if (AiSay < 50) { if (info.TokensRequest > 0) CallBet(info); else RaiseBet(info.TokensRequest + info.TokensBetSize * 2, info); }
			else RaiseBet(info.TokensRequest + info.TokensBetSize * 3, info);

			return;
		}


		bool CanRaise(ITexasHoldEmPlayerTurnInfo info)
		{
			if (info.BetRaiseCountLimit > 0 && this.BetRaiseCounter >= info.BetRaiseCountLimit) return false;
			else if (this.BetRaiseCounter >= 4) return false;
			return true;
		}


		void RandomDecision(ITexasHoldEmPlayerTurnInfo info)
		{
			if (info.TokensRequest > 0)
			{
				if (CRandom.Random.RandomBool(percentchance: this.Profile.Defensive)) FoldBet();
				else if (CanRaise(info) && CRandom.Random.RandomBool(percentchance: this.Profile.Offensive)) RaiseBet(info.TokensRequest + info.TokensBetSize, info);
				else CallBet(info);
			}
			else
			{
				if (CanRaise(info) && CRandom.Random.RandomBool(percentchance: this.Profile.Offensive)) RaiseBet(info.TokensRequest + info.TokensBetSize, info);
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

		void CallBet(ITexasHoldEmPlayerTurnInfo info)
		{
			this.TableSeat.PlaceBet(info.TokensRequest);
			this.TableSeat.Comment = $" - {this.Name} call {info.TokensRequest} tokens";
			this.Status = "Call";
		}

		void RaiseBet(int tokensplaced, ITexasHoldEmPlayerTurnInfo info)
		{
			if (!CanRaise(info)) { if (info.TokensRequest > 0) CallBet(info); else CheckBet(); return; }

			BetRaiseCounter++;
			if (info.TokensBetLimit > 0 && (tokensplaced - info.TokensRequest > info.TokensBetLimit)) tokensplaced = info.TokensRequest + info.TokensBetLimit;
			this.TableSeat.PlaceBet(tokensplaced);

			if (info.TokensRequest > 0) this.TableSeat.Comment = $" - {this.Name} call {info.TokensRequest} and raise {tokensplaced - info.TokensRequest} tokens";
			else this.TableSeat.Comment = $" - {this.Name}  raise {tokensplaced} tokens";
			this.Status = "Raise";
		}

		ITexasHoldEmAi AI;

	}
}
