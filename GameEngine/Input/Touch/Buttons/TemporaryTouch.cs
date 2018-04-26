namespace Raspware.GameEngine.Input.Touch.Buttons
{
	public sealed class TemporaryTouch : Button
	{
		public TemporaryTouch(int x, int y, double radius) : base(x, y, radius) { }

		public void Reset(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}