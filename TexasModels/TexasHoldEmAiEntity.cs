using Syslib.Games;
using Syslib.BaseInterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Games.Card.TexasHoldEm.Models
{
	public class TexasHoldEmAiEntity : IAiEntry, ICopy<TexasHoldEmAiEntity, IAiEntry>
	{
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		public int PCount { get; set; }
		public int WCount { get; set; }

		public TexasHoldEmAiEntity Copy() => new TexasHoldEmAiEntity().Copy(this);

		public TexasHoldEmAiEntity Copy(IAiEntry source)
		{
			this.Id = source.Id;
			this.PCount = source.PCount;
			this.WCount = source.WCount;
			return this;
		}

	}
}

