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
				// Fullscreen
				/*@
					var element = this._wrapper;
					if(element.requestFullscreen)
						element.requestFullscreen();
					else if(element.mozRequestFullScreen)
						element.mozRequestFullScreen();
					else if(element.webkitRequestFullscreen)
						element.webkitRequestFullscreen();
					else if(element.msRequestFullscreen)
						element.msRequestFullscreen();
				*/
				_applyFullscreen = false;
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

		public bool CurrentlyFullscreen()
		{
			return (
				(Document.DocumentElement.ClientWidth == Window.Screen.Width && Document.DocumentElement.ClientHeight == Window.Screen.Height) ||
				(Window.InnerWidth == Window.Screen.Width && Window.OuterHeight == Window.Screen.Height)
			);
		}
		public void ExitFullscreen()
		{
			if (!CurrentlyFullscreen())
				return;

			/*@
			 if(document.exitFullscreen) {
				document.exitFullscreen();
			  } else if(document.mozCancelFullScreen) {
				document.mozCancelFullScreen();
			  } else if(document.webkitExitFullscreen) {
				document.webkitExitFullscreen();
			  }
			 */
		}
	}
}