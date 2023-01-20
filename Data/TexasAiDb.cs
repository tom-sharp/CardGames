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

namespace Games.Card.TexasHoldEm.Models
{
	class TexasAiDb : ITexasAiDb
	{
		public TexasAiDb(TexasDbContext ctx)
		{
			this.db = ctx;
		}


		public TexasHoldEmAiEntity GetEntry(int id)
		{
			TexasHoldEmAiEntity result = db.TexasAI.AsNoTracking().FirstOrDefault(o => o.Id == id);
			return result;
		}


		// Will Update or Add entity if not exist
		public bool UpdateEntry(TexasHoldEmAiEntity aiEntry)
		{
			if (aiEntry == null) return false;
			var exist = db.TexasAI.FirstOrDefault(o => o.Id == aiEntry.Id);
			if (exist != null)
			{
				if (aiEntry.PCount < exist.PCount)
				{
					// merge data -  expect aiEntry to be new data
					exist.PCount += aiEntry.PCount;
					exist.WCount += aiEntry.WCount;
				}
				else {
					// add diff . expect aiEntry has been read before and accumulated data
					exist.PCount += aiEntry.PCount - exist.PCount;
					exist.WCount += aiEntry.WCount - exist.WCount;
				}
			}
			else
			{
				this.db.TexasAI.Add(aiEntry);
			}
			
			return true;
		}


		public void UpdateAndSave(CList<TexasHoldEmAiEntity> aiEntries) {
			if (aiEntries == null) return;
			foreach (var entity in aiEntries) { 
				this.UpdateEntry(entity); 
			}
			this.SaveChanges();
		}


		public async Task<TexasHoldEmAiEntity> GetEntryAsync(int id)
		{
			var result = await db.TexasAI.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
			return result;
		}


		// Will Update or Add entity if not exist
		public async Task<bool> UpdateEntryAsync(TexasHoldEmAiEntity aiEntry)
		{
			if (aiEntry == null) return false;
			var exist = await db.TexasAI.FirstOrDefaultAsync(o => o.Id == aiEntry.Id);
			if (exist != null)
			{
				if (aiEntry.PCount < exist.PCount)
				{
					// merge data -  expect aiEntry to be new data
					exist.PCount += aiEntry.PCount;
					exist.WCount += aiEntry.WCount;
				}
				else
				{
					// add diff . expect aiEntry has been read before and accumulated data
					exist.PCount += aiEntry.PCount - exist.PCount;
					exist.WCount += aiEntry.WCount - exist.WCount;
				}
			}
			else
			{
				await this.db.TexasAI.AddAsync(aiEntry);
			}
			return true;
		}


		// Will Update or Add entity if not exist
		public int SaveChanges()
		{
			int result = this.db.SaveChanges();
			return result;
		}


		readonly TexasDbContext db;
	}
}
