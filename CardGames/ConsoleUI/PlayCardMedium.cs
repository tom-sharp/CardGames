using Syslib.ConUI;
using Syslib.Games.Card;
using System;

namespace Games.Card.TexasHoldEm.ConsoleUI
{
	class PlayCardMedium : ConObject
	{
		public PlayCardMedium(int x, int y, IConObject parent) : base(parent)
		{
			this.Size(width: 5, height: 5);
			this.Position(x, y);
			this.CardBackColor = new ConColor(ConsoleColor.Gray, ConsoleColor.White);
			this.RedCardColor = new ConColor(ConsoleColor.Red, ConsoleColor.White);
			this.BlackCardColor = new ConColor(ConsoleColor.Black, ConsoleColor.White);
			this.SelectedCardColor = new ConColor(ConsoleColor.Yellow, ConsoleColor.White);
			this.NoCardColor = new ConColor(ConsoleColor.White, ConsoleColor.Black);
			if (parent != null) this.NoCardColor.Set(parent.Color());
			this.selected = new ConBorder(this);
			this.r1 = null;
			this.r2 = null;
			this.r3 = null;
		}

		public ConColor CardBackColor { get; }
		public ConColor RedCardColor { get; }
		public ConColor BlackCardColor { get; }
		public ConColor SelectedCardColor { get; }
		public ConColor NoCardColor { get; }

		public override IConObject Update()
		{
			if (this.r1 != null)
			{
				this.r1.Color(this.Color()); this.r1.Update();
				this.r2.Color(this.Color()); this.r2.Update();
				this.r3.Color(this.Color()); this.r3.Update();
				this.selected.Update();
			}
			else { this.Color(this.NoCardColor); this.ClearArea(); }
			return this;
		}

		public PlayCardMedium Update(IPlayCard card)
		{
			if (this.r1 == null)
			{
				r1 = new ConText(1, 1, 3, this);
				r2 = new ConText(1, 2, 3, this);
				r3 = new ConText(1, 3, 3, this);
			}
			this.selected.BorderType = ConBorder.ConBorderType.SingleLine;
			if (card == null || card.Visibility == CardVisibility.Hidden)
			{
				this.Color(this.NoCardColor);
				r1.Text = null;
				r2.Text = null;
				r3.Text = null;
				this.selected.BorderType = ConBorder.ConBorderType.Space;
				this.Update();
				return this;
			}

			switch (card.Suit)
			{
				case PlayCardSuit.Heart:
				case PlayCardSuit.Diamond: this.Color(this.RedCardColor); break;
				case PlayCardSuit.Spade:
				case PlayCardSuit.Club: this.Color(this.BlackCardColor); break;
			}

			if (card.Suit == PlayCardSuit.Invalid || card.Visibility != CardVisibility.Public)
			{
				this.Color(this.CardBackColor);
				r1.Text = $"§§§";
				r2.Text = $"§§§";
				r3.Text = $"§§§";
			}
			else
			{
				if (card.IsSelected) this.selected.BorderType = ConBorder.ConBorderType.SolidBlockThin;
				r1.Text = $"{card.Symbol[0]}  ";
				r2.Text = $" {card.Symbol[1]} ";
				r3.Text = $"  {card.Symbol[0]}";
			}
			this.Update();
			return this;
		}

		ConText r1, r2, r3;
		ConBorder selected;
	}


}
