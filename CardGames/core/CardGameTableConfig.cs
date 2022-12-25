

namespace Games.Card
{
	public class CardGameTableConfig
	{
		public int Seats { get; set; }


		public bool IsValid() {
			if ((this.Seats >= 0) && (this.Seats < 30)) return true;
			return false;
		}

	}
}
