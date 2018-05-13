using System;
using System.Collections.Generic;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Keyboard
{
	public sealed class ActionsRaisers : IActionsRaisers
	{
		public Dictionary<int, IEvents> Events { get; private set; }

		public ActionsRaisers(Layers layers, NonNullList<IActionConfigurationKeyboard> actionConfigurations)
		{
			if (layers == null)
				throw new ArgumentNullException(nameof(layers));
			if (actionConfigurations == null)
				throw new ArgumentNullException(nameof(actionConfigurations));

			var actionsEvents = new Dictionary<int, IEvents>();
			foreach (var action in actionConfigurations)
				actionsEvents.Add(action.Id, new Events(action.KeyCode, layers.Wrapper));

			Events = actionsEvents;

			// TODO: Figure out how to apply this to a canvas so it is not so global
			Document.OnKeyDown = (e) => InputKeyDown(e);
			Document.OnKeyUp = (e) => InputKeyUp(e);
		}

		private void InputKeyDown(KeyboardEvent e)
		{
			foreach (var key in Events.Keys)
				Events[key].As<Events>().InputDown(e);
		}

		private void InputKeyUp(KeyboardEvent e)
		{
			foreach (var key in Events.Keys)
				Events[key].As<Events>().InputUp(e);			
		}
	}
}