using System;
using Bridge.Html5;
using static Raspware.GameEngine.Input.Keyboard.Events;

namespace Raspware.GameEngine.Input.Keyboard
{
	public sealed class Actions : IActions
	{
		public static IActions Instance { get; } = new Actions();
		private Actions()
		{
			Document.AddEventListener(EventType.KeyDown, (e) => InputKeyDown((KeyboardEvent)e));
			Document.AddEventListener(EventType.KeyUp, (e) => InputKeyUp((KeyboardEvent)e));
		}

		private void InputKeyDown(KeyboardEvent e)
		{
			Up.As<Events>().InputKeyDown(e);
			Down.As<Events>().InputKeyDown(e);
			Left.As<Events>().InputKeyDown(e);
			Right.As<Events>().InputKeyDown(e);
			Cancel.As<Events>().InputKeyDown(e);
			Button1.As<Events>().InputKeyDown(e);
		}

		private void InputKeyUp(KeyboardEvent e)
		{
			Up.As<Events>().InputKeyUp(e);
			Down.As<Events>().InputKeyUp(e);
			Left.As<Events>().InputKeyUp(e);
			Right.As<Events>().InputKeyUp(e);
			Cancel.As<Events>().InputKeyUp(e);
			Button1.As<Events>().InputKeyUp(e);
		}

		public IEvents Up { get; } = new Events(Buttons._upArrow);
		public IEvents Down { get; } = new Events(Buttons._downArrow);
		public IEvents Left { get; } = new Events(Buttons._leftArrow);
		public IEvents Right { get; } = new Events(Buttons._rightArrow);
		public IEvents Cancel { get; } = new Events(Buttons._escape);
		public IEvents Button1 { get; } = new Events(Buttons._space);
	}
}