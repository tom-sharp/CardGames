using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmConIO : ITexasHoldEmIO
	{
		public TexasHoldEmConIO()
		{
			this.SupressOutput = false;
		}

		public void ShowMsg(string msg)
		{
			if (!SupressOutput) {
				if (msg == null) Console.WriteLine("");
				else Console.WriteLine(msg);
			}
		}

		public bool SupressOutput { get; set; }

	}
}
