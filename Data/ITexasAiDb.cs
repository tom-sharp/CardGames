using Syslib.Games;
using Syslib.Games.Card.TexasHoldEm;
using System.Collections.Generic;
using System.Threading.Tasks;
using Games.Card.TexasHoldEm.Models;


namespace Games.Card.TexasHoldEm.Data
{
	public interface ITexasAiDb : IAiEntryDb
	{


		/// <summary>
		/// 
		///		CanConnect
		///		Return true if there is at least 1 entity in db
		///		
		/// </summary>
		public bool CanConnect();


		/// <summary>
		/// 
		///		UpdateAndSave
		///		Will update or create entities in Db ánd save it
		///		return true of all succeded
		///		
		/// </summary>
		public bool UpdateAndSave(ICollection<IAiEntry> aiEntries);


		/// <summary>
		/// 
		///		GetEntryAsync
		///		Return Entity with provided id or
		///		null if not found (or there was an error)
		///		
		/// </summary>
		public Task<IAiEntry> GetEntryAsync(int id);


		/// <summary>
		/// 
		///		GetAllAsync
		///		Return all entries in db
		///		
		/// </summary>
		public Task<IEnumerable<IAiEntry>> GetAllAsync();

		/// <summary>
		/// 
		///		UpdateEntryAsync
		///		Update or create entity if not exist
		///		Return updated or created entity
		///		if there was an error null is returned
		///		
		/// </summary>
		public Task<IAiEntry> UpdateEntryAsync(IAiEntry aiEntry);


		public Task UpdateAndSaveAsync(ICollection<IAiEntry> aiEntries);


		/// <summary>
		/// 
		///		SaveChangesAsync
		///		Save changes to Db and return number of saved items
		///		if there was an error -1 is returned
		///		
		/// </summary>
		public Task<int> SaveChangesAsync();

	}
}