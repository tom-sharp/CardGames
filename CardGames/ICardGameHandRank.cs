﻿using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Card
{
	public interface ICardGameHandRank
	{
		public void RankHand(CList<Card> cards);

	}
}