namespace Games.Card
{
	public interface ITokenWallet
	{

		int Clear();

		int AddTokens(int tokens);

		int RemoveTokens(int tokens);


		int Tokens { get; }
	}
}