using System;
using Bridge.Html5;

namespace Raspware.GameEngine.Input.Keyboard

{
	public sealed partial class Events : IEvents, IEventsFullscreen
	{
		private bool _buttonDown = false;
		private bool _buttonUp = false;
		private bool _onceOnButtonDownLock = false;
		private bool _applyFullscreen = false;
		private int _keyCode;

		private HTMLDivElement _wrapper;

		public Events(KeyCodes keyCode, HTMLDivElement wrapper)
		{
			if (wrapper == null)
				throw new ArgumentNullException(nameof(wrapper));

			_keyCode = (int)keyCode;
			_wrapper = wrapper;
		}

		public void InputDown(KeyboardEvent e)
		{
			if (e.KeyCode != _keyCode)
				return;

			_buttonDown = true;
			_buttonUp = false;
		}

		public void InputUp(KeyboardEvent e)
		{
			if (e.KeyCode != _keyCode)
				return;

			_buttonDown = false;
			_buttonUp = true;
			_onceOnButtonDownLock = false;

			if (_applyFullscreen)
			{
				_applyFullscreen = false;
				EventsExtensions.ApplyFullscreen(_wrapper);
			}
		}

		public bool PressedDown()
		{
			return _buttonDown;
		}

		public bool PostPressedDown()
		{
			if (_buttonUp)
			{
				_buttonUp = false;
				return true;
			}
			return false;
		}

		public bool OnceOnPressDown()
		{
			if (_buttonDown && !_onceOnButtonDownLock)
			{
				_buttonDown = false;
				_onceOnButtonDownLock = true;
				return true;
			}
			return false;
		}

		public void ApplyFullscreenOnPressUp() => _applyFullscreen = true;
	}
}