namespace Games.Card
{
	public interface ICardPlayerProfile
	{
		/// <summary>
		/// Card player profile name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Cardplayer profile defensive weight 0-100
		/// </summary>
		public int Defensive { get; set; }

		/// <summary>
		/// Cardplayer profile offensive weight 0-100
		/// </summary>
		public int Offensive { get; set; }

		/// <summary>
		/// Indicate how much randomness in decision 0-100
		/// </summary>
		public int Randomness { get; }


		/// <summary>
		/// Weight of profile value -100 - +100
		/// Negative is weight to defensive player
		/// Positive is weight to offensive player
		/// </summary>
		public int Weight { get; }


	}
}