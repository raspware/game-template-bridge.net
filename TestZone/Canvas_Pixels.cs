using System;
using Bridge.Html5;
using static Bridge.Html5.CanvasTypes;

namespace Raspware.TestZone
{
	public static class Canvas_Pixels
	{
		public static void Go()
		{
			var pipeline = new Pipeline();
			pipeline.Tick();
		}

		private sealed class Pipeline
		{
			private long lastTick = DateTime.Now.Millisecond;
			private readonly HTMLCanvasElement _canvas;

			public Pipeline()
			{
				_canvas = new HTMLCanvasElement() { Height = 35, Width = 50 };

				var style = new HTMLStyleElement()
				{
					InnerHTML = @"
html, body, canvas{
	margin: 0;
	padding:0;
}
canvas {
	position: absolute;
	background-color: #7777FF;
	width: 100%;
	height: 100%;
	top: 0;
	left: 0
}"
				};

				Document.Head.AppendChild(style);
				Document.Body.AppendChild(_canvas);
			}

			public void Tick()
			{
				var current = DateTime.Now.Millisecond;
				var context = _canvas.GetContext(CanvasContext2DType.CanvasRenderingContext2D);

				context.ClearRect(0, 0, _canvas.Width, _canvas.Height);
				context.Font = "10px Consolas, monospace";

				var r = new Random();
				CanvasTest(context, r);

				context.FillText((1000 / (current - lastTick)) + "fps", 2, 10);
				lastTick = current;

				Window.RequestAnimationFrame(Tick);
			}

			private static void CanvasTest(CanvasRenderingContext2D domContext, Random r)
			{
				var canvasBuffer = new HTMLCanvasElement() { Height = 35, Width = 50 };
				var contextBuffer = canvasBuffer.GetContext(CanvasContext2DType.CanvasRenderingContext2D);
				var imageData = contextBuffer.GetImageData(0, 0, canvasBuffer.Width, canvasBuffer.Height);

				var pixelDataIndex = 0;

				for (var rowIndex = 0; rowIndex < canvasBuffer.Height; rowIndex++)
				{
					for (var colIndex = 0; colIndex < canvasBuffer.Width; colIndex++)
					{
						byte opacity = 0;
						if (rowIndex > 15)
							opacity = (byte)(255 * ((double)rowIndex / canvasBuffer.Height));

						imageData.Data[pixelDataIndex] = 5;
						imageData.Data[pixelDataIndex + 1] = 25;
						imageData.Data[pixelDataIndex + 2] = r.Next(100, 150).As<byte>(); ;
						imageData.Data[pixelDataIndex + 3] = opacity;

						pixelDataIndex += 4; // set the indexes for this pixel
					}
				}

				domContext.PutImageData(imageData, 0, 0);
			}
		}
	}
}