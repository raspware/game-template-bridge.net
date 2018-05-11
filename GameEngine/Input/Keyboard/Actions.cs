using System;
using System.Collections.Generic;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Keyboard
{
	public sealed class Actions
	{
		public Dictionary<int, IEvents> ActionsEvents { get; private set; }

		public Actions(Layer controls, NonNullList<IActionKeyboard> actions)
		{
			if (controls == null)
				throw new ArgumentNullException(nameof(controls));
			if (actions == null)
				throw new ArgumentNullException(nameof(actions));

			var actionsEvents = new Dictionary<int, IEvents>();
			foreach (var action in actions)
				actionsEvents.Add(action.Id, new Events(action.Key));

			controls.CanvasElement.AddEventListener(EventType.KeyDown, (e) => InputKeyDown((KeyboardEvent)e));
			controls.CanvasElement.AddEventListener(EventType.KeyUp, (e) => InputKeyUp((KeyboardEvent)e));
		}

		private void InputKeyDown(KeyboardEvent e)
		{
			Up.As<Events>().InputDown(e);
			Down.As<Events>().InputDown(e);
			Left.As<Events>().InputDown(e);
			Right.As<Events>().InputDown(e);
			Cancel.As<Events>().InputDown(e);
			Button1.As<Events>().InputDown(e);
		}

		private void InputKeyUp(KeyboardEvent e)
		{
			Up.As<Events>().InputUp(e);
			Down.As<Events>().InputUp(e);
			Left.As<Events>().InputUp(e);
			Right.As<Events>().InputUp(e);
			Cancel.As<Events>().InputUp(e);
			Button1.As<Events>().InputUp(e);
		}

		public IEvents Up { get; } = new Events(Events.Keys._upArrow);
		public IEvents Down { get; } = new Events(Events.Keys._downArrow);
		public IEvents Left { get; } = new Events(Events.Keys._leftArrow);
		public IEvents Right { get; } = new Events(Events.Keys._rightArrow);
		public IEvents Cancel { get; } = new Events(Events.Keys._escape);
		public IEvents Button1 { get; } = new Events(Events.Keys._space);
	}
}