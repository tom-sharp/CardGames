

namespace Games.Card
{
	public class CardPlayerProfile : ICardPlayerProfile
	{
		public CardPlayerProfile()
		{
			defensive = 0;
			offensive = 0;
			randomness = 0;
			this.Name = "Custom";
		}
		public string Name { get; protected set; }

		public int Defensive { 
			get { 
				return this.defensive; 
			} 
			set {
				if (value > 100) this.defensive = 100;
				else if (value < 0) this.defensive = 0;
				else this.defensive = value;
				this.Name = "Custom";
			}
		}
		public int Offensive
		{
			get
			{
				return this.offensive;
			}
			set
			{
				if (value > 100) this.offensive = 100;
				else if (value < 0) this.offensive = 0;
				else this.offensive = value;
				this.Name = "Custom";
			}
		}

		public int Randomness
		{
			get
			{
				return this.randomness;
			}
			set
			{
				if (value > 100) this.randomness = 100;
				else if (value < 0) this.randomness = 0;
				else this.randomness = value;
				this.Name = "Custom";
			}
		}

		public int Weight { get { return this.offensive - this.defensive; }  }
		
		int defensive;
		int offensive;
		int randomness;
	}
}
