using Syslib.Games.Card.TexasHoldEm;
using Syslib;
using Games.Card.TexasHoldEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syslib.Games.Card;
using Microsoft.EntityFrameworkCore;
using Syslib.Games;

namespace Games.Card.TexasHoldEm.Models
{
	public class TexasAiDb : ITexasAiDb, IAiEntryDb
	{

		public TexasAiDb(TexasDbContext ctx)
		{
			this.db = ctx;
		}


		public IAiEntry GetEntry(int id)
		{
			return this.GetEntryAsync(id).Result;
		}


		// Will Update or Add entity if not exist and return the updated Entry
		public IAiEntry UpdateEntry(IAiEntry aiEntry)
		{
			if (aiEntry == null) return null;
			return this.UpdateEntryAsync(aiEntry).Result;
		}


		public void UpdateAndSave(ICollection<IAiEntry> aiEntries) {
			if (aiEntries == null) return;
			foreach (var entity in aiEntries) { 
				this.UpdateEntry(entity); 
			}
			this.SaveChanges();
		}


		public int SaveChanges()
		{
			return this.SaveChangesAsync().Result;
		}


		public async Task UpdateAndSaveAsync(ICollection<IAiEntry> aiEntries)
		{
			if (aiEntries == null) return;
			foreach (var entity in aiEntries)
			{
				await this.UpdateEntryAsync(entity);
			}
			await this.SaveChangesAsync();
		}





		public async Task<IAiEntry> GetEntryAsync(int id)
		{
			return await db.TexasAI.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
		}


		public async Task<IAiEntry> UpdateEntryAsync(IAiEntry aiEntry)
		{
			if (aiEntry == null) return null;
			var exist = await db.TexasAI.FirstOrDefaultAsync(o => o.Id == aiEntry.Id);
			if (exist != null)
			{
				exist.PCount += aiEntry.PCount;
				exist.WCount += aiEntry.WCount;
				aiEntry.PCount = exist.PCount;
				aiEntry.WCount = exist.WCount;
			}
			else
			{
				aiEntry = new TexasHoldEmAiEntity() { Id = aiEntry.Id, PCount = aiEntry.PCount, WCount = aiEntry.WCount };
				await this.db.TexasAI.AddAsync((TexasHoldEmAiEntity)aiEntry);
			}
			return aiEntry;
		}


		public async Task<int> SaveChangesAsync()
		{
			return await this.db.SaveChangesAsync();
		}


		readonly TexasDbContext db;
	}
}
