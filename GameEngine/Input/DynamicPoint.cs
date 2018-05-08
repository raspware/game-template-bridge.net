namespace Raspware.GameEngine.Input
{
	public sealed class DynamicPoint : IPoint
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public double Radius { get; private set; }

		public DynamicPoint(int x, int y, double radius)
		{
			X = x;
			Y = y;
			Radius = radius;
		}

		public void Reset(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}