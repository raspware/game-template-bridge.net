namespace Raspware.GameEngine.Input.Shared
{
	public sealed class TemporaryButton : Button
	{
		public TemporaryButton(int x, int y, double radius) : base(0, x, y, radius) { }

		public void Reset(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}