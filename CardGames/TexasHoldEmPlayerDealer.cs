using System;
using System.Collections.Generic;
using System.Threading;
using Games.Card.TexasHoldEm.Models;
using Syslib;
using Syslib.Games;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;


namespace Games.Card.TexasHoldEm
{

	/*
		  Texas Hold'em

		  Each player recieves two private cards and there after each player have three option;
		   - fold (will not participate in this round any longer)
		   - call (add required bet to the pot)
		   - raise (add required bet to the pot and add another bet, require other players to commit again)

		  The first player out is the one to the left of the dealer, rotating clockwise
		  When starting the round a minimum bet is required to participate in the game round
		  and the two first players to the left of dealer are required to place a half and a full bet.

		  Therafter dealer place three public cards on table that is common for everyone, now have a total
		  of five cards each, yet again remaining players make a choice fold, call/check or raise.

		  Thereafter dealer place a fourth public card on the table, and remaining players decide if they
		  will fold, call/check or raise.

		  Finally dealer place a fifth card on the table and now there is a total of five public common cards
		  and two private cards. Reamining players make their decissions (fold,call/check or raise).

		  When the final bet is placed, remaining players show their hand and the one with higest ranked hand win the pot.
		  If two or more players have the same rank, highest card value of the rank wins, or if the rank card value are
		  same the pot is split

		  Number of maximum required cards in one round is (5 + players * 2):
			2 players = 9 cards		3 players = 11 cards		4 players = 13 cards	5 players = 15 cards
			6 players = 17 cards	7 players = 19 cards		8 players = 21 cards	9 players = 23 cards
			10 players = 25 cards. 

		  Texas Hold'em utilizes 1 deck and deck is shuffled after each round

	*/


	public class TexasHoldEmPlayerDealer : CardPlayerDefault
	{

		public TexasHoldEmPlayerDealer(ICardPlayerConfig config, ITexasHoldEmIO UI) :base(config) {
			this.cardStack = new PlayCardStack(decks: 1);
			PlayCard.SetSymbolUTF16Suit(this.cardStack);
			this.dealerSeat = null;
			this.firstCardSeat = null;
			this.lastBetRaiseSeat = null;
			this.playerturninfo = new TexasHoldEmPlayerTurnInfo();
			this.IO = UI;
			this.StatsHand = new TexasStatisticsEntity();
		}

		public int BetRaiseCounter { get; private set; }


		public override bool PlayGame(IGameTable table)
		{

			if (!SetUpGameRound(table)) return false;
			if (!PlaceInitialBets()) { RollBackBets(); return false; }

			this.PlayRound();

			return true;
		}


		// Loop through all seats and make players ready
		private bool SetUpGameRound(IGameTable table)
		{
			if (table == null) return false;
			if (this.IO == null) return false;

			this.gametable = (ICardTable)table;


			this.betsize = 0;
			this.requiredbet = 1;

			this.gametable.TablePot.Clear();

			foreach (var seat in this.gametable.TableSeats)
			{
				if (!seat.IsFree) {	seat.GetReady(); }
			}

			if (this.gametable.ActiveSeatCount < 2) return false;

			return true;
		}

		void Wait(int ms = 0) {
			if (this.gametable.SleepTime < 0 || ms < 0) return;
			if (ms == 0) Thread.Sleep(this.gametable.SleepTime);
			else Thread.Sleep(ms);
		}

		private void PlayRound() {
			this.IO.ShowNewRound(this.gametable);
			this.cardStack.ShuffleCards();                  // shuffle deck
			DealPlayerCards(cards: 1);                      // deal 1 card to each active player
			DealPlayerCards(cards: 1);                      // deal 1 card to each active player
			ShowHumanCards();
			this.IO.ReDrawGameTable();
			PlaceBets();
			DealPublicCards(cards: 3);                      // deal 3 public cards (dealer hand)
			PlaceBets();
			DealPublicCards(cards: 1);                      // deal 1 public cards (dealer hand)
			PlaceBets();
			DealPublicCards(cards: 1);                      // deal 1 public cards (dealer hand)
			PlaceBets();
			FindWinner();
			this.IO.ShowRoundSummary(this.gametable, samepage: true);
			Statistics(this.Cards.GetCards());
		}



		// this will advance trackers for first card seat as well as last bet Raise seat
		// expected that at least two active players exist to call this function
		private bool PlaceInitialBets()
		{
			if (this.dealerSeat != null) this.dealerSeat.IsDealer = false;
			this.dealerSeat = this.gametable.NextActiveSeat(this.dealerSeat);
			this.firstCardSeat = this.gametable.NextActiveSeat(this.dealerSeat);
			this.lastBetRaiseSeat = this.gametable.NextActiveSeat(this.firstCardSeat);

			if ((this.dealerSeat == null) || (this.firstCardSeat == null) || (this.lastBetRaiseSeat == null)) return false;

			this.dealerSeat.IsDealer = true;

			playerturninfo.TokensRequired = this.requiredbet;
			((ITexasHoldEmPlayer)this.firstCardSeat.Player).PlaceBet(playerturninfo);

			this.requiredbet *= 2;
			this.betsize = this.requiredbet;
			playerturninfo.TokensRequired = requiredbet;
			((ITexasHoldEmPlayer)this.lastBetRaiseSeat.Player).PlaceBet(playerturninfo);

			this.lastBetRaiseSeat = this.gametable.NextActiveSeat(this.lastBetRaiseSeat);

			return true;
		}

		// ask for bets around the table until all active player has placed bets and there is still at least two player
		private void PlaceBets() {


			foreach (var playerseat in this.gametable.TableSeats) {
				if (playerseat.IsActive && playerseat.Player.Type != GamePlayerType.Default) ((ITexasHoldEmPlayer)playerseat.Player).BetRaiseCounter = 0;
			}

			// If run immediately after init bets: first seat to be asked is next active after last raise 
			// If run in normal bet round first to ask is first card seat (may have folded and not active)

			var seat = this.lastBetRaiseSeat;
			if (seat == null) seat = this.firstCardSeat;

			do {
				if (this.gametable.ActiveSeatCount < 2) break;
				if (this.lastBetRaiseSeat != null && !this.lastBetRaiseSeat.IsActive) this.lastBetRaiseSeat = seat; 
				if (seat.IsActive)
				{
					seat.IsInTurn = true;
					this.IO.ShowPlayerSeat(seat);
					this.playerturninfo.TokensRequired = 0;
					this.playerturninfo.BetRaiseCountLimit = this.gametable.MaxBetRaises;
					this.playerturninfo.TokensBetLimit = this.gametable.MaxBetLimit;
					this.playerturninfo.TokensBetSize = this.betsize;
					this.playerturninfo.TokensRequest = this.requiredbet - seat.Bets;
					this.playerturninfo.ActivePlayers = this.gametable.ActiveSeatCount;
					this.playerturninfo.NumberOfPlayers = this.gametable.PlayerCount;
					this.playerturninfo.CommonCards = this.Cards.GetCards();
					((ITexasHoldEmPlayer)seat.Player).AskBet(this.playerturninfo);
					if (seat.Bets < this.requiredbet) { if (seat.IsActive) { BugCheck.Critical(this, "PlaceBets : Player do not meet required bet, still active"); } }
					if (seat.Bets > this.requiredbet) { if ((seat.Bets - this.requiredbet) % this.betsize != 0) BugCheck.Critical(this, "PlaceBets : Bet rasie was not of betsize"); }
					if (seat.Bets > this.requiredbet) { this.requiredbet = seat.Bets; this.lastBetRaiseSeat = seat; }
					this.IO.ShowProgressMessage(seat.Comment);
					Wait();
					seat.IsInTurn = false;
					this.IO.ShowPlayerSeat(seat);
					if (this.lastBetRaiseSeat == null) this.lastBetRaiseSeat = seat;
				}
				seat = this.gametable.NextActiveSeat(seat);
			} while (seat != this.lastBetRaiseSeat);

			this.CollectPlayerBets();
			this.lastBetRaiseSeat = null;
			this.requiredbet = 0;
			this.IO.ReDrawGameTable();
		}


		private void CollectPlayerBets() {
			int bet = 0;
			foreach (var seat in this.gametable.TableSeats) {
				if (seat.IsActive) { if (bet == 0) bet = seat.Bets; if (seat.Bets != bet) BugCheck.Critical(this, "CollectPlayerBets : Not all same"); }
				if (seat != null) this.gametable.TablePot.CashIn(seat.CollectBet());
			}
		}

		// roll back bets made, as for some reson round was not completed
		private void RollBackBets() {
			foreach (var seat in this.gametable.TableSeats) {
				if (seat != null) seat.RollbackBet();
			}
		}

		// Deal 1 card around the table
		private bool DealPlayerCards(int cards) {
			var seat = this.firstCardSeat;
			int card = 0;

			if (cards < 1) return false;

			if (this.cardStack.CardsLeft < (cards * this.gametable.ActiveSeatCount)) this.cardStack.ShuffleCards();
			seat.Player.Cards.TakeCard(this.cardStack.NextCard());
			IO.ShowPlayerSeat(seat);
			Wait();
			seat = this.gametable.NextActiveSeat(seat);

			while (seat != this.firstCardSeat) {
				card = 0;
				while (card < cards) {
					seat.Player.Cards.TakeCard(this.cardStack.NextCard()); 
					card++;
					IO.ShowPlayerSeat(seat);
					Wait();
				}
				seat = this.gametable.NextActiveSeat(seat);
			}
			return true;
		}

		private bool DealPublicCards(int cards) {
			int card = 0;
			if (cards > this.cardStack.CardsTotal) return false;
			if (this.cardStack.CardsLeft < cards + 1) this.cardStack.ShuffleCards();
			while (card++ < cards) { 
				this.Cards.TakeCard(this.cardStack.NextCard());
				IO.ShowPlayerSeat(this.TableSeat);
				Wait();
			}
			return true;
		}

		private void ShowHumanCards() {
			foreach (var seat in this.gametable.TableSeats) {
				if (seat.IsActive && seat.Player.Type == GamePlayerType.Human) {
					seat.IsShowCards = true;
					this.IO.ShowPlayerSeat(seat);
				}
			}
		}


		private void FindWinner() {
			ulong winnerrank = 0;
			var WinnersSeats = new CList<ICardTableSeat>();
			var texasrank = new TexasRankOn5Cards();

			// rank all hands, even those who fold (for statistics only)

			foreach (var seat in this.gametable.TableSeats) {
				if (!seat.IsFree) {
					seat.Player.Cards.RankCards(texasrank); // rank all player cards individually (for statistics)
					if (seat.Player != this)
					{
						(seat.Player as ITexasHoldEmPlayer).Action = PlayerAction.NotSet;
						seat.Player.Cards.Signature = texasrank.GetSignature(seat.Player.Cards.GetCards().Add(this.Cards.GetCards()));
						if (seat.IsActive) {
							seat.IsShowCards = true;
							seat.Player.Status = seat.Player.Cards.Signature.Name;
							if (winnerrank < seat.Player.Cards.Signature.Rank) winnerrank = seat.Player.Cards.Signature.Rank;
						}
					}
				}
			}
			foreach (var seat in this.gametable.TableSeats)	{
				if (!seat.IsFree && seat.IsActive) {
					if (seat.Player.Cards.Signature.Rank >= winnerrank) { WinnersSeats.Add(seat); }
				}
			}


			int potshare = this.gametable.TablePot.Tokens; 
			if (WinnersSeats.Count() > 1) { potshare /= WinnersSeats.Count(); this.IO.ShowProgressMessage($"Pot split with {WinnersSeats.Count()} players, each win {potshare} tokens"); }
			foreach (var seat in WinnersSeats) {
				(seat.Player as ITexasHoldEmPlayer).Action = PlayerAction.Win;
				seat.Player.Cards.WinHand = true;
				seat.CashIn(this.gametable.TablePot.CashOut(potshare));
				seat.Comment = $" - Winner {seat.Player.Name} win {potshare} tokens";

				this.IO.ShowProgressMessage(seat.Comment);
				if ((this.gametable.TablePot.Tokens < potshare) && (this.gametable.TablePot.Tokens > 0)) {
					seat.CashIn(this.gametable.TablePot.Clear());
					seat.Comment = $" Uneven potshare given to {seat.Player.Name}";
					this.IO.ShowProgressMessage(seat.Comment);
				}
			}
			this.IO.ReDrawGameTable();
		}


		// Add statistics if requested - only add active hands from
		// end of game as inactive may not have a complete hand
		private void Statistics(IPlayCards commoncards) {

			var statistics = this.gametable.GetStatistics() as TexasHoldEmStatistics;
			if (statistics == null) return;
			if (commoncards == null || commoncards.Count() != 5) return;

			TexasPlayRoundEntity roundentity;
			TexasPlayerHandEntity handentity;

			roundentity = new TexasPlayRoundEntity();
			var rank5 = new TexasRankOn5Cards();
			var cards = new PlayCards();

			IPlayCardsRankSignature cardsranksignature;
			roundentity.Card1Signature = PlayCard.Signature(commoncards.First());
			roundentity.Card2Signature = PlayCard.Signature(commoncards.Next());
			roundentity.Card3Signature = PlayCard.Signature(commoncards.Next());
			roundentity.Card4Signature = PlayCard.Signature(commoncards.Next());
			roundentity.Card5Signature = PlayCard.Signature(commoncards.Next());

			cards.Clear();
			cards.Add(commoncards.First()); 
			cards.Add(commoncards.Next()); 
			cards.Add(commoncards.Next());
			cardsranksignature = rank5.GetSignature(cards);
			roundentity.Card3RankId = cardsranksignature.RankId;

			cards.Add(commoncards.Next());
			cardsranksignature = rank5.GetSignature(cards);
			roundentity.Card4RankId = cardsranksignature.RankId;

			cards.Add(commoncards.Next());
			cardsranksignature = rank5.GetSignature(cards);
			roundentity.Card5RankId = cardsranksignature.RankId;
			roundentity.Card5RankName = cardsranksignature.Name;
			roundentity.WinRankId = 0;
			roundentity.Players = (byte)this.gametable.PlayerCount;
			roundentity.PlayerHands = new List<TexasPlayerHandEntity>();

			foreach (var seat in this.gametable.TableSeats)
			{
				if ((!seat.IsFree) && (seat.Player.Type != GamePlayerType.Default))
				{
					handentity = new TexasPlayerHandEntity();
					cards.Clear();
					cards.Add(seat.Player.Cards.GetCards());

					if (cards.Count() != 2) continue;

					handentity.WinRound = seat.Player.Cards.WinHand;
					handentity.Card1Signature = PlayCard.Signature(cards.First());
					handentity.Card2Signature = PlayCard.Signature(cards.Next());

					cardsranksignature = rank5.GetSignature(cards);
					handentity.Card2RankId = cardsranksignature.RankId;

					cards.Add(commoncards.First());
					cards.Add(commoncards.Next());
					cards.Add(commoncards.Next());
					cardsranksignature = rank5.GetSignature(cards);
					handentity.Card5RankId = cardsranksignature.RankId;

					cards.Add(commoncards.Next());
					cardsranksignature = rank5.GetSignature(cards);
					handentity.Card6RankId = cardsranksignature.RankId;

					handentity.HandRankId = seat.Player.Cards.Signature.RankId;
					handentity.HandRankName = seat.Player.Cards.Signature.Name;

					if (handentity.WinRound) {
						roundentity.WinRankId = handentity.HandRankId;
					}
					roundentity.PlayerHands.Add(handentity);
				}
			}
			statistics.StatsAddRound(roundentity);
		}

		readonly TexasStatisticsEntity StatsHand;
		readonly ITexasHoldEmIO IO;
		IPlayCardStack cardStack;
		int requiredbet;
		int betsize;
		TexasHoldEmPlayerTurnInfo playerturninfo;
		ICardTableSeat dealerSeat;
		ICardTableSeat firstCardSeat;    // player seat that recieves the first card in a deal around the table
		ICardTableSeat lastBetRaiseSeat;        // player that placed the last bet and raised, requiring other to place bets
		ICardTable gametable;
	}


}
