using Syslib;
using Syslib.Games;
using Syslib.Games.Card;
using Syslib.Games.Card.TexasHoldEm;
using CardGames;
using System.Threading;

namespace Games.Card.TexasHoldEm
{
	//public class TexasHoldEmPlayerRobot : TexasHoldEmPlayer
	//{
	//	public TexasHoldEmPlayerRobot(IPlayer player) : base(player)
	//	{
	//	}

	//	public override bool InTurn(ITexasHoldEmTurnInfo info)
	//	{
	//		if (info.TokensRequired > 0) { this.RequiredBet(info); return true; }
	//		if (CRandom.Random.RandomBool(this.Profile.Randomness)) { RandomDecision(info); return true; }

	//		var mycards = this.Cards.GetCards();
	//		var hand = this.Cards.GetCards().Add(info.CommonCards);

	//		// find out: 2 cards, 2+3, 2+4, 2+5 (flop, turn, river, showdown)
	//		switch (hand.Count())
	//		{
	//			case 2: flop(info, mycards); return true;
	//			case 5: turn(info, mycards, info.CommonCards, hand, info.ActivePlayers); break;
	//			case 6: river(info, mycards, info.CommonCards, hand, info.ActivePlayers); break;
	//			case 7: showdown(info, mycards, info.CommonCards, hand, info.ActivePlayers); break;
	//			default: BugCheck.Critical(this, $"TexasHoldEmRobot::InTurns : Invalid number of cards {hand.Count()}"); break;
	//		}

	//		// for now accept all requests if not resolved above
	//		if (info.TokensRequest > 0) this.CallBet(info);
	//		else CheckBet(info);
	//		return true;
	//	}



	//	// 2 cards
	//	void flop(ITexasHoldEmTurnInfo info, IPlayCards mycards) {

	//		var rank2card = mycards.RankCards(new TexasHoldEmRankOn2Cards()).RankSignature.Rank;
	//		int result = new EvaluateTexasHand().EvaluateFlop(rank2card, this.Profile.Offensive - this.Profile.Defensive, info.ActivePlayers);

	//		if (result < 0) { if (info.TokensRequest > 0) FoldBet(info); }
	//		else if (result == 0) { if (info.TokensRequest > 0) CallBet(info); else CheckBet(info); }
	//		else if (CanRaise(info)) RaiseBet(info.TokensRequest + result * info.TokensBetSize, info);
	//		else if (info.TokensRequest > 0) CallBet(info);
	//		else CheckBet(info);

	//	}


	//	// 5 cards
	//	void river(ITexasHoldEmTurnInfo info, IPlayCards mycards, IPlayCards dealercards, IPlayCards allcards, int players) { 
	//	}

	//	// 6 cards
	//	void turn(ITexasHoldEmTurnInfo info, IPlayCards mycards, IPlayCards dealercards, IPlayCards allcards, int players) { 
	//	}

	//	// 7 cards
	//	void showdown(ITexasHoldEmTurnInfo info, IPlayCards mycards, IPlayCards dealercards, IPlayCards allcards, int players) { 
	//	}





	//}
}
