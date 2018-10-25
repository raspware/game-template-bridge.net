namespace Raspware.GameEngine.Shape
{
	public sealed class Circle : IShape
	{
		public double X { get; private set; }
		public double Y { get; private set; }
		public double Radius { get; private set; }

		public Circle(double x, double y, double radius)
		{
			X = x;
			Y = y;
			Radius = radius;
		}
	}
}