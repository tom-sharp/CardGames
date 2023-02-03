using Syslib;
using Syslib.ConUI;
using Syslib.Games.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm.ConsoleUI
{

	class PlayerSeatConsole : ConObject
	{
		public PlayerSeatConsole(int x, int y, IConObject parent = null) : base(parent)
		{
			this.Seat = null;
			this.Position(x,y);
			this.size.Width = 12;
			this.HighLightColor = new ConColor();
			this.InTurnColor = new ConColor();
			this.InactiveColor = new ConColor();
			this.name = new ConText(0, 0, this.Width, this);
			this.tokens = new ConText(0, 1, this.Width, this);
			this.bet = new ConText(0, 2, this.Width, this);
			this.status = new ConText(0, 3, this.Width, this);
			this.dealer = new ConText(0, 9, this.Width, this);
			this.dealer.Color(this.InTurnColor);
			this.Cards = new CList<PlayCardMedium>()
				.Add(new PlayCardMedium(0, 5, this))
				.Add(new PlayCardMedium(5, 5, this));
			if (y > 10) {
				this.dealer.Position(0,0);
				this.Cards.First().Position(0, 2);
				this.Cards.Next().Position(5, 2);
				this.name.Position(0, 6);
				this.tokens.Position(0,7);
				this.bet.Position(0,8);
				this.status.Position(0,9);
			}


		}

		public ICardTableSeat Seat { get; set; }
		public ConColor HighLightColor { get; }
		public ConColor InTurnColor { get; }
		public ConColor InactiveColor { get; }


		public override IConObject Update()
		{
			if (this.Seat == null || this.Seat.Player == null || this.Seat.IsFree)
			{
				this.name.Text = "Empty";
				this.name.Update();
				return this;
			}

			var invalidcard = new PlayCardHeart(0);
			if (this.Seat.IsDealer) { this.dealer.Color(this.InTurnColor); this.dealer.Text = "Dealer"; }
			else { this.dealer.Color(this.color); this.dealer.Text = ""; }
			


			if (!this.Seat.IsActive)
			{
				this.name.Color(this.InactiveColor);
				this.tokens.Color(this.InactiveColor);
				this.bet.Color(this.InactiveColor);
				this.status.Color(this.InactiveColor);
			}
			else if (this.Seat.IsInTurn)
			{
				this.name.Color(this.HighLightColor);
				this.tokens.Color(this.InTurnColor);
				this.bet.Color(this.InTurnColor);
				this.status.Color(this.InTurnColor);
			}
			else if (this.Seat.Player.Cards.WinHand) {
				this.name.Color(this.InTurnColor);
				this.tokens.Color(this.InTurnColor);
				this.bet.Color(this.InTurnColor);
				this.status.Color(this.HighLightColor);
			}
			else
			{
				this.name.Color(this.Color());
				this.tokens.Color(this.Color());
				this.bet.Color(this.Color());
				if ((this.Seat.Player as ITexasHoldEmPlayer).Action == PlayerAction.Raise) this.status.Color(this.HighLightColor);
				else this.status.Color(this.Color());
			}

			this.name.Text = $"{this.Seat.Player.Name}";
			this.tokens.Text = $"Tkns{this.Seat.Player.Tokens,8}";
			this.bet.Text = $"Bet{this.Seat.Bets,9}";
			this.status.Text = $"{this.Seat.Player.Status,-12}";


			if (this.Seat.IsActive && this.Seat.IsShowCards)
			{
				var cards = this.Seat.Player.Cards.GetCards();
				this.Cards.First().SetUp(cards.First()).Update();
				this.Cards.Next().SetUp(cards.Next()).Update();
			}
			else if (this.Seat.IsActive)
			{
				var cards = this.Seat.Player.Cards.GetCards().Count();
				if (cards > 0) this.Cards.First().SetUp(invalidcard).Update();
				if (cards > 1) this.Cards.Next().SetUp(invalidcard).Update();
			}
			else
			{
				this.Cards.First().SetUp(null).Update();
				this.Cards.Next().SetUp(null).Update();
			}

			return this;
		}

		ConText name { get; }
		ConText tokens { get; }
		ConText bet { get; }
		ConText status { get; }
		ConText dealer { get; }

		readonly CList<PlayCardMedium> Cards;

	}


}
