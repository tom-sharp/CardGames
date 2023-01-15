namespace CardGames
{
	public interface ITexasHoldEmSettings
	{
		/// <summary>
		/// 
		/// Number of rounds to play before break. 
		/// Set to 0 for unlimited
		/// 
		/// </summary>
		int RoundsToPlay { get; set; }

		/// <summary>
		/// 
		/// Number of table seats card table should be setup for
		/// 
		/// </summary>
		int TableSeats { get; set; }


		/// <summary>
		/// 
		/// Number of card players
		/// 
		/// </summary>
		int Players { get; set; }


		/// <summary>
		/// 
		/// Amount of tokens each player should start with
		/// 
		/// </summary>
		int Tokens { get; set; }


		/// <summary>
		/// 
		/// Enable statistics
		/// 
		/// </summary>
		bool EnableStatistics { get; set; }


		/// <summary>
		/// 
		/// Number of raies a player can do in one round, unlimited if 0
		/// 
		/// </summary>
		int MaxBetRaises { get; set; }


		/// <summary>
		/// 
		/// Max BetRaise Limit, 0 for unlimited
		/// 
		/// </summary>
		int MaxBetLimit { get; set; }


		/// <summary>
		/// 
		/// Run Game without any UI Updates
		/// 
		/// </summary>
		bool Quiet { get; set; }


		/// <summary>
		/// 
		/// Use Quiet Output, but override for round summary
		/// 
		/// </summary>
		bool QuietNotSummary { get; set; }


		/// <summary>
		/// 
		/// Use Quiet Output, but override for statistics summary at end of game
		/// 
		/// </summary>
		bool QuietNotStatistics { get; set; }


		/// <summary>
		/// 
		/// Set game speed for computer player, slowdown in  milliseconds
		/// This is ignored in Quiet mode
		/// 
		/// </summary>
		int SleepTime { get; set; }



		/// <summary>
		/// 
		/// Save statistics to Db
		/// 
		/// </summary>
		bool UseDb { get; set; }

	}
}