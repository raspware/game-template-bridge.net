using System;
using Bridge.Html5;
using Raspware.GameEngine.Input.Keyboard;
using Raspware.GameEngine.Input.Mouse;
using Raspware.GameEngine.Input.Touch;
using Raspware.GameEngine.Rendering;
using Raspware.GameEngine.Shape;

namespace Raspware.GameEngine.Input
{
	public sealed class ActionConfiguration : IActionConfiguration, IActionConfigurationKeyboard, IActionConfigurationMouse, IActionConfigurationTouch, IActionConfigurationRenderer
	{
		private readonly Resolution _resolution;
		public int Id { get; }
		public Circle Circle { get; private set; }
		public Keyboard.Events.KeyCodes KeyCode { get; private set; }
		private bool _active;

		public ActionConfiguration(int id, Keyboard.Events.KeyCodes keyCode, Circle circle, Resolution resolution)
		{
			if (circle == null)
				throw new ArgumentNullException(nameof(circle));
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			Id = id;
			Circle = circle;
			KeyCode = keyCode;
			_active = true;
			_resolution = resolution;
		}

		// TODO: Turn this into an override memeber
		public void Render(CanvasRenderingContext2D context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			if (_active)
				RenderActive(context);
			else
				RenderInactive(context);
		}



		private void RenderActive(CanvasRenderingContext2D context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			context.BeginPath();
			context.Arc(Circle.X, Circle.Y, Circle.Radius - _resolution.Multiply(0.5), 0, Math.PI * 2);
			context.StrokeStyle = "rgba(255,255,255,0.4)";
			context.LineWidth = _resolution.Multiply(1);
			context.Stroke();
			context.ClosePath();

			context.BeginPath();
			context.Arc(Circle.X, Circle.Y, Circle.Radius - _resolution.Multiply(1.5), 0, Math.PI * 2);
			context.FillStyle = "rgba(255,255,255,0.4)";
			context.Fill();
			context.ClosePath();
		}

		private void RenderInactive(CanvasRenderingContext2D context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			context.BeginPath();
			context.Arc(Circle.X, Circle.Y, Circle.Radius, 0, Math.PI * 2);
			context.FillStyle = "rgba(255,255,255,0.05)";
			context.Fill();
			context.ClosePath();
		}

		public void Activate() { _active = true; }

		public void Deactivate() { _active = false; }
	}
}