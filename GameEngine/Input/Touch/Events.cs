using System;
using Raspware.GameEngine.Input.Touch.Buttons;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
{
	// TODO: Implement this!
	public sealed class Events : IEvents
	{
		private Button _button { get; }
		private Resolution _resolution { get; }

		public Events(Resolution resolution, Button button) {
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (button == null)
				throw new ArgumentNullException(nameof(button));

			_resolution = resolution;
			_button = button;
		}
		public bool OnceOnPressDown()
		{
			return false;
		}

		public bool PostPressedDown()
		{
			return false;
		}

		public bool PressedDown()
		{
			return false;
		}
	}
}
