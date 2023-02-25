using Syslib;
using Syslib.Games.Card;
using Syslib.Games;
using Syslib.Games.Card.TexasHoldEm;
using CardGames;


namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayerHuman : TexasHoldEmPlayer
	{
		public TexasHoldEmPlayerHuman(ITexasHoldEmUI inout, IPlayer player):base(player)
		{
			this.IO = inout;
		}

		public override bool InTurn(ITexasHoldEmTurnInfo info)
		{
			if (info == null) return false;
			if (this.Type != GamePlayerType.Human) return false;
			if (info.TokensRequired > 0) { this.RequiredBet(info); return true; }

			int returnbet;

			if (CanRaise(info)) returnbet = this.IO.AskForBet(info.TokensRequest, info.TokensBetSize);
			else returnbet = this.IO.AskForBet(info.TokensRequest, -1);

			if (returnbet < 0) FoldBet(info);
			else if (returnbet == 0)
			{
				if (info.TokensRequest == 0) CheckBet(info);
				else CallBet(info);
			}
			else RaiseBet(returnbet + info.TokensRequest, info);

			return true;

		}

		readonly ITexasHoldEmUI IO;

	}
}
