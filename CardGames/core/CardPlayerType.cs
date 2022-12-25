using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public class CardPlayerType : ICardPlayerType
	{
		public CardPlayerType(string name, int id)
		{
			this.Name = name;
			this.Id = id;
		}
		public string Name { get; }
		public int Id { get; }
	}
}
