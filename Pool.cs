namespace Bunbarlang
{
	internal class Pool
	{
		private Logger logger;
		private List<Bet> bets;

		public Pool(Logger logger)
		{
			this.logger = logger;
			Bets = [];
		}

		internal List<Bet> Bets { get => bets; set => bets = value; }

		public void HR_AddBet(Bet bet)
		{
			Bets.Add(bet);
			var betColor = GetBetColor(bet.Horse);
			logger.HR_LogBetPlaced(bet.Horse.Name, bet.Amount, betColor);
		}

		public void BJ_AddBet(Bet bet)
		{
			Bets.Add(bet);
			logger.BJ_LogBetPlaced(bet.Amount);
		}

		public void ResetBet()
        {
            Bets.Clear();
        }

        private ConsoleColor GetBetColor(Horse horse)
		{
			// Színkódok a lovakhoz
			switch (horse.Color.ToLower())
			{
				case "red":
					return ConsoleColor.Red;
				case "blue":
					return ConsoleColor.Blue;
                case "yellow":
                    return ConsoleColor.Yellow;
                case "green":
					return ConsoleColor.Green;
				default:
					return ConsoleColor.White;
			}
		}

		public void HorsePayout(Player player, Horse winner, Logger logger)
		{
			// A fogadások kifizetése a nyertes ló alapján
			foreach (var bet in Bets)
			{
				decimal payout = bet.Amount * winner.Odds;
				if (bet.Horse == winner)
				{
					player.Balance += payout;
					logger.HR_LogBetResult(winner.Name, payout, true);
				}
				else
				{
					player.Balance -= bet.Amount;
					logger.HR_LogBetResult(winner.Name, payout, false);
				}
			}
		}

		public void BlackjackPayout(Player player, Logger logger, bool isWin)
		{
			// A fogadások kifizetése a nyertes ló alapján
			foreach (var bet in Bets)
			{
				decimal payout = bet.Amount;
				if (isWin)
				{
					player.Balance += payout;
					logger.BJ_LogBetResult(payout, true);
				}
				else
				{
					player.Balance -= bet.Amount;
					logger.BJ_LogBetResult(payout, false);
				}
			}
		}
	}

}