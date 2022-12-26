

namespace Games.Card
{
	class CardPlayerHuman : CardPlayer
	{
		public CardPlayerHuman(string name = null, ITokenWallet wallet = null) : base(CardPlayerType.Human, null)
		{
			if (name != null) this.Name = name;
			if (wallet != null) this.Wallet = wallet;
		}
	}
}
