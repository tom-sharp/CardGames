using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public interface ITexasHoldEmIO
	{
		public void ShowMsg(string msg);

		public bool SupressOutput { get; set; }
	}
}
