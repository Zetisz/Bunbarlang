namespace Bunbarlang
{
	internal class Race
	{
		private List<Horse> horses;
		private Pool bettingPool;
		private Track raceTrack;
		
		public Race(List<Horse> horses, Logger logger)
		{
			this.horses = horses;
			bettingPool = new Pool(logger);
			raceTrack = new Track(logger);
		}

		public Horse? StartRace()
		{
			var winner = raceTrack.StartRace(horses);
			return winner;
		}

        public void Reset()
        {
            foreach (var horse in horses)
            {
                horse.DistanceCovered = 0;
            }

			bettingPool.ResetBet();
        }
	}
}
