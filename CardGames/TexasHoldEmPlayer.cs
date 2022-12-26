using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayer : CardGamePlayer
	{
		public TexasHoldEmPlayer(ICardGameTable gametable, ITexasHoldEmIO inout)
		{
			this.gametable = gametable;
			this.IO = inout;
		}



		// return true if accept or raise bet or false if fold
		public override bool AskBet(ICardGameTableSeat seat, int tokens)
		{
			if ((seat.IsFree) || (!seat.IsActive)) return false;

			switch (seat.Player.PlayerType) {
				case CardPlayerType.Dealer: return AskBetDealer(seat, tokens);
				case CardPlayerType.Human: return AskBetHuman(seat, tokens);
				case CardPlayerType.Robot: return AskBetRobot(seat, tokens);
			}
			return false;
		}

		bool AskBetHuman(ICardGameTableSeat seat, int tokens) {

			this.IO.ShowPlayerCards(seat, this.gametable.DealerSeat);
			int ret = this.IO.AskForBet(tokens);
			if (ret < 0)
			{
				seat.Fold();
				return false;
			}
			else if (ret == 0) { seat.PlaceBet(tokens); return true; }
			seat.PlaceBet(tokens);
			seat.RaiseBet(ret);
			return true;
		}

		bool AskBetRobot(ICardGameTableSeat seat, int tokens)
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
				return RandomDecision(seat, tokens);
			}


			// for now accept all requests
			seat.PlaceBet(tokens);
			return true;
		}

		bool AskBetDealer(ICardGameTableSeat seat, int tokens)
		{
			// for now accept all requests
			seat.PlaceBet(tokens);
			return true;
		}

		bool RandomDecision(ICardGameTableSeat seat, int tokens) {
			// Make a random decission
			// if tokens == 0 options are fold, check, raise
			// if tokens > 0 optopns are fold, call or raise
			if (tokens > 0)
			{
				if (CRandom.Random.RandomBool(percentchance: 5))
				{
					seat.Fold();	// 5% chance to fold 
				}
				else {
					seat.PlaceBet(tokens);	// call bet
					if (CRandom.Random.RandomBool(percentchance: 5))
					{
						seat.RaiseBet(1);	// 5% chance to raise
					}
				}
			}
			else {
				seat.PlaceBet(tokens);  // check bet
				if (CRandom.Random.RandomBool(percentchance: 5))
				{
					seat.RaiseBet(1);   // 5% chance to raise
				}
			}
			return seat.IsActive;
		}


		ITexasHoldEmIO IO;
		ICardGameTable gametable = null;

	}
}
