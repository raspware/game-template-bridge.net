using Bridge.Html5;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
{
	public sealed class Events : IEvents, IEventsFullscreen
	{
		private IActionConfigurationTouch _actionConfiguration { get; }
		private HTMLDivElement _wrapper { get; }
		private Resolution _resolution { get; }
		private Layer _layer { get; }

		private bool _isButtonDown = false;
		private bool _isButtonUp = false;
		private bool _isInputDown = false;
		private bool _onceOnButtonDownLock = false;
		private bool _applyFullscreen = false;
		private int _identifier = -1;

		public Events(Resolution resolution, IActionConfigurationTouch actionConfiguration, HTMLCanvasElement controls, HTMLDivElement wrapper)
		{
			_wrapper = wrapper;
			_resolution = resolution;
			_actionConfiguration = actionConfiguration;
		}

		public void InputDown(Bridge.Html5.Touch touch)
		{
			if (_identifier != -1)
				return;

			_isInputDown = true;

			if (!_actionConfiguration.Point.Collision(GetCurrentTouchPosition(touch)))
			{
				if (_isButtonDown)
				{
					_isButtonDown = false;
					_isButtonUp = true;
				}
				return;
			}

			// at this point we know we are on the button
			if (!_isButtonDown)
			{
				_identifier = touch.Identifier;
				_isButtonDown = true;
				_isButtonUp = false;
			}
		}

		public void InputUp(Bridge.Html5.Touch touch)
		{
			if (_identifier == -1 || _identifier != touch.Identifier)
				return;

			_isInputDown = false;

			if (_isButtonDown)
			{
				_isButtonDown = false;
				_isButtonUp = true;
				_onceOnButtonDownLock = false;
				_identifier = -1;
			}

			if (_applyFullscreen)
			{
				_applyFullscreen = false;
				EventsHelper.ApplyFullscreen(_wrapper);
			}
		}

		public void InputMove(Bridge.Html5.Touch touch)
		{
			if (_isButtonDown && !_actionConfiguration.Point.Collision(GetCurrentTouchPosition(touch)) && _identifier == touch.Identifier)
			{
				_isButtonDown = false;
				_isButtonUp = true;
				_identifier = -1;
				return;
			}

			if (!_isButtonDown && _actionConfiguration.Point.Collision(GetCurrentTouchPosition(touch)))
			{
				_isButtonDown = true;
				_isButtonUp = false;
				_identifier = touch.Identifier;
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

		public void ApplyFullscreenOnPressUp() => _applyFullscreen = true;

		private Point GetCurrentTouchPosition(Bridge.Html5.Touch touch)
		{
			return new Point(
				_resolution.GetEventX(_wrapper, touch),
				_resolution.GetEventY(_wrapper, touch),
				_resolution.Multiply(6)
			);
		}
	}
}