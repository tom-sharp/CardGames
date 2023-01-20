using Syslib.Games.Card.TexasHoldEm;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm.Models
{
	public interface ITexasAiDb
	{

		TexasHoldEmAiEntity GetEntry(int id);

		Task<TexasHoldEmAiEntity> GetEntryAsync(int id);

		bool UpdateEntry(TexasHoldEmAiEntity aiEntry);

		Task<bool> UpdateEntryAsync(TexasHoldEmAiEntity aiEntry);

		int SaveChanges();

	}
}