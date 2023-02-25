using Syslib.Games.Card.TexasHoldEm;

namespace Games.Card.TexasHoldEm
{
	public interface ITexasHoldEmSettings : ITexasHoldEmConfig
	{

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
		/// Learn Ai
		/// 
		/// </summary>
		public bool LearnAi { get; set; }

		/// <summary>
		/// 
		/// How many rounds Ai should play for learning before game play
		/// if database fail
		/// 
		/// </summary>
		public int LearnAiFallback { get; set; }


		/// <summary>
		/// 
		/// Create or upgrade db to latest migration
		/// 
		/// </summary>
		bool CreateDb { get; set; }

		/// <summary>
		/// 
		/// Delete Db
		/// 
		/// </summary>
		bool DropDb { get; set; }


	}
}