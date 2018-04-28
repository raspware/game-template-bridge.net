using System;
using Bridge.Html5;

namespace Raspware.GameEngine.Input.Shared
{
	// TODO: Finish this
	public class Button
	{
		public Button(int x, int y, double radius)
		{
			X = x;
			Y = y;
			Radius = radius;
		}

		public int X;
		public int Y;
		public double Radius;
		// TODO: Turn this into an override memeber
		public void Render(CanvasRenderingContext2D context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			context.BeginPath();
			context.Arc(X, Y, Radius, 0, Math.PI * 2);
			context.FillStyle = "rgba(255,255,255,0.4)";
			context.Fill();
			context.ClosePath();
		}

		public bool Collision(Button button)
		{
			var dx = X - button.X;
			var dy = Y - button.Y;
			var distance = Math.Sqrt(dx * dx + dy * dy);
			return distance < Radius + button.Radius;
		}
	}
}