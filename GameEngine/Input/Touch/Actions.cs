using System;
using System.Collections.Generic;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
{
	// TODO: Sort this out
	public sealed class Actions
	{
		private static Dictionary<int, DynamicPoint> _currentTouches = new Dictionary<int, DynamicPoint>();
		private Actions(Resolution resolution, Layers layers, NonNullList<IActionTouch> actionConfigurations)
		{


			var controls = layers.Controls.CanvasElement;

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
							resolution.RenderAmount(1)
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
			Up.As<Events>().InputDown(touch);
			
		}

		private void InputTouchUp(Bridge.Html5.Touch touch)
		{
			Up.As<Events>().InputUp(touch);
			
		}

		private void InputTouchMove(Bridge.Html5.Touch touch)
		{
			Up.As<Events>().InputMove(touch);
			
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
