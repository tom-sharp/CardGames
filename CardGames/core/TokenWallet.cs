

namespace Games.Card
{
	public class TokenWallet : ITokenWallet
	{

		public TokenWallet() { this.tokens = 0; }

		public TokenWallet(int tokens) : this() { this.tokens = tokens; }


		public int Clear() { int ret = this.tokens; this.tokens = 0; return ret; }

		public int AddTokens(int tokens) { if (tokens > 0) { this.tokens += tokens; return tokens; } return 0; }

		public int RemoveTokens(int tokens) { if (tokens > 0) { this.tokens -= tokens; return tokens; } return 0; }


		public int Tokens { get { return this.tokens; } }

		int tokens;

	}
}
