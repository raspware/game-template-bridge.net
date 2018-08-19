namespace Raspware.ExampleGame.Stage.RayCasterAdditional
{
	public sealed class Origin
	{
		public Origin(double x, double y, double height, double distance)
		{
			X = x;
			Y = y;
			Height = height;
			Distance = distance;
		}

		public double X { get; set; }
		public double Y { get; set; }
		public double Height { get; set; }
		public double Distance { get; set; }
	}
}