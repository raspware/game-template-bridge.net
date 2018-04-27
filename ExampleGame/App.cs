using System;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Input.SharedButtons;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame
{
	public class App
	{
		public static void Main()
		{
			Resolution.ConfigureInstance(Resolution.PixelSize._FHD, Resolution.OrientationTypes.Landscape);
			Layers.ConfigureInstance();
			DefaultButtons.ConfigureInstance();

			GameEngine.Input.Mouse.Actions.ConfigureInstance(
				DefaultButtons.Instance,
				Layers.Instance.GetLayer(Layers.Id.Controls)
			);
			GameEngine.Input.Touch.Actions.ConfigureInstance(
				DefaultButtons.Instance,
				Layers.Instance.GetLayer(Layers.Id.Controls)
			);

			new Game(
				new GameEngine.Input.Combined.Actions(
					NonNullList.Of(
						GameEngine.Input.Keyboard.Actions.Instance,
						GameEngine.Input.Mouse.Actions.Instance,
						GameEngine.Input.Touch.Actions.Instance
					)
				),
				DefaultButtons.Instance
			);

			//Document.AddEventListener(EventType.TouchStart, TouchTest);
			//Document.AddEventListener(EventType.MouseDown, MouseTest);
		}

		public static void TouchTest(Event e)
		{
			var touchEvent = e.As<TouchEvent>();
			touchEvent.PreventDefault();

			var touches = touchEvent.ChangedTouches;

			var canvas = Document.GetElementById(Layers.Id.Controls.ToString()).As<HTMLCanvasElement>();
			var context = canvas.GetContext(CanvasTypes.CanvasContext2DType.CanvasRenderingContext2D);

			var xPercentage = (double)(touchEvent.PageX - canvas.OffsetLeft) / canvas.OffsetWidth;
			var x = Math.Floor(Resolution.Instance.Width * xPercentage);

			var yPercentage = (double)(touchEvent.PageY - canvas.OffsetTop) / canvas.OffsetHeight;
			var y = Math.Floor(Resolution.Instance.Height * yPercentage);

			for (var i = 0; i < touches.Length; i++)
			{
				var touch = touches[i];
				context.BeginPath();
				context.Arc(
					x,
					y,
					Resolution.Instance.RenderAmount(8),
					0,
					Math.PI * 2
				);
				context.FillStyle = "red";
				context.Fill(CanvasTypes.CanvasFillRule.EvenOdd);
				context.ClosePath();

				Console.WriteLine(touch);
			}

		}

		public static void MouseTest(Event e)
		{
			var mouseEvent = e.As<MouseEvent>();
			mouseEvent.PreventDefault();

			var canvas = Document.GetElementById(Layers.Id.Controls.ToString()).As<HTMLCanvasElement>();
			var context = canvas.GetContext(CanvasTypes.CanvasContext2DType.CanvasRenderingContext2D);

			var xPercentage = (double)(mouseEvent.PageX - canvas.OffsetLeft) / canvas.OffsetWidth;
			var x = Math.Floor(Resolution.Instance.Width * xPercentage);

			var yPercentage = (double)(mouseEvent.PageY - canvas.OffsetTop) / canvas.OffsetHeight;
			var y = Math.Floor(Resolution.Instance.Height * yPercentage);

			context.BeginPath();
			context.Arc(
				x,
				y,
				Resolution.Instance.RenderAmount(8),
				0,
				Math.PI * 2
			);

			context.FillStyle = "red";
			context.Fill(CanvasTypes.CanvasFillRule.EvenOdd);
			context.ClosePath();

			Console.WriteLine(mouseEvent);
		}

	}
}