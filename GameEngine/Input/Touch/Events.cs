using System;
using System.Collections.Generic;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
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

		private Dictionary<int, Button> _currentTouchers = new Dictionary<int, Button>();

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

		public void InputTouchDown(Bridge.Html5.Touch touch)
		{
			if (!_button.Collision(GetCurrentMousePosition(touch)))
				return;

			_keyDown = true;
			_keyUp = false;
		}

		public void InputTouchUp(Bridge.Html5.Touch touch)
		{
			if (!_button.Collision(GetCurrentMousePosition(touch)))
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

		private TemporaryButton GetCurrentMousePosition(Bridge.Html5.Touch touch)
		{
			return new TemporaryButton(
				Position.Instance.GetEventX(touch),
				Position.Instance.GetEventY(touch),
				_resolution.RenderAmount(1)
			);
		}
	}
}
