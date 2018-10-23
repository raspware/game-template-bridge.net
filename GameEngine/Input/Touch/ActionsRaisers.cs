using System;
using System.Collections.Generic;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
{
	public sealed class ActionsRaisers : IActionsRaisers
	{
		private Dictionary<int, DynamicPoint> _currentTouches = new Dictionary<int, DynamicPoint>();
		public Dictionary<int, IEvents> Events { get; }
		public ActionsRaisers(Resolution resolution, Layers layers, NonNullList<IActionConfigurationTouch> actionConfigurations)
		{
			if (resolution == null)
				throw new ArgumentException(nameof(resolution));
			if (layers == null)
				throw new ArgumentException(nameof(layers));
			if (actionConfigurations == null)
				throw new ArgumentException(nameof(actionConfigurations));

			var controls = layers.Controls.CanvasElement;
			var actionsEvents = new Dictionary<int, IEvents>();
			foreach (var action in actionConfigurations)
				actionsEvents.Add(action.Id, new Events(resolution, action, controls, layers.Wrapper));

			Events = actionsEvents;

			controls.OnTouchEnd = OnTouchEndLeaveAndCancel;
			controls.OnTouchLeave = OnTouchEndLeaveAndCancel;
			controls.OnTouchCancel = OnTouchEndLeaveAndCancel;

			controls.OnTouchStart = (e) =>
			{
				e.PreventDefault();

				var touches = e.ChangedTouches;
				foreach (var touch in touches)
				{
					if (_currentTouches.ContainsKey(touch.Identifier))
						continue;

					_currentTouches.Add(
						touch.Identifier,
						new DynamicPoint(
							resolution.GetEventX(layers.Wrapper, touch),
							resolution.GetEventY(layers.Wrapper, touch),
							resolution.Multiply(1)
						)
					);

					InputTouchDown(touch);
				}
			};

			controls.OnTouchMove = (e) =>
			{
				var touches = e.ChangedTouches;
				foreach (var touch in touches)
				{
					if (!_currentTouches.ContainsKey(touch.Identifier))
						continue;

					_currentTouches.Get(touch.Identifier)
					.Reset(
						resolution.GetEventX(layers.Wrapper, touch),
						resolution.GetEventY(layers.Wrapper, touch)
					);

					InputTouchMove(touch);
				}
			};
		}

		private void InputTouchDown(Bridge.Html5.Touch touch)
		{
			foreach (var key in Events.Keys)
				Events[key].As<Events>().InputDown(touch);
		}

		private void InputTouchUp(Bridge.Html5.Touch touch)
		{
			foreach (var key in Events.Keys)
				Events[key].As<Events>().InputUp(touch);
		}

		private void InputTouchMove(Bridge.Html5.Touch touch)
		{
			foreach (var key in Events.Keys)
				Events[key].As<Events>().InputMove(touch);
		}

		private void OnTouchEndLeaveAndCancel(TouchEvent<HTMLCanvasElement> touchEvent)
		{
			if (touchEvent == null)
				throw new ArgumentNullException(nameof(touchEvent));

			var touches = touchEvent.ChangedTouches;
			foreach (var touch in touches)
			{
				if (_currentTouches.ContainsKey(touch.Identifier))
					_currentTouches.Remove(touch.Identifier);

				InputTouchUp(touch);
			}
		}
	}
}