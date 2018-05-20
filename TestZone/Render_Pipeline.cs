using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bridge.Html5;
using ProductiveRage.Immutable;
using static Bridge.Html5.CanvasTypes;

namespace Raspware.TestZone
{
	public static class Render_Pipeline
	{
		private const int Width = 1920;
		private const int Height = 1080;
		public static void Go()
		{
			var pipeline = new Pipeline();
			pipeline.Tick();
		}

		private sealed class Pipeline
		{
			private Layers _layers;
			private readonly HTMLCanvasElement _canvas;

			public Pipeline()
			{
				_layers = new Layers(NonNullList.Of(
					new Layer()
				));

				_canvas = GetNewCanvas();

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
				var time = Stopwatch.StartNew();
				time.Start();
				var context = _canvas.GetContext(CanvasContext2DType.CanvasRenderingContext2D);
				context.ClearRect(0, 0, Width, Height);
				_layers.Render(context);
				time.Stop();
				context.FillText((1000 / time.Elapsed.Milliseconds).ToString() + "fps", 20, 40);
				Window.RequestAnimationFrame(Tick);
			}

		}

		private sealed class Layer
		{
			private readonly HTMLCanvasElement _bufferCanvas;
			public Layer()
			{
				_bufferCanvas = GetNewCanvas();
			}
			public void Render(CanvasRenderingContext2D context, Layers layers)
			{
				if (context == null)
					throw new ArgumentNullException(nameof(context));
				if (layers == null)
					throw new ArgumentNullException(nameof(layers));

				var bufferContext = _bufferCanvas.GetContext(CanvasContext2DType.CanvasRenderingContext2D);
				var width = layers.Width;
				var height = layers.Height;

				for (var y = 0; y <= _bufferCanvas.Height; y += height)
					for (var x = 0; x <= _bufferCanvas.Width; x += width)
						bufferContext.DrawImage(layers.GetPattern(),x, y, width, height);

				bufferContext.Font = "40px Consolas, monospace";
				context.DrawImage(_bufferCanvas, 0, 0);
			}
		}

		private sealed class Layers
		{
			private CanvasRenderingContext2D _bufferContext => GetNewCanvas().GetContext(CanvasContext2DType.CanvasRenderingContext2D);
			private NonNullList<Layer> _layers;
			private List<HTMLImageElement> _patterns = new List<HTMLImageElement>();

			public double Amount = 0.1;
			public int Height;
			public int Width;

			public Layers(NonNullList<Layer> layers)
			{
				if (layers == null)
					throw new ArgumentNullException(nameof(layers));

				_layers = layers;
				Height = int.Parse((GetNewCanvas().Height * Amount).ToString());
				Width = int.Parse((GetNewCanvas().Width * Amount).ToString());
			}

			public void Render(CanvasRenderingContext2D context)
			{
				if (context == null)
					throw new ArgumentNullException(nameof(context));

				if (GetPatternAmount() < 10)
				{
					BuildPattern(this.Height, this.Width);
					context.Font = "40px Consolas, monospace";
					context.FillText($"Patterns: {GetPatternAmount()}", 20, 80);
					return;
				}

				_layers.ToList().ForEach(_ => _.Render(context, this));
			}

			public void BuildPattern(int height, int width)
			{
				var canvas = new HTMLCanvasElement() { Height = height, Width = width };
				var context = canvas.GetContext(CanvasContext2DType.CanvasRenderingContext2D);

				for (var y = 0; y <= canvas.Height; y++)
					for (var x = 0; x <= canvas.Width; x++)
					{
						context.FillStyle = Pattern.GetRandomGrey();
						context.FillRect(x,y,1,1);
					}

				_patterns.Add(new HTMLImageElement()
				{
					Src = canvas.ToDataURL(),
				});
			}

			public HTMLImageElement GetPattern()
			{
				var r = new Random();
				return _patterns[r.Next(0, _patterns.Count)];
			}

			public int GetPatternAmount()
			{
				return _patterns.Count;
			}
		}

		private static class Pattern
		{
			public static string GetRandomGrey()
			{
				var level = new Random().Next(80, 200);
				return $"rgb({level},{level},{level})";
			}
			public static string GetRandomColour()
			{
				var r = new Random().Next(0, 255);
				var g = new Random().Next(0, 255);
				var b = new Random().Next(0, 255);
				return $"rgb({r},{g},{b})";
			}
			public static byte GetRandomByte()
			{
				return byte.Parse(new Random().Next(0, 255).ToString());
			}
		}

		private static HTMLCanvasElement GetNewCanvas(int height = Height, int width = Width)
		{
			return new HTMLCanvasElement() { Height = height, Width = width };
		}
	}
}