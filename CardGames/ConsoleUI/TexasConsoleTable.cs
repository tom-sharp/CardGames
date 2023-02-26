using Syslib;
using Syslib.ConUI;
using Syslib.Games.Card;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Games.Card.TexasHoldEm.ConsoleUI
{

	// 9 player seats whereof 1 is human
	// 1 common card seat
	//
	//  pl  pl  pl  pl  pl  pl
	//  pl  pl  common  pl  pl
	//

	class TexasConsoleTable : ConObject, IEnumerable<PlayerSeatConsole>
	{
		public TexasConsoleTable() : base(null)
		{
			this.Position(0, 0);
			this.Size(width: 120, height: 27);
			this.Border = new ConBorder(this);
			this.CommonSeat = new CommonSeatConsole(2, 1, this);
			this.playerseats = new CList<PlayerSeatConsole>();
		}

		public ConBorder Border { get; }
		public CommonSeatConsole CommonSeat { get; }

		public void DealPlayerCards(int milliseconds)
		{
			int cardno = 0;
			this.playerseats.ForEachWhile(o => {
				if (cardno == 3) return false;
				if (o == null) return true;
				if (o.Seat != null && !o.Seat.IsFree)
				{
					var cards = o.Seat.Player.Cards.GetCards();
					if (cardno == 1) { cards.First().Visibility = CardVisibility.ExclusivePlayer; }
					else if (cardno == 2) { cards.First(); cards.Next().Visibility = CardVisibility.ExclusivePlayer; }
					if (o.Seat.IsDealer) { cardno++; }
					if (cardno > 0) Thread.Sleep(milliseconds);
					o.Update();
				}
				return true;
			});
		}

		public void DealCommonCards(int milliseconds) 
		{
			var cards = this.CommonSeat.Seat.Player.Cards.GetCards();
			cards.ForEach(card=> {
				if (card.Visibility == CardVisibility.Hidden) 
				{
					card.Visibility = CardVisibility.Public;
					Thread.Sleep(milliseconds);
					Thread.Sleep(milliseconds);
					this.CommonSeat.Update();
				}
			});
		}

		public PlayerSeatConsole PlayerSeat(int number)
		{
			int count = 0;
			foreach (var seat in this.playerseats) { if (count++ == number) return seat; }
			return null;
		}


		public PlayerSeatConsole PlayerSeat(ICardTableSeat seat)
		{
			return this.playerseats.First(o => seat == o.Seat);
		}

		public enum TableSetUp { Seats8, Seats10 }

		public void SetUp(TableSetUp seats)
		{
			switch (seats) {
				case TableSetUp.Seats8: this.SetUp8Seats(); break;
				case TableSetUp.Seats10: this.SetUp10Seats(); break;
			}

			foreach (var seat in this.playerseats)
			{
				seat.HighLightColor.Set(this.CommonSeat.HighLightColor);
				seat.InTurnColor.Set(this.CommonSeat.InTurnColor);
				seat.InactiveColor.Set(this.CommonSeat.InactiveColor);
			}
		}
		void SetUp8Seats()
		{
			this.CommonSeat.Position(2, 1);
			this.playerseats.Clear();
			this.playerseats
					.Add(new PlayerSeatConsole(38, 1, this))
					.Add(new PlayerSeatConsole(56, 1, this))
					.Add(new PlayerSeatConsole(74, 1, this))
					.Add(new PlayerSeatConsole(92, 1, this))
					.Add(new PlayerSeatConsole(92, 15, this))
					.Add(new PlayerSeatConsole(74, 15, this))
					.Add(new PlayerSeatConsole(56, 15, this))
					.Add(new PlayerSeatConsole(38, 15, this));
			int cx = 2, cy = 12, cw = 36, ch = 1;
			this.CommonSeat.SeatComment.Size(cw , ch).Position(cx, cy++);
			foreach (var seat in this.playerseats) { seat.SeatComment.Size(cw, ch).Position(cx , cy++); }
		}

		void SetUp10Seats() {
			this.CommonSeat.Position(2, 1);
			this.playerseats.Clear();
			this.playerseats
					.Add(new PlayerSeatConsole(38, 1, this))
					.Add(new PlayerSeatConsole(53, 1, this))
					.Add(new PlayerSeatConsole(68, 1, this))
					.Add(new PlayerSeatConsole(83, 1, this))
					.Add(new PlayerSeatConsole(98, 1, this))
					.Add(new PlayerSeatConsole(98, 15, this))
					.Add(new PlayerSeatConsole(83, 15, this))
					.Add(new PlayerSeatConsole(68, 15, this))
					.Add(new PlayerSeatConsole(53, 15, this))
					.Add(new PlayerSeatConsole(38, 15, this));

			int cx = 2, cy = 12, cw = 36, ch = 1;
			this.CommonSeat.SeatComment.Size(cw, ch).Position(cx, cy++);
			foreach (var seat in this.playerseats) { seat.SeatComment.Size(cw, ch).Position(cx, cy++); }
		}

		public override IConObject Update()
		{
			this.ClearArea();
			this.Border.Update();
			foreach (var seat in this.playerseats) seat.Update();
			this.CommonSeat.Update();
			return this;
		}
		public void UpdateComments() {
			this.CommonSeat.SeatComment.Text = this.CommonSeat.Seat.Comment;
			foreach (var seat in this.playerseats) if (seat.Seat != null) { seat.SeatComment.Text = seat.Seat.Comment; } else seat.SeatComment.Text = "Empty";
		}

		public IEnumerator<PlayerSeatConsole> GetEnumerator() { return playerseats.GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }


		CList<PlayerSeatConsole> playerseats;

	}

}
