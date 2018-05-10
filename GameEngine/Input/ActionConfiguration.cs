using System;
using Bridge.Html5;
using Raspware.GameEngine.Input.Keyboard;
using Raspware.GameEngine.Input.Mouse;

namespace Raspware.GameEngine.Input
{
	public sealed class ActionConfiguration : IActionConfiguration, IActionConfigurationKeyboard, IActionConfigurationMouse
	{
		public int Id { get; }
		public Point Point { get; private set; }
		public Keyboard.Events.KeyCodes KeyCode { get; private set; }

		public ActionConfiguration(int id, Keyboard.Events.KeyCodes keyCode)
		{
			Id = id;
			KeyCode = keyCode;
		}

		public ActionConfiguration(int id, Keyboard.Events.KeyCodes keyCode, Point point)
		{
			if (point == null)
				throw new ArgumentNullException(nameof(point));

			Id = id;
			Point = point;
			KeyCode = keyCode;
		}

		// TODO: Turn this into an override memeber
		public void Render(CanvasRenderingContext2D context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			context.BeginPath();
			context.Arc(Point.X, Point.Y, Point.Radius, 0, Math.PI * 2);
			context.FillStyle = "rgba(255,255,255,0.4)";
			context.Fill();
			context.ClosePath();
		}
	}
}