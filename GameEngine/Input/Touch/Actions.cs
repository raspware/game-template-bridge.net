using System;
using System.Collections.Generic;
using Bridge.Html5;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
{

	// TODO: Implement touch move.
	public sealed class Actions : IActions
	{
		public static IActions Instance { get; private set; }
		private static bool _configured { get; set; } = false;
		private static Dictionary<int, TemporaryButton> _currentTouches = new Dictionary<int, TemporaryButton>();
		private Actions(Resolution resolution, IButtons buttons, Layer layer)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));
			if (layer == null)
				throw new ArgumentNullException(nameof(layer));

			Up = new Events(resolution, buttons.Up, layer);
			Down = new Events(resolution, buttons.Down, layer);
			Left = new Events(resolution, buttons.Left, layer);
			Right = new Events(resolution, buttons.Right, layer);
			Cancel = new Events(resolution, buttons.Cancel, layer);
			Button1 = new Events(resolution, buttons.Button1, layer);

			layer.CanvasElement.OnTouchEnd = OnTouchEndLeaveAndCancel;
			layer.CanvasElement.OnTouchLeave = OnTouchEndLeaveAndCancel;
			layer.CanvasElement.OnTouchCancel = OnTouchEndLeaveAndCancel;

			layer.CanvasElement.OnTouchStart = (e) =>
			{
				e.PreventDefault();

				var touches = e.ChangedTouches;
				foreach (var touch in touches)
				{
					if (_currentTouches.ContainsKey(touch.Identifier))
						continue;

					_currentTouches.Add(
						touch.Identifier,
						new TemporaryButton(
							Shared.Position.Instance.GetEventX(touch),
							Shared.Position.Instance.GetEventY(touch),
							resolution.RenderAmount(1)
						)
					);

					InputTouchDown(touch);
				}
			};

			layer.CanvasElement.OnTouchMove = (e) =>
			{
				var touches = e.ChangedTouches;
				foreach (var touch in touches)
				{
					if (!_currentTouches.ContainsKey(touch.Identifier))
						continue;

					_currentTouches.Get(touch.Identifier)
					.Reset(
						Shared.Position.Instance.GetEventX(touch),
						Shared.Position.Instance.GetEventY(touch)
					);
				}
			};
		}

		private void InputTouchDown(Bridge.Html5.Touch touch)
		{
			Up.As<Events>().InputTouchDown(touch);
			Down.As<Events>().InputTouchDown(touch);
			Left.As<Events>().InputTouchDown(touch);
			Right.As<Events>().InputTouchDown(touch);
			Cancel.As<Events>().InputTouchDown(touch);
			Button1.As<Events>().InputTouchDown(touch);
		}

		private void InputTouchUp(Bridge.Html5.Touch touch)
		{
			Up.As<Events>().InputTouchUp(touch);
			Down.As<Events>().InputTouchUp(touch);
			Left.As<Events>().InputTouchUp(touch);
			Right.As<Events>().InputTouchUp(touch);
			Cancel.As<Events>().InputTouchUp(touch);
			Button1.As<Events>().InputTouchUp(touch);
		}

		public static void ConfigureInstance(IButtons buttons, Layer layer)
		{
			if (_configured)
				throw new Exception($"'{nameof(Instance)}' has already been configured!");
			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));

			Instance = new Actions(Resolution.Instance, buttons, layer);
			_configured = true;
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

		public IEvents Up { get; private set; }
		public IEvents Down { get; private set; }
		public IEvents Left { get; private set; }
		public IEvents Right { get; private set; }
		public IEvents Cancel { get; private set; }
		public IEvents Button1 { get; private set; }
	}
}
