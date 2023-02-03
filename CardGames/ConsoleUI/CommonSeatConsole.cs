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
				.Add(new PlayCardMedium(5, 3, this))
				.Add(new PlayCardMedium(10, 3, this))
				.Add(new PlayCardMedium(15, 3, this))
				.Add(new PlayCardMedium(20, 3, this));

			this.HighLightColor = new ConColor();
			this.InTurnColor = new ConColor();
			this.InactiveColor = new ConColor();

			if (y > 10) {
				this.Cards.First().Position(0, 0);
				this.Cards.Next().Position(5, 0);
				this.Cards.Next().Position(10, 0);
				this.Cards.Next().Position(15, 0);
				this.Cards.Next().Position(20, 0);
				this.name.Position(0, 4);
				this.pot.Position(0, 5);
			}

		}

		public ICardTableSeat Seat { get; set; }
		public int TablePot { get; set; }

		public ConColor HighLightColor { get; }
		public ConColor InTurnColor { get; }
		public ConColor InactiveColor { get; }

		public override IConObject Update()
		{
			if (this.Seat == null) return this;

			this.name.Text = this.Seat.Player.Name;
			this.pot.Text = $"Pot {this.TablePot,20}";
			var cards = this.Seat.Player.Cards.GetCards();
			this.Cards.First().SetUp(cards.First()).Update();
			this.Cards.Next().SetUp(cards.Next()).Update();
			this.Cards.Next().SetUp(cards.Next()).Update();
			this.Cards.Next().SetUp(cards.Next()).Update();
			this.Cards.Next().SetUp(cards.Next()).Update();

			return this;
		}

		ConText name;
		ConText pot;
		CList<PlayCardMedium> Cards;


	}

}
