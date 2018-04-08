using System;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch.Buttons
{
	public sealed class DefaultButtons : IButtons
	{
		private DefaultButtons()
		{
			var spacer = _resolution.RenderAmount(2);
			var defaultRadius = _resolution.RenderAmount(8);
			var actionButtonsRadius = _resolution.RenderAmount(16);


			Down = new Button(
				spacer + (defaultRadius * 3),
				_resolution.Height - defaultRadius - spacer,
				defaultRadius
			);

			Up = new Button(
				Down.X,
				Down.Y - (defaultRadius * 4),
				defaultRadius
			);

			Left = new Button(
				defaultRadius + spacer,
				Down.Y - (defaultRadius * 2),
				defaultRadius
			);

			Right = new Button(
				Left.X + (defaultRadius * 4),
				Left.Y,
				defaultRadius
			);
			Cancel = new Button(
				_resolution.Width - defaultRadius - spacer,
				defaultRadius + spacer,
				defaultRadius
			);
			Button1 = new Button(
				_resolution.Width - actionButtonsRadius - spacer,
				_resolution.Height - actionButtonsRadius - spacer,
				actionButtonsRadius
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