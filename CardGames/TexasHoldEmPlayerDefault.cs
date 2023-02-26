using Syslib;
using Syslib.Games.Card;
using Syslib.Games;
using System.Threading;
using Syslib.Games.Card.TexasHoldEm;
using CardGames;

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

		  Max number of players 5 + 2 * players <= 52  == players max 23 (using 1 deck)

	*/






	public class TexasHoldEmPlayerDefault : TexasHoldEmPlayer
	{

		public TexasHoldEmPlayerDefault(TexasHoldEmTable table, ITexasHoldEmConfig config, ITexasHoldEmUI inout) : base(new Player() { Name = "Common Cards", Type = GamePlayerType.Default })
		{
			this.config = config;
			this.table = table;
			this.IO = inout;
		}


		public override bool InTurn(ITexasHoldEmTurnInfo info)
		{
			if (info == null) return false;
			if (this.Type != GamePlayerType.Default) return false;
			switch (info.GameStatus) {
				case TexasHoldEmGameStatus.Error:
					return false;
				case TexasHoldEmGameStatus.PreFlop:
					if (!SetUpGameRound()) return false;
					if (!PlaceInitialBets()) return false;
					this.IO.ShowNewRound(this.table);
					return true;
				case TexasHoldEmGameStatus.Flop:
					this.IO.DealFlop();
					SetHumanCardVisibilityPublic();
					PlaceBets();
					return true;
				case TexasHoldEmGameStatus.Turn:
					this.IO.DealTurn();
					PlaceBets();
					return true;
				case TexasHoldEmGameStatus.River:
					this.IO.DealRiver();
					PlaceBets();
					return true;
				case TexasHoldEmGameStatus.ShowDown:
					this.IO.DealShowDown();
					PlaceBets();
					return true;
				case TexasHoldEmGameStatus.Winner:
					FindWinner();
					return true;
				case TexasHoldEmGameStatus.Completed:
					var result = this.IO.ShowRoundSummary(this.table);
					if (config.RoundsToPlay == config.RoundsPlayed || !result)
					{
						foreach (var seat in this.table.TableSeats) seat.Summary();
						this.IO.ShowPlayerSummary();
					}
					if (!result) info.Response.Action = TexasHoldEmPlayerStatus.Pass;
					return result;
				default:
					IO.ShowErrMsg("Default player:: Unhandled Gamestatus");
					break;
			}
			return false;

		}

		// Loop through all seats and make players ready
		bool SetUpGameRound()
		{
			if (table == null) return false;
			if (this.IO == null) return false;

			this.requiredbet = 0;

			return true;
		}

		// this will advance trackers for first card seat as well as last bet Raise seat
		// expected that at least two active players exist to call this function
		bool PlaceInitialBets()
		{
			var dealerseat = this.table.DealerSeat;
			var halfblindseat = this.table.NextActivePlayerSeat(dealerseat);
			var fullblindseat = this.table.NextActivePlayerSeat(halfblindseat);

			if ((dealerseat == null) || (halfblindseat == null) || (fullblindseat == null)) return false;

			var infohalfblind = new TexasHoldEmTurnInfo() { TokensRequired = this.config.BetSize / 2 };
			var infofullblind = new TexasHoldEmTurnInfo() { TokensRequired = this.config.BetSize };

			halfblindseat.InTurn(infohalfblind);
			fullblindseat.InTurn(infofullblind);

			this.lastBetRaiseSeat = this.table.NextActivePlayerSeat(fullblindseat);
			this.requiredbet= this.config.BetSize;

			return true;
		}

		// ask for bets around the table until all active player has placed bets and there is still at least two player
		void PlaceBets()
		{


			// If run immediately after init bets: first seat to be asked is next active after last raise 
			// If run in normal bet round first to ask is first card seat (may have folded and not active)

			var seat = this.lastBetRaiseSeat;
			if (seat == null) seat = this.table.NextActivePlayerSeat(this.table.DealerSeat);

			do
			{
				if (this.table.ActiveSeatCount < 3) break;
				if (this.lastBetRaiseSeat != null && !this.lastBetRaiseSeat.IsActive) this.lastBetRaiseSeat = seat;
				if (seat.IsActive)
				{
					seat.Status = TexasHoldEmPlayerStatus.InTurn;
					this.IO.ShowPlayerAction(seat);
					seat.InTurn(TurnInfo(seat));
					if (seat.Bets < this.requiredbet) { if (seat.IsActive) { BugCheck.Critical(this, "PlaceBets : Player do not meet required bet, still active"); } }
					if (seat.Bets > this.requiredbet) { if ((seat.Bets - this.requiredbet) % this.config.BetSize != 0) BugCheck.Critical(this, "PlaceBets : Bet rasie was not of betsize"); }
					if (seat.Bets > this.requiredbet) { this.requiredbet = seat.Bets; this.lastBetRaiseSeat = seat; }
					this.IO.ShowPlayerAction(seat);
					if (this.lastBetRaiseSeat == null) this.lastBetRaiseSeat = seat;
				}
				seat = this.table.NextActivePlayerSeat(seat);
			} while (seat != this.lastBetRaiseSeat);

			this.lastBetRaiseSeat = null;
			this.requiredbet = 0;
			this.IO.ReDrawGameTable();
		}

		ITexasHoldEmTurnInfo TurnInfo(ITexasHoldEmSeat seat) {
			var turninfo = new TexasHoldEmTurnInfo()
			{
				TokensRequired = 0,
				TokensRequest = this.requiredbet - seat.Bets,
				BetRaiseCountLimit = this.config.BetRaiseCountLimit,
				TokensBetLimit = this.config.BetLimit,
				TokensBetSize = this.config.BetSize,
				ActivePlayers = this.table.ActiveSeatCount - 1,
				NumberOfPlayers = this.table.PlayerCount,
				CommonCards = this.Cards.GetCards()
			};
			return turninfo;
		}


		void SetHumanCardVisibilityPublic() {
			this.table.ForEach(seat=> {
				if (seat.IsActive && seat.Player.Type == GamePlayerType.Human) {
					var cards = seat.Player.Cards.GetCards();
					cards.ForEach(card=> card.Visibility = CardVisibility.Public);
					this.IO.ShowPlayerSeat(seat);
				}
			});

		}


		void SetCardVisibilityPublic(ITexasHoldEmSeat seat= null)
		{

			if (seat != null) {
				var cards = seat.Player.Cards.GetCards();
				foreach (var card in cards) { card.Visibility = CardVisibility.Public; }
				this.IO.ShowPlayerSeat(seat);
				return;
			}


			foreach (var pseat in this.table.TableSeats)
			{
				if (pseat.IsActive)
				{
					var cards = pseat.Player.Cards.GetCards();
					foreach (var card in cards) { card.Visibility = CardVisibility.Public; }
				}
			}
		}


		void FindWinner()
		{
			ulong winnerrank = 0;
			var WinnersSeats = new CList<ITexasHoldEmSeat>();
			var texasrank = new TexasHoldEmRankOn5Cards();

			// rank all hands, even those who fold (for statistics only)

			foreach (var seat in this.table.TableSeats)
			{
				if (!seat.IsFree)
				{
					seat.Player.Cards.RankCards(texasrank); // rank all player cards individually (for statistics)
					if (seat.Player != this)
					{
						seat.Player.Cards.Signature = texasrank.GetSignature(seat.Player.Cards.GetCards().Add(this.Cards.GetCards()));
						if (seat.IsActive)
						{
							seat.Player.Status = seat.Player.Cards.Signature.Name;
							if (seat.Status == TexasHoldEmPlayerStatus.Rasie) seat.Status = TexasHoldEmPlayerStatus.Bet;
							if (winnerrank < seat.Player.Cards.Signature.Rank) winnerrank = seat.Player.Cards.Signature.Rank;
						}
					}
				}
			}
			this.SetCardVisibilityPublic();

			foreach (var seat in this.table.TableSeats)
			{
				if (!seat.IsFree && seat.IsActive && seat.Player.Type != GamePlayerType.Default)
				{
					if (seat.Player.Cards.Signature.Rank >= winnerrank) {  WinnersSeats.Add(seat); }
				}
			}


			int potshare = this.table.TablePot.Tokens;
			if (WinnersSeats.Count() > 1) { potshare /= WinnersSeats.Count(); }
			foreach (var seat in WinnersSeats)
			{

				seat.Status = TexasHoldEmPlayerStatus.Winner;
				seat.Player.Cards.WinHand = true;
				seat.CashIn(this.table.TablePot.CashOut(potshare), $" - {seat.Player.Name} win {potshare} tokens");
				if ((this.table.TablePot.Tokens < potshare) && (this.table.TablePot.Tokens > 0))
				{
					seat.CashIn(this.table.TablePot.ClearTokens(), $" Uneven potshare given to {seat.Player.Name}");
				}
			}
			this.IO.ReDrawGameTable();
		}


		readonly ITexasHoldEmUI IO;
		int requiredbet;
		ITexasHoldEmSeat lastBetRaiseSeat;        // player that placed the last bet and raised, requiring other to place bets


		readonly TexasHoldEmTable table;
		readonly ITexasHoldEmConfig config;

	}
}
