

namespace Games.Card
{
	class CardPlayerRobot : CardPlayer
	{
		public CardPlayerRobot(string name = null, ITokenWallet wallet = null, ICardPlayerProfile profile = null) : base(CardPlayerType.Robot, profile)
		{
			if (name != null) this.Name = name;
			if (wallet != null) this.Wallet = wallet;
		}
	}
}
