namespace Bunbarlang
{
	internal class Horse
	{
		private string name;
		private decimal odds;
		private decimal speed;
		private decimal distanceCovered;
		private string color;


		public Horse(string name, decimal odds, decimal speed, string color)
		{
			Name = name;
			Odds = odds;
			Speed = speed;
			DistanceCovered = 0;
			this.color = color;
		}
		public string Name { get => name; set => name = value; }
		public decimal Odds { get => odds; set => odds = value; }
		public decimal Speed { get => speed; set => speed = value; }
		public decimal DistanceCovered { get => distanceCovered; set => distanceCovered = value; }
        public string Color { get => color; set => color = value; }

        public void Run()
		{
			var stepSize = Speed * new Random().Next(1, 3) * 0.5m; // Kisebb lépések, 0.1-es szorzóval
			DistanceCovered += stepSize; // Növeljük a távolságot a lépések nagyságával
		}

		public override string ToString() 
		{
			return $"{name} - {odds} - ˙{speed} km/h - {distanceCovered}";
		}
	}
}