using System;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch.Buttons
{
	public sealed class DefaultButtons : IButtons
	{
		private DefaultButtons()
		{
			Up = new Button(
				_resolution.RenderAmount(1),
				_resolution.RenderAmount(1),
				_resolution.RenderAmount(1)
			);
		}
		private static bool _configured { get; set; } = false;
		private static Resolution _resolution { get; set; } = null;
		public static DefaultButtons Instance { get; private set; } = null;

		public static void ConfigureInstance(Resolution resolution)
		{
			if (_configured)
				throw new Exception($"'{nameof(Instance)}' has already been configured!");
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			_configured = true;
			_resolution = resolution;
			Instance = new DefaultButtons();
		}


		// TODO: Do these
		public Button Up { get; }
		public Button Down { get; }
		public Button Left { get; } 
		public Button Right { get; } 
		public Button Cancel { get; }
		public Button Button1 { get; }
	}
}