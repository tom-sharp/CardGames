using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
	public class TexasHoldEmSettings : ITexasHoldEmSettings
	{
		public TexasHoldEmSettings()
		{
			Default();
		}

		public int RoundsToPlay { get; set; }
		public int TableSeats { get; set; }
		public int Players { get; set; }
		public int Tokens { get; set; }
		public bool EnableStatistics { get; set; }
		public bool UseDb { get; set; }
		public bool Quiet { get; set; }
		public bool QuietNotSummary { get; set; }
		public bool QuietNotStatistics { get; set; }
		public int SleepTime { get; set; }
		public int MaxBetRaises { get; set; }
		public int MaxBetLimit { get; set; }

		void Default()
		{
			RoundsToPlay = 5;
			TableSeats = 9;
			Players = 9;
			Tokens = 1000;
			EnableStatistics = false;
			UseDb = false;
			Quiet = false;
			QuietNotStatistics = false;
			QuietNotSummary = false;
			SleepTime = 250;
			MaxBetRaises = 4;
			MaxBetLimit = 0;
		}



	}
}
