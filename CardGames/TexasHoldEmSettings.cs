using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syslib.Games.Card.TexasHoldEm;


namespace Games.Card.TexasHoldEm
{
	internal class TexasHoldEmSettings : TexasHoldEmConfig, ITexasHoldEmSettings
	{
		public TexasHoldEmSettings()
		{
			Default();
		}

		public int Players { get; set; }
		public int Tokens { get; set; }
		public bool EnableStatistics { get; set; }
		public bool CreateDb { get; set; }
		public bool DropDb { get; set; }

		public int SleepTime { get; set; }

		public bool Quiet { get; set; }
		public bool QuietNotSummary { get; set; }
		public bool QuietNotStatistics { get; set; }
		public bool LearnAi { get; set; }
		public int LearnAiFallback { get; set; }

		public ITexasHoldEmPlayer Player1 { get; set; }
		public ITexasHoldEmPlayer Player2 { get; set; }
		public ITexasHoldEmPlayer Player3 { get; set; }
		public ITexasHoldEmPlayer Player4 { get; set; }
		public ITexasHoldEmPlayer Player5 { get; set; }
		public ITexasHoldEmPlayer Player6 { get; set; }
		public ITexasHoldEmPlayer Player7 { get; set; }
		public ITexasHoldEmPlayer Player8 { get; set; }



		void Default()
		{
			Players = 8;
			Tokens = 1000;
			EnableStatistics = false;
			CreateDb = false;
			DropDb = false;
			Quiet = false;
			QuietNotStatistics = false;
			QuietNotSummary = false;
			LearnAi = false;
			LearnAiFallback = 2000;
			Player1 = null;
			Player2 = null;
			Player3 = null;
			Player4 = null;
			Player5 = null;
			Player6 = null;
			Player7 = null;
			Player8 = null;


			RoundsToPlay = 1;
			Seats = 8;
			SleepTime = 250;
			BetRaiseCountLimit = 4;
			BetLimit = 0;
			CardStackDecks = 1;
		}



	}
}
