using System;
using Bridge.Html5;

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

		public IEvents Up { get; } = new Events(Events.Keys._upArrow);
		public IEvents Down { get; } = new Events(Events.Keys._downArrow);
		public IEvents Left { get; } = new Events(Events.Keys._leftArrow);
		public IEvents Right { get; } = new Events(Events.Keys._rightArrow);
		public IEvents Cancel { get; } = new Events(Events.Keys._escape);
		public IEvents Button1 { get; } = new Events(Events.Keys._space);
	}
}