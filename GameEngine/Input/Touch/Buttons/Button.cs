namespace Raspware.GameEngine.Input.Touch.Buttons
{
	// TODO: Finish this
	public sealed class Button
	{
		public Button(int x, int y, double radius)
		{
			X = x;
			Y = y;
			Radius = radius;
		}
		public int X { get; }
		public int Y { get; }
		public double Radius { get; }
	}
}