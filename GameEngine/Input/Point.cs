namespace Raspware.GameEngine.Input
{
	public class Point: IPoint
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public double Radius { get; private set; }

		public Point(int x, int y, double radius )
		{
			X = x;
			Y = y;
			Radius = radius;
		}
	}
}
