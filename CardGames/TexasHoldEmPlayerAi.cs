using Syslib;
using Syslib.Games.Card;
using Syslib.Games;
using System.Threading;
using Syslib.Games.Card.TexasHoldEm;
using CardGames;

namespace Games.Card.TexasHoldEm
{
	public class TexasHoldEmPlayerAi : TexasHoldEmPlayer
	{

		public TexasHoldEmPlayerAi(ITexasHoldEmAi ai, IPlayer player) : base(player)
		{
			this.AI = ai;
		}

		public override bool GetReady()
		{
			var result = base.GetReady();
			if (this.AI == null) { result = false; }
			if (this.Type != GamePlayerType.Ai) result = false;
			return result;
		}


		public override bool InTurn(ITexasHoldEmTurnInfo info)
		{
			if (info == null) return false;
			if (this.Type != GamePlayerType.Ai) return false;
			if (info.TokensRequired > 0) { this.RequiredBet(info); return true; }
			if (CRandom.Random.RandomBool(this.Profile.Randomness)) { RandomDecision(info); return true; }

			if (this.AI == null) return false;
			if (!this.AI.DbError) this.AI.Learn(playrounds: 10, players: info.NumberOfPlayers);

			var mycards = this.Cards.GetCards();
			var AiSay = this.AI.AskRate(info.NumberOfPlayers, mycards, info.CommonCards);

			AiSay += this.Profile.Offensive - this.Profile.Defensive;
			AiSay += this.Profile.Bluffer;

			if (AiSay < 6)
			{
				if (info.TokensRequest > 0) FoldBet(info);
				else CheckBet(info);
			}
			else if (AiSay < 15)
			{
				if (info.TokensRequest > 0) this.CallBet(info);
				else this.CheckBet(info);
			}
			else if (AiSay < 30)
			{
				if (info.TokensRequest > 0) this.CallBet(info);
				else if (CanRaise(info)) this.RaiseBet(info.TokensRequest + info.TokensBetSize, info);
				else CheckBet(info);
			}
			else if (AiSay < 50)
			{
				if (info.TokensRequest > 0) this.CallBet(info);
				else if (CanRaise(info)) this.RaiseBet(info.TokensRequest + info.TokensBetSize * 2, info);
				else CheckBet(info);
			}
			else if (CanRaise(info)) RaiseBet(info.TokensRequest + info.TokensBetSize * 3, info);
			else if (info.TokensRequest > 0) this.CallBet(info);
			else CheckBet(info);

			return true;

		}

		ITexasHoldEmAi AI;

	}
}
