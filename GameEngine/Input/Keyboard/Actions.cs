using static Raspware.GameEngine.Input.Keyboard.Events;

namespace Raspware.GameEngine.Input.Keyboard
{
	public sealed class Actions : IActions
	{
		public static IActions Instance { get; } = new Actions();
		private Actions() { }

		public IEvents Up { get; } = new Events(Buttons._upArrow);
		public  IEvents Down { get; } = new Events(Buttons._downArrow);
		public  IEvents Left { get; } = new Events(Buttons._leftArrow);
		public  IEvents Right { get; } = new Events(Buttons._rightArrow);
		public IEvents Escape { get; } = new Events(Buttons._escape);
	}
}