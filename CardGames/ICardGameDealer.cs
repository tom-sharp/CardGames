﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public interface ICardGameDealer 
	{

		public bool Run(CardGameTableSeat[] players);


	}
}