using System;
using System.Collections.Generic;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Mouse
{
	public sealed class ActionsRaisers : IActionsRaisers
	{
		public Dictionary<int, IEvents> Events { get; }

		public ActionsRaisers(Resolution resolution, Layers layers, NonNullList<IActionConfigurationMouse> actionConfigurations)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (layers == null)
				throw new ArgumentNullException(nameof(layers));
			if (actionConfigurations == null)
				throw new ArgumentNullException(nameof(actionConfigurations));

			var controls = layers.Controls.CanvasElement;

			var actionsEvents = new Dictionary<int, IEvents>();
			foreach (var action in actionConfigurations)
				actionsEvents.Add(action.Id, new Events(resolution, action, controls, layers.Wrapper));

			Events = actionsEvents;

			controls.OnMouseDown = (e) => InputMouseDown(e);
			controls.OnMouseUp = (e) => InputMouseUp(e);
			controls.OnMouseMove = (e) => InputMouseMove(e);
		}

		private void InputMouseDown(MouseEvent<HTMLCanvasElement> e)
		{
			foreach (var key in Events.Keys)
				Events[key].As<Events>().InputDown(e);
		}

		private void InputMouseUp(MouseEvent<HTMLCanvasElement> e)
		{
			foreach (var key in Events.Keys)
				Events[key].As<Events>().InputUp(e);

		}
		private void InputMouseMove(MouseEvent<HTMLCanvasElement> e)
		{
			foreach (var key in Events.Keys)
				Events[key].As<Events>().InputMove(e);
		}
	}
}