using static Raspware.GameEngine.Input.KeyboardEvents;

namespace Raspware.GameEngine.Input
{
	public sealed class KeyboardActions : IActions
	{
		public static IActions Instance { get; } = new KeyboardActions();
		private KeyboardActions() { }

		public IEvents Up { get; } = new KeyboardEvents(Button._upArrow);
		public  IEvents Down { get; } = new KeyboardEvents(Button._downArrow);
		public  IEvents Left { get; } = new KeyboardEvents(Button._leftArrow);
		public  IEvents Right { get; } = new KeyboardEvents(Button._rightArrow);
		public IEvents Escape { get; } = new KeyboardEvents(Button._escape);
	}
}