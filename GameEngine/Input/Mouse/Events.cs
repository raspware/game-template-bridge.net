using System;
using Bridge.Html5;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Mouse
{
	public sealed class Events : IEvents
	{
		private Actions _button { get; }
		private Resolution _resolution { get; }
		private Layer _layer { get; }

		private bool _isButtonDown = false;
		private bool _isButtonUp = false;
		private bool _isInputDown = false;
		private bool _onceOnButtonDownLock = false;

		public Events(Resolution resolution, Shared.Action button, Layer layer)
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

		public void InputDown(MouseEvent<HTMLCanvasElement> e)
		{
			_isInputDown = true;

			if (!_button.Collision(GetCurrentMousePosition(e)))
				return;

			_isButtonDown = true;
			_isButtonUp = false;
		}

		public void InputUp(MouseEvent<HTMLCanvasElement> e)
		{
			_isInputDown = false;

			if (!_button.Collision(GetCurrentMousePosition(e)))
				return;

			_isButtonDown = false;
			_isButtonUp = true;
			_onceOnButtonDownLock = false;
		}

		public void InputMove(MouseEvent<HTMLCanvasElement> e)
		{
			if (_onceOnButtonDownLock && (!_isInputDown || !_button.Collision(GetCurrentMousePosition(e))))
				_onceOnButtonDownLock = false;

			if (_isInputDown && _button.Collision(GetCurrentMousePosition(e)))
			{
				_isButtonDown = true;
				_isButtonUp = false;
			}
			else
			{
				_isButtonDown = false;
				_isButtonUp = true;
			}
		}
		public bool PressedDown()
		{
			return _isButtonDown;
		}

		public bool PostPressedDown()
		{
			if (_isButtonUp)
			{
				_isButtonUp = false;
				return true;
			}
			return false;
		}

		public bool OnceOnPressDown()
		{
			if (_isInputDown && _isButtonDown && !_onceOnButtonDownLock)
			{
				_onceOnButtonDownLock = true;
				return true;
			}
			return false;
		}

		private Point GetCurrentMousePosition(MouseEvent<HTMLCanvasElement> e)
		{
			return new Point(
				Shared.Position.Instance.GetEventX(e),
				Shared.Position.Instance.GetEventY(e),
				_resolution.RenderAmount(1)
			);
		}
	}
}
