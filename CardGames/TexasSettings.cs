using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
	public class TexasSettings
	{
		public TexasSettings()
		{
			Default();
		}


		/// <summary>
		/// Number of rounds to play before break. 
		/// Set to 0 for unlimited
		/// </summary>
		public int RoundsToPlay { get; set; }


		/// <summary>
		/// Number of table seats card table should be setup for
		/// </summary>
		public int TableSeats { get; set; }


		/// <summary>
		/// Number of card players
		/// </summary>
		public int Players { get; set; }


		/// <summary>
		/// Amount of tokens each player should start with
		/// </summary>
		public int Tokens { get; set; }


		/// <summary>
		/// Enable statistics
		/// </summary>
		public bool EnableStatistics { get; set; }

		public bool UseDb { get; set; }

		/// <summary>
		/// Quiet output
		/// </summary>
		public bool Quiet { get; set; }


		/// <summary>
		/// Use Quiet Output, but not for round summary
		/// </summary>
		public bool QuietNotSummary { get; set; }


		/// <summary>
		/// Use Quiet Output, but not for statistics summary at end
		/// </summary>
		public bool QuietNotStatistics { get; set; }

		/// <summary>
		/// How long time to slow down gameplay in milliseconds
		/// </summary>
		public int SleepTime { get; set; }


		/// <summary>
		/// Reset to defalt settings
		/// </summary>
		public void Default() {
			RoundsToPlay = 5;
			TableSeats = 8;
			Players = 5;
			Tokens = 1000;
			EnableStatistics = false;
			UseDb = false;
			Quiet = false;
			QuietNotStatistics = false;
			QuietNotSummary = false;
			SleepTime = 250;
		}


		/// <summary>
		/// Process cmdline arguments
		/// return empty string is successful or,
		/// argumet that could not be parsed or "?" for help
		/// </summary>
		public string ProcessArguments(string[] args) {
			if ((args != null) && (args.Length > 0))
			{
				var str = new CStr();
				var filter = new CStr("0123456789");
				foreach (var arg in args)
				{
					str.Str(arg).ToLower();
					if (str.BeginWith("?")) { return "?"; }
					else if (str.BeginWith("-s")) { EnableStatistics = true; QuietNotStatistics = true; }
					else if (str.BeginWith("-db")) UseDb = true;
					else if (str.BeginWith("-qr")) { Quiet = true; QuietNotSummary = true; }
					else if (str.BeginWith("-q")) Quiet = true;
					else if (str.BeginWith("r")) RoundsToPlay = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("s")) TableSeats = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("p")) Players = str.FilterKeep(filter).ToInt32();
					else if (str.BeginWith("t")) Tokens = str.FilterKeep(filter).ToInt32();
					else return arg;
				}
			}

			return "";
		}

	}
}
