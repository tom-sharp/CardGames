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


		// return true if accept or raise bet or false if fold
		// need to know here is common cards, active players
		public void AskBet(ITexasHoldEmPlayerTurnInfo info)
		{
			if (CRandom.Random.RandomBool(this.Profile.Randomness)) { RandomDecision(info); return; }


			var mycards = this.Cards.GetCards();
			var hand = this.Cards.GetCards().Add(info.CommonCards);

			// find out: 2 cards, 2+3, 2+4, 2+5 (flop, turn, river, showdown)
			switch (hand.Count()) {
				case 2: flop(info, mycards); return;
				case 5: turn(info, mycards, info.CommonCards, hand, info.ActivePlayers); break;
				case 6: river(info, mycards, info.CommonCards, hand, info.ActivePlayers); break;
				case 7: showdown(info, mycards, info.CommonCards, hand, info.ActivePlayers); break;
			}

			// for now accept all requests if not resolved above
			if (info.TokensRequest > 0) this.CallBet(info);
			else CheckBet();

		}

		// 2 cards
		void flop(ITexasHoldEmPlayerTurnInfo info, IPlayCards mycards) {

			var rank2card = mycards.RankCards(new TexasRankOn2Cards()).RankSignature.Rank;
			int result = new EvaluateTexasHand().EvaluateFlop(rank2card, this.Profile.Weight, info.ActivePlayers);

			if (result < 0) { if (info.TokensRequest > 0) FoldBet(); }
			else if (result == 0) { if (info.TokensRequest > 0) CallBet(info); else CheckBet(); }
			else RaiseBet(info.TokensRequest + result * info.TokensBetSize, info);

		}


		// 5 cards
		void river(ITexasHoldEmPlayerTurnInfo info, IPlayCards mycards, IPlayCards dealercards, IPlayCards allcards, int players) { 
		}

		// 6 cards
		void turn(ITexasHoldEmPlayerTurnInfo info, IPlayCards mycards, IPlayCards dealercards, IPlayCards allcards, int players) { 
		}

		// 7 cards
		void showdown(ITexasHoldEmPlayerTurnInfo info, IPlayCards mycards, IPlayCards dealercards, IPlayCards allcards, int players) { 
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
			if (!CanRaise(info)) { if (info.TokensRequest > 0) CallBet(info); else CheckBet(); return; }

			BetRaiseCounter++;
			if (info.TokensBetLimit > 0 && (tokensplaced - info.TokensRequest > info.TokensBetLimit)) tokensplaced = info.TokensRequest + info.TokensBetLimit;
			this.TableSeat.PlaceBet(tokensplaced);

			if (info.TokensRequest > 0) this.TableSeat.Comment = $" - {this.Name} call {info.TokensRequest} and raise {tokensplaced - info.TokensRequest} tokens";
			else this.TableSeat.Comment = $" - {this.Name}  raise {tokensplaced} tokens";
			this.Status = "Raise";
			this.Action = PlayerAction.Raise;
		}



	}
}
