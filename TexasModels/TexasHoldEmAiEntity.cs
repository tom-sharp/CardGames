using Syslib.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm.Models
{
	public class TexasHoldEmAiEntity : IAiEntry
	{
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		public int PCount { get; set; }
		public int WCount { get; set; }

	}
}

