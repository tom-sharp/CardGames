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

namespace Games.Card.TexasHoldEm.Data
{
	public class TexasAiDb : ITexasAiDb
	{

		public TexasAiDb(TexasDbContext ctx)
		{
			this.db = ctx;
		}


		public IAiEntry GetEntry(int id)
		{
			IAiEntry result = null;
			try
			{
				result = this.GetEntryAsync(id).Result;
			}
			catch (AggregateException) 
			{
				return result;
			}
			return result;
		}



		// Will Update or Add entity if not exist and return the updated Entry
		public IAiEntry UpdateEntry(IAiEntry aiEntry)
		{
			if (aiEntry == null) return null;
			IAiEntry result = null;
			try {
				result = this.UpdateEntryAsync(aiEntry).Result;
			}
			catch (AggregateException) {
				return result;
			}
			return result;
		}


		public bool UpdateAndSave(ICollection<IAiEntry> aiEntries) {
			if (aiEntries == null) return true;
			foreach (var entity in aiEntries) { 
				if (this.UpdateEntry(entity) == null) return false; 
			}
			if (this.SaveChanges() < 0) return false;
			return true;
		}


		public int SaveChanges()
		{
			int result;
			try
			{
				result = this.SaveChangesAsync().Result;
			}
			catch (AggregateException) 
			{
				return -1;
			}
			return result;
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


		public bool CanConnect() 
		{
			return this.CanConnectAsync().Result;
		}

		public async Task<bool> CanConnectAsync()
		{
			IAiEntry result;
			try 
			{
				result = await db.TexasAI.AsNoTracking().FirstOrDefaultAsync(o => o.Id != 0);
			}
			catch { return false; }
			if (result == null) return false;
			return true;
		}



		public async Task<IAiEntry> GetEntryAsync(int id)
		{
			return await db.TexasAI.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
		}




		public async Task<IEnumerable<IAiEntry>> GetAllAsync()
		{
			return await db.TexasAI.AsNoTracking().ToListAsync();
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
			int result = 0;
			try
			{
				result = await this.db.SaveChangesAsync();
			}
			catch 
			{
				return -1;
			}
			return result;
		}



		readonly TexasDbContext db;
	}
}
