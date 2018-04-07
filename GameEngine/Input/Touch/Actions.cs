using System;
using Raspware.GameEngine.Input.Touch.Buttons;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
{

	// TODO: Implement this!
	public sealed class Actions : IActions
	{
		public static IActions Instance { get; private set; } = null;
		private static bool _configured { get; set; } = false;
		private static Resolution _resolution { get; set; }

		private Actions(Resolution resolution, IButtons buttons)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));

			Up = new Events(resolution, buttons.Up);
			Down = new Events(resolution, buttons.Down);
			Left = new Events(resolution, buttons.Left);
			Right = new Events(resolution, buttons.Right);
			Cancel = new Events(resolution, buttons.Cancel);
			Button1 = new Events(resolution, buttons.Button1);
		}

		public static void ConfigureInstance(Resolution _resolution, IButtons buttons)
		{
			if (_configured)
				throw new Exception($"'{nameof(Instance)}' has already been configured!");
			if (_resolution == null)
				throw new ArgumentNullException(nameof(_resolution));
			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));

			Instance = new Actions(_resolution, buttons);
			_configured = true;
		}

		public IEvents Up { get; private set; }
		public IEvents Down { get; private set; }
		public IEvents Left { get; private set; }
		public IEvents Right { get; private set; }
		public IEvents Cancel { get; private set; }
		public IEvents Button1 { get; private set; }
	}
}
