namespace Raspware.ExampleGame
{
	public sealed class Data
	{
		public int Score;
		public int Lives;
		public int TimePassed;
		public static Data Instance { get; } = new Data();

		private Data()
		{
			Reset();
		}
		public void Reset()
		{
			Score = 0;
			Lives = 3;
			TimePassed = 0;
		}
	}
}