using Syslib.Games;
using Syslib.Games.Card.TexasHoldEm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm.Models
{
	public interface ITexasAiDb : IAiEntryDb
	{


		void UpdateAndSave(ICollection<IAiEntry> aiEntries);



		Task<IAiEntry> GetEntryAsync(int id);

		Task<IAiEntry> UpdateEntryAsync(IAiEntry aiEntry);

		Task UpdateAndSaveAsync(ICollection<IAiEntry> aiEntries);

		Task<int> SaveChangesAsync();

	}
}