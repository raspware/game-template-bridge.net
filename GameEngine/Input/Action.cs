using System;
using Bridge.Html5;
using Raspware.GameEngine.Input.Keyboard;

namespace Raspware.GameEngine.Input
{
	public sealed class Action : IActionKeyboard, IActionTouchAndMouse
	{
		public int Id { get; }
		public Point Point { get; private set; }
		public Events.Keys Key { get; private set; }

		public Action(int id, Point point, Events.Keys key)
		{
			if (point == null)
				throw new ArgumentNullException(nameof(point));

			Id = id;
			Point = point;
			Key = key;
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