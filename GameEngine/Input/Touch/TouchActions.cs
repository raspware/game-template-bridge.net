using System;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input
{

	// TODO: Implement this!
	public sealed class TouchActions : IActions
    {
		public static IActions Instance { get; private set; } = null;
		private static bool _configured { get; set; } = false;
		private static Resolution _resolution { get; set; }

		private TouchActions(Resolution resolution)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			Up = new TouchEvents(resolution);
			Down = new TouchEvents(resolution);
			Left = new TouchEvents(resolution);
			Right = new TouchEvents(resolution);
			Escape = new TouchEvents(resolution);
		}

		public static void ConfigureInstance(Resolution resolution)
		{
			if (_configured)
				throw new Exception($"'{nameof(Instance)}' has already been configured!");
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			Instance = new TouchActions(resolution);
			_configured = true;
		}

		public IEvents Up { get; private set; }
        public IEvents Down { get; private set; }
        public IEvents Left { get; private set; }
        public IEvents Right { get; private set; }
        public IEvents Escape { get; private set; }
    }
}
