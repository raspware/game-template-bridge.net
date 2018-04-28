using System;
using Bridge.Html5;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Mouse
{
	// TODO: Implement this!
	public sealed class Events : IEvents
	{
		private Button _button { get; }
		private Resolution _resolution { get; }
		private Layer _layer { get; }

		private bool _keyDown = false;
		private bool _keyUp = false;
		private bool _onceOnKeyDownLock = false;

		public Events(Resolution resolution, Button button, Layer layer)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (button == null)
				throw new ArgumentNullException(nameof(button));
			if (layer == null)
				throw new ArgumentNullException(nameof(layer));

			_resolution = resolution;
			_button = button;
			_layer = layer;
		}

		public void InputMouseDown(MouseEvent<HTMLCanvasElement> e)
		{
			if (!_button.Collision(GetCurrentMousePosition(e)))
				return;

			_keyDown = true;
			_keyUp = false;
		}

		public void InputMouseUp(MouseEvent<HTMLCanvasElement> e)
		{
			if (!_button.Collision(GetCurrentMousePosition(e)))
				return;

			_keyDown = false;
			_keyUp = true;
			_onceOnKeyDownLock = false;
		}

		public bool PressedDown()
		{
			return _keyDown;
		}

		public bool PostPressedDown()
		{
			if (_keyUp)
			{
				_keyUp = false;
				return true;
			}
			return false;
		}

		public bool OnceOnPressDown()
		{
			if (_keyDown && !_onceOnKeyDownLock)
			{
				_keyDown = false;
				_onceOnKeyDownLock = true;
				return true;
			}
			return false;
		}

		private TemporaryButton GetCurrentMousePosition(MouseEvent<HTMLCanvasElement> e)
		{
			return new TemporaryButton(
				Shared.Position.Instance.GetEventX(e),
				Shared.Position.Instance.GetEventY(e),
				_resolution.RenderAmount(1)
			);
		}
	}
}
