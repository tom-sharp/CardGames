using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	interface ICardGameDealer 
	{

		public bool Run(ICardGamePlayer[] players);


	}
}
