﻿using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	internal class TexasHoldEmSettings : ITexasHoldEmSettings
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
		public bool LearnAi { get; set; }
		public int SleepTime { get; set; }
		public int MaxBetRaises { get; set; }
		public int MaxBetLimit { get; set; }

		public ITexasHoldEmPlayer Player1 { get; set; }
		public ITexasHoldEmPlayer Player2 { get; set; }
		public ITexasHoldEmPlayer Player3 { get; set; }
		public ITexasHoldEmPlayer Player4 { get; set; }
		public ITexasHoldEmPlayer Player5 { get; set; }
		public ITexasHoldEmPlayer Player6 { get; set; }
		public ITexasHoldEmPlayer Player7 { get; set; }
		public ITexasHoldEmPlayer Player8 { get; set; }
		public ITexasHoldEmPlayer Player9 { get; set; }


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
			LearnAi = false;
			SleepTime = 250;
			MaxBetRaises = 4;
			MaxBetLimit = 0;
			Player1 = null;
			Player2 = null;
			Player3 = null;
			Player4 = null;
			Player5 = null;
			Player6 = null;
			Player7 = null;
			Player8 = null;
			Player9 = null;
		}



	}
}