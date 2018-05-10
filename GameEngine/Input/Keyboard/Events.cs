using Bridge.Html5;

namespace Raspware.GameEngine.Input.Keyboard

{
	public sealed partial class Events : IEvents
	{
		private bool _buttonDown = false;
		private bool _buttonUp = false;
		private bool _onceOnButtonDownLock = false;
		private int _keyCode;

		public Events(KeyCodes keyCode)
		{
			_keyCode = (int)keyCode;
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
	}
}