namespace Games.Card
{
	public interface IPlayerCards
	{
		bool AddCard(Card card);
		bool RemoveCard(Card card);
		void Reset();
	}
}