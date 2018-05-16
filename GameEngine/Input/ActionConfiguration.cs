using System;
using Bridge.Html5;
using Raspware.GameEngine.Input.Keyboard;
using Raspware.GameEngine.Input.Mouse;
using Raspware.GameEngine.Input.Touch;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input
{
	public sealed class ActionConfiguration : IActionConfiguration, IActionConfigurationKeyboard, IActionConfigurationMouse, IActionConfigurationTouch, IActionConfigurationRenderer
	{
		private readonly Resolution _resolution;
		public int Id { get; }
		public Point Point { get; private set; }
		public Keyboard.Events.KeyCodes KeyCode { get; private set; }

		public ActionConfiguration(int id, Keyboard.Events.KeyCodes keyCode, Point point, Resolution resolution)
		{
			if (point == null)
				throw new ArgumentNullException(nameof(point));
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			Id = id;
			Point = point;
			KeyCode = keyCode;
			_resolution = resolution;
		}

		// TODO: Turn this into an override memeber
		public void Render(CanvasRenderingContext2D context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			context.BeginPath();
			context.Arc(Point.X, Point.Y, Point.Radius - _resolution.RenderAmount(0.5), 0, Math.PI * 2);
			context.StrokeStyle = "rgba(255,255,255,0.4)";
			context.LineWidth = _resolution.RenderAmount(1);
			context.Stroke();
			context.ClosePath();

			context.BeginPath();
			context.Arc(Point.X, Point.Y, Point.Radius - _resolution.RenderAmount(1.5), 0, Math.PI * 2);
			context.FillStyle = "rgba(255,255,255,0.4)";
			context.Fill();
			context.ClosePath();
		}
	}
}