using Syslib;
using Syslib.ConUI;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm.ConsoleUI
{

	class CommonSeatConsole : ConObject
	{
		public CommonSeatConsole(int x, int y, IConObject parent = null) : base(parent)
		{
			this.Seat = null;
			this.Position(x,y);
			this.size.Width = 36;
			this.name = new ConText(0, 0,this.Width, this);
			this.pot = new ConText(0, 1, this.Width, this);
			this.Cards = new CList<PlayCardMedium>()
				.Add(new PlayCardMedium(0, 3, this))
				.Add(new PlayCardMedium(6, 3, this))
				.Add(new PlayCardMedium(12, 3, this))
				.Add(new PlayCardMedium(18, 3, this))
				.Add(new PlayCardMedium(24, 3, this));

			this.HighLightColor = new ConColor();
			this.InTurnColor = new ConColor();
			this.InactiveColor = new ConColor();

			if (y > 10) {
				this.Cards.First().Position(0, 0);
				this.Cards.Next().Position(6, 0);
				this.Cards.Next().Position(12, 0);
				this.Cards.Next().Position(18, 0);
				this.Cards.Next().Position(24, 0);
				this.name.Position(0, 4);
				this.pot.Position(0, 5);
			}
			this.SeatComment = new ConText(parent);

		}

		public ConText SeatComment { get; }

		public ITexasHoldEmSeat Seat { get; set; }
		public int TablePot { get; set; }

		public ConColor HighLightColor { get; }
		public ConColor InTurnColor { get; }
		public ConColor InactiveColor { get; }

		public override IConObject Update()
		{
			if (this.Seat == null) return this;
			
			this.SeatComment.Text = this.Seat.Comment;
			this.name.Text = this.Seat.Player.Name;
			this.pot.Text = $"Pot {this.TablePot,20}";
			var cards = this.Seat.Player.Cards.GetCards();
			this.Cards.First().Update(cards.First()).Update();
			this.Cards.Next().Update(cards.Next());
			this.Cards.Next().Update(cards.Next());
			this.Cards.Next().Update(cards.Next());
			this.Cards.Next().Update(cards.Next());

			return this;
		}
		public void UpdateComment()
		{
			if (this.Seat == null || this.Seat.Player == null || this.Seat.IsFree)
			{
				this.SeatComment.Color(this.InactiveColor);
				this.SeatComment.Text = "Empty";
				return;
			}
			this.SeatComment.Color(this.Color());
			this.SeatComment.Text = this.Seat.Comment;
		}

		ConText name;
		ConText pot;
		CList<PlayCardMedium> Cards;


	}

}
