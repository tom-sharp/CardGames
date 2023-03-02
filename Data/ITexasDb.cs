using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Card.TexasHoldEm.Models;


namespace Games.Card.TexasHoldEm.Data
{
	public interface ITexasDb
	{

		/// <summary>
		/// 
		///		AiDb
		///		Data table for Ai client
		///		
		/// </summary>
		ITexasAiDb AiDb { get; }


		bool AddHand(TexasStatisticsEntity hand);

		bool AddPlayRound(TexasPlayRoundEntity playround);

		Task<TexasStatisticsEntity> GetHandAsync(int id);

		Task<IEnumerable<TexasStatisticsEntity>> GetHandsAsync(bool winhand);
		Task<IEnumerable<TexasStatisticsEntity>> GetHandsAsync(bool winhand, byte card1, byte card2);



		/// <summary>
		/// 
		///		SaveChanges
		///		save any pending updates not yet saved
		///		return number of items saved
		///		or -1 on error
		///		
		/// </summary>
		int SaveChanges();


		/// <summary>
		///		
		///		MigrateDb
		///		Try to migrate Db and return
		///		true if successful or false otherwise
		///		
		/// </summary>
		bool MigrateDb();



		/// <summary>
		///		
		///		DeleteDb
		///		Try to delete Db and return
		///		true if successful or false otherwise
		///		
		/// </summary>
		bool DeleteDb();


		/// <summary>
		///		
		///		ConnectDb
		///		Try to connect to Db and return
		///		true if successful or false otherwise
		///		
		/// </summary>
		bool ConnectDb();

	}
}
