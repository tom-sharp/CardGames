using Syslib.ConUI;
using Syslib.Games.Card;
using System;

namespace Games.Card.TexasHoldEm.ConsoleUI
{
	class PlayCardMedium : ConObject
	{
		public PlayCardMedium(int x, int y, IConObject parent) : base(parent)
		{
			this.Size(width: 3, height: 3);
			this.Position(x, y);
			this.CardBackColor = new ConColor(ConsoleColor.Gray, ConsoleColor.White);
			this.RedCardColor = new ConColor(ConsoleColor.Red, ConsoleColor.White);
			this.BlackCardColor = new ConColor(ConsoleColor.Black, ConsoleColor.White);
			this.NoCardColor = new ConColor(ConsoleColor.White, ConsoleColor.Black);
			if (parent != null) this.NoCardColor.Set(parent.Color());
			r1 = null;
			r2 = null;
			r3 = null;
		}

		public ConColor CardBackColor { get; }
		public ConColor RedCardColor { get; }
		public ConColor BlackCardColor { get; }
		public ConColor NoCardColor { get; }

		public override IConObject Update()
		{
			if (this.r1 != null)
			{
				this.r1.Color(this.Color()); this.r1.Update();
				this.r2.Color(this.Color()); this.r2.Update();
				this.r3.Color(this.Color()); this.r3.Update();
			}
			else { this.Color(this.NoCardColor); this.ClearArea(); }
			return this;
		}

		public PlayCardMedium SetUp(IPlayCard card)
		{
			if (this.r1 == null)
			{
				r1 = new ConText(0, 0, this.Width, this);
				r2 = new ConText(0, 1, this.Width, this);
				r3 = new ConText(0, 2, this.Width, this);
			}
			if (card != null)
			{
				switch (card.Suit)
				{
					case PlayCardSuit.Heart:
					case PlayCardSuit.Diamond: this.Color(this.RedCardColor); break;
					case PlayCardSuit.Spade:
					case PlayCardSuit.Club: this.Color(this.BlackCardColor); break;
					default: this.Color(this.CardBackColor); break;
				}
			}
			if (card == null)
			{
				r1.Text = null;
				r2.Text = null;
				r3.Text = null;
				this.Color(this.NoCardColor);
			}
			else if (card.Suit == PlayCardSuit.Invalid)
			{
				r1.Text = $"§§§";
				r2.Text = $"§§§";
				r3.Text = $"§§§";
			}
			else
			{
				r1.Text = $"{card.Symbol[0]}  ";
				r2.Text = $" {card.Symbol[1]} ";
				r3.Text = $"  {card.Symbol[0]}";
			}
			return this;
		}

		ConText r1, r2, r3;
	}


}
