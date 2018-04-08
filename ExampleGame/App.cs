using System;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Input.Touch.Buttons;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame
{
	public class App
	{
		public static void Main()
		{
			Resolution.ConfigureInstance(Resolution.PixelSize._FHD, Resolution.OrientationTypes.Landscape);
			var resolution = Resolution.Instance;

			// Resolution Defaults
			DefaultButtons.ConfigureInstance(resolution);
			var buttons = DefaultButtons.Instance;

			GameEngine.Input.Touch.Actions.ConfigureInstance(resolution, buttons);

			Layers.ConfigureInstance(resolution);
			new Game(
				Data.Instance,
				Layers.Instance,
				resolution,
				new GameEngine.Input.Combined.Actions(
					NonNullList.Of(
						GameEngine.Input.Keyboard.Actions.Instance,
						GameEngine.Input.Touch.Actions.Instance
					)
				),
				buttons
			);

			Document.AddEventListener(EventType.TouchStart, TouchTest);
			Document.AddEventListener(EventType.MouseDown, MouseTest);
		}

		public static void TouchTest(Event e)
		{
			var touchEvent = e.As<TouchEvent>();
			touchEvent.PreventDefault();
			var touches = touchEvent.ChangedTouches;

			var canvas = Document.GetElementById(Layers.Id.Test.ToString()).As<HTMLCanvasElement>();
			var context = canvas.GetContext(CanvasTypes.CanvasContext2DType.CanvasRenderingContext2D);

			for (var i = 0; i < touches.Length; i++)
			{
				var touch = touches[i];
				context.BeginPath();
				context.Arc(
					(Resolution.Instance.Width / canvas.OffsetWidth) * touch.PageX,
					(Resolution.Instance.Height / canvas.OffsetHeight) * touch.PageY,
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

			var canvas = Document.GetElementById(Layers.Id.Test.ToString()).As<HTMLCanvasElement>();
			var context = canvas.GetContext(CanvasTypes.CanvasContext2DType.CanvasRenderingContext2D);

			context.BeginPath();
			context.Arc(
				(Resolution.Instance.Width / canvas.OffsetWidth) * mouseEvent.PageX,
				(Resolution.Instance.Height / canvas.OffsetHeight) * mouseEvent.PageY,
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