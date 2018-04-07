using System;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch.Buttons
{
	public sealed class DefaultButtons : IButtons
	{
		private DefaultButtons()
		{
			// TODO: Sort out all OrientationTypes - Use height and width to figure it out
			var defaultRadius = _resolution.RenderAmount(10);
			var actionButtonsRadius = _resolution.RenderAmount(20);
			var directionalButtonsX = _resolution.RenderAmount(2);
			var directionalButtonsY = _resolution.RenderAmount(48);

			Up = new Button(
				(defaultRadius * 3) + directionalButtonsX,
				 directionalButtonsY,
				defaultRadius
			);
			Down = new Button(
				(defaultRadius * 3) + directionalButtonsX,
				(defaultRadius * 4) + directionalButtonsY,
				defaultRadius
			);
			Left = new Button(
				defaultRadius + directionalButtonsX,
				((defaultRadius * 2)) + directionalButtonsY,
				defaultRadius
			);
			Right = new Button(
				(defaultRadius * 5) + directionalButtonsX,
				((defaultRadius * 2)) + directionalButtonsY,
				defaultRadius
			);
			Cancel = new Button(
				_resolution.Width - (defaultRadius + _resolution.RenderAmount(2)),
				defaultRadius + _resolution.RenderAmount(2),
				defaultRadius
			);
			Button1 = new Button(
				_resolution.Width - (actionButtonsRadius + _resolution.RenderAmount(2)),
				_resolution.Height - (actionButtonsRadius + _resolution.RenderAmount(2)),
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