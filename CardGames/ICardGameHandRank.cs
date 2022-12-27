using Syslib;
using Syslib.Games.Card;

namespace Games.Card
{
	public interface ICardGameHandRank
	{
		public void RankHand(CList<IPlayCard> cards);

	}
}
