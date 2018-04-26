using Bridge.Html5;

namespace Raspware.GameEngine.Input.Keyboard

{
	public sealed partial class Events : IEvents
	{
		private bool _keyDown = false;
		private bool _keyUp = false;
		private bool _onceOnKeyDownLock = false;
		private int _keyCode;

		public Events(Buttons keyCode)
		{
			_keyCode = (int)keyCode;
		}

		public void InputKeyDown(KeyboardEvent e)
		{
			if (e.KeyCode != _keyCode)
				return;

			_keyDown = true;
			_keyUp = false;
		}

		public void InputKeyUp(KeyboardEvent e)
		{
			if (e.KeyCode != _keyCode)
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
	}
}