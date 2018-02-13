using System;
using Raspware.Shooter.Rendering;

namespace Raspware.Shooter.Input
{

	// TODO: Implement this!
	public sealed class TouchActions : IActions
    {
		public static IActions Instance { get; private set; } = null;
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

		private static Resolution _resolution { get; set; }

		public static void ConfigureInstance(Resolution resolution)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			Instance = new TouchActions(resolution);
		}

		public IEvents Up { get; private set; }

        public IEvents Down { get; private set; }

        public IEvents Left { get; private set; }

        public IEvents Right { get; private set; }

        public IEvents Escape { get; private set; }
    }
}
