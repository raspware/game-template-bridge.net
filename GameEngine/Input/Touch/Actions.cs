using System;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
{

	// TODO: Implement this!
	public sealed class Actions : IActions
	{
		public static IActions Instance { get; private set; } = null;
		private static bool _configured { get; set; } = false;
		private static Resolution _resolution { get; set; }

		private Actions(Resolution resolution)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			Up = new Events(resolution);
			Down = new Events(resolution);
			Left = new Events(resolution);
			Right = new Events(resolution);
			Cancel = new Events(resolution);
			Button1 = new Events(resolution);
		}

		public static void ConfigureInstance(Resolution resolution)
		{
			if (_configured)
				throw new Exception($"'{nameof(Instance)}' has already been configured!");
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			Instance = new Actions(resolution);
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
