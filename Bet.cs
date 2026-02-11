namespace Bunbarlang
{
	internal class Bet
	{
		private decimal amount;
		private Horse? horse;
		private BetType betType;


		public Bet(decimal amount)
		{
			Amount = amount;
			Horse = null;
		}

		public Bet(Horse? horse, decimal amount)
		{
			Horse = horse;
			Amount = amount;
		}

		public decimal Amount { get => amount; set => amount = value; }
		internal Horse? Horse { get => horse; set => horse = value; }
		public BetType BetType { get => betType; set => betType = value; }

	}

	public enum BetType
	{
		Win,
		Place,
		Show
	}

}