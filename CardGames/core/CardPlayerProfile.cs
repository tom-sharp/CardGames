

namespace Games.Card
{
	public abstract class CardPlayerProfile : ICardPlayerProfile
	{
		public CardPlayerProfile(CardPlayerType cardPlayerType)
		{
			this.Type = cardPlayerType;
		}
		public ICardPlayerType Type { get; }
	}

}
