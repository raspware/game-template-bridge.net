using System;
using Bridge.Html5;

namespace Raspware.GameEngine.Input.Touch.Buttons
{
	// TODO: Finish this
	public sealed class Button
	{
		public Button(int x, int y, double radius)
		{
			X = x;
			Y = y;
			Radius = radius;
		}
		public int X { get; }
		public int Y { get; }
		public double Radius { get; }

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
	}
}