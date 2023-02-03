using Syslib;
using Syslib.ConUI;
using Syslib.Games.Card;
using System;
using System.Collections;
using System.Collections.Generic;

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
			this.Size(width: 120, height: 25);
			this.Color(fg: ConsoleColor.Green, bg: ConsoleColor.Black);
			this.Border = new ConBorder(this);
			this.CommonSeat = new CommonSeatConsole(37, 21, this);
			this.Log = new ConLog(32, 12, 38, 7, this);
			this.playerseats = new CList<PlayerSeatConsole>();
		}

		public ConBorder Border { get; }
		public CommonSeatConsole CommonSeat { get; }

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

		public ConLog Log { get; }

		public enum TableSetUp { Seats10 }
		public void SetUp(TableSetUp seats)
		{
			this.playerseats.Clear();
			if (seats == TableSetUp.Seats10)
			{
				this.playerseats
					.Add(new PlayerSeatConsole(2, 1, this))
					.Add(new PlayerSeatConsole(20, 1, this))
					.Add(new PlayerSeatConsole(38, 1, this))
					.Add(new PlayerSeatConsole(56, 1, this))
					.Add(new PlayerSeatConsole(74, 1, this))
					.Add(new PlayerSeatConsole(92, 1, this))
					.Add(new PlayerSeatConsole(92, 15, this))
					.Add(new PlayerSeatConsole(74, 15, this))
					.Add(new PlayerSeatConsole(20, 15, this))
					.Add(new PlayerSeatConsole(2, 15, this));
				foreach (var seat in this.playerseats)
				{
					seat.HighLightColor.Set(this.CommonSeat.HighLightColor);
					seat.InTurnColor.Set(this.CommonSeat.InTurnColor);
					seat.InactiveColor.Set(this.CommonSeat.InactiveColor);
				}
			}
		}

		public override IConObject Update()
		{
			this.ClearArea();
			this.Border.Update();
			foreach (var seat in this.playerseats) seat.Update();
			this.CommonSeat.Update();
			return this;
		}

		public IEnumerator<PlayerSeatConsole> GetEnumerator() { return playerseats.GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }


		CList<PlayerSeatConsole> playerseats;

	}

}
