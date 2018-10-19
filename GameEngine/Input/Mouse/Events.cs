using System;
using Bridge.Html5;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Mouse
{
	public sealed class Events : IEvents, IEventsFullscreen
	{
		private IActionConfigurationMouse _actionConfiguration { get; }
		private Resolution _resolution { get; }
		private HTMLCanvasElement _controls { get; }
		private HTMLDivElement _wrapper { get; }

		private bool _isButtonDown = false;
		private bool _isButtonUp = false;
		private bool _isInputDown = false;
		private bool _onceOnButtonDownLock = false;
		private bool _applyFullscreen = false;

		public Events(Resolution resolution, IActionConfigurationMouse actionConfiguration, HTMLCanvasElement controls, HTMLDivElement wrapper)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (actionConfiguration == null)
				throw new ArgumentNullException(nameof(actionConfiguration));
			if (controls == null)
				throw new ArgumentNullException(nameof(controls));
			if (wrapper == null)
				throw new ArgumentNullException(nameof(wrapper));

			_resolution = resolution;
			_actionConfiguration = actionConfiguration;
			_controls = controls;
			_wrapper = wrapper;
		}

		public void InputDown(MouseEvent<HTMLCanvasElement> e)
		{
			_isInputDown = true;

			if (!_actionConfiguration.Point.Collision(GetCurrentMousePosition(e)))
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
				_isButtonDown = true;
				_isButtonUp = false;
			}
		}

		public void InputUp(MouseEvent<HTMLCanvasElement> e)
		{
			_isInputDown = false;

			if (_isButtonDown) {
				_isButtonDown = false;
				_isButtonUp = true;
				_onceOnButtonDownLock = false;
			}

			if (_applyFullscreen)
			{
				_applyFullscreen = false;
				EventsHelper.ApplyFullscreen(_wrapper);
			}
		}

		public void InputMove(MouseEvent<HTMLCanvasElement> e)
		{
			if (_isButtonDown && !_actionConfiguration.Point.Collision(GetCurrentMousePosition(e)))
			{
				_isButtonDown = false;
				_isButtonUp = true;
				return;
			}

			if (!_isButtonDown && _actionConfiguration.Point.Collision(GetCurrentMousePosition(e)) && _isInputDown)
			{
				_isButtonDown = true;
				_isButtonUp = false;
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

		private Point GetCurrentMousePosition(MouseEvent<HTMLCanvasElement> e)
		{
			return new Point(
				_resolution.GetEventX(_wrapper, e),
				_resolution.GetEventY(_wrapper, e),
				_resolution.RenderAmount(7)
			);
		}
	}
}