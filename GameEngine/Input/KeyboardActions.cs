namespace Raspware.GameEngine.Input
{
	public sealed class KeyboardActions : IActions
	{
		public static IActions Instance { get; } = new KeyboardActions();
		private KeyboardActions() { }

		public IEvents Up { get; } = new KeyboardEvents(38);
		public  IEvents Down { get; } = new KeyboardEvents(40);
		public  IEvents Left { get; } = new KeyboardEvents(37);
		public  IEvents Right { get; } = new KeyboardEvents(39);
		public IEvents Escape { get; } = new KeyboardEvents(27);
	}
}