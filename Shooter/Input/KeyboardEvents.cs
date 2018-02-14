using System;
using Bridge.Html5;

namespace Raspware.Shooter.Input
{
	public sealed class KeyboardEvents : IEvents
	{
		private bool _keyDown = false;
		private bool _keyUp = false;
		private bool _onceOnKeyDownLock = false;
		private int _keyCode;

		public KeyboardEvents(int keyCode)
		{
			_keyCode = keyCode;
			Document.AddEventListener(EventType.KeyDown, InputKeyDown);
			Document.AddEventListener(EventType.KeyUp, InputKeyUp);
		}

		private void InputKeyDown(Event e)
		{
			if (e.IsKeyboardEvent() && e.As<KeyboardEvent>().KeyCode == _keyCode)
			{
				_keyDown = true;
				_keyUp = false;
			}
		}

		private void InputKeyUp(Event e)
		{
			if (e.IsKeyboardEvent() && e.As<KeyboardEvent>().KeyCode == _keyCode)
			{
				_keyDown = false;
				_keyUp = true;
				_onceOnKeyDownLock = false;
			}
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
	}
}