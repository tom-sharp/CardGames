namespace Games.Card
{
	interface ICardStack
	{
		int CardsLeft { get; }
		int CardsTotal { get; }

		Card NextCard(bool firstcard = false);
		void ShuffleCards();
		void SortCards();
	}
}