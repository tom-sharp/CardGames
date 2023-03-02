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


		public bool IsEmpty()
		{
			return this.IsEmptyAsync().Result;
		}

		public async Task<bool> IsEmptyAsync()
		{
			IAiEntry result;
			try
			{
				result = await db.TexasAI.AsNoTracking().FirstOrDefaultAsync(o => o.Id != 0);
			}
			catch
			{
				return true;
			}

			if (result == null) return true;
			return false;
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


		public async Task<IAiEntry> GetEntryAsync(int id)
		{
			return await db.TexasAI.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
		}


		public async Task<IEnumerable<IAiEntry>> GetAllAsync()
		{
			return await db.TexasAI.AsNoTracking().ToListAsync();
		}




		// Will Update or Add entity if not exist and return the updated Entry
		public IAiEntry UpdateEntry(IAiEntry aiEntry)
		{
			if (aiEntry == null) return null;
			IAiEntry result = null;
			try 
			{
				result = this.UpdateEntryAsync(aiEntry).Result;
			}
			catch (AggregateException) 
			{
				return result;
			}
			return result;
		}

		public async Task<IAiEntry> UpdateEntryAsync(IAiEntry aiEntry)
		{
			if (aiEntry == null) return null;
			var exist = await db.TexasAI.FirstOrDefaultAsync(o => o.Id == aiEntry.Id);
			if (exist != null)
			{
				if (exist.PCount < 100000) {
					exist.PCount += aiEntry.PCount;
					exist.WCount += aiEntry.WCount;
					aiEntry.PCount = exist.PCount;
					aiEntry.WCount = exist.WCount;
				}
			}
			else
			{
				aiEntry = new TexasHoldEmAiEntity() { Id = aiEntry.Id, PCount = aiEntry.PCount, WCount = aiEntry.WCount };
				await this.db.TexasAI.AddAsync((TexasHoldEmAiEntity)aiEntry);
			}
			return aiEntry;
		}




		public bool Update(ICollection<IAiEntry> aiEntries) {
			if (aiEntries == null) return true;
			foreach (var aientry in aiEntries) 
			{ 
				if (this.UpdateEntry(aientry) == null) return false; 
			}
			return true;
		}




		public async Task<bool> UpdateAsync(ICollection<IAiEntry> aiEntries)
		{
			if (aiEntries == null) return true;
			foreach (var entity in aiEntries)
			{
				try
				{
					if (await this.UpdateEntryAsync(entity) == null) return false;
				}
				catch 
				{
					return false;
				}
			}
			return true;
		}









		public int SaveChanges()
		{
			return this.SaveChangesAsync().Result;
		}

		public async Task<int> SaveChangesAsync()
		{
			int result;
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
