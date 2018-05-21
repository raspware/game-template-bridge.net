using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Html5;
using ProductiveRage.Immutable;
using static Bridge.Html5.CanvasTypes;

namespace Raspware.TestZone
{
	public static class Render_Pipeline
	{
		public static void Go()
		{
			var pipeline = new Pipeline();
			pipeline.Tick();
		}

		private sealed class Pipeline
		{
			private long lastTick = DateTime.Now.Millisecond;
			private readonly Layers _layers;
			private readonly HTMLCanvasElement _canvas;

			public Pipeline()
			{
				_canvas = new HTMLCanvasElement() { Height = 1080, Width = 1920 };
				_layers = new Layers(
					NonNullList.Of(new Layer()),
					_canvas.Width,
					_canvas.Height,
					0.1
				);

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
				var fps = current - lastTick;

				var context = _canvas.GetContext(CanvasContext2DType.CanvasRenderingContext2D);
				context.ClearRect(0, 0, _canvas.Width, _canvas.Height);
				context.Font = "40px Consolas, monospace";

				_layers.Render(context);

				context.FillText(fps + "fps", 20, 40);
				lastTick = current;

				Window.RequestAnimationFrame(Tick);
			}
		}


		private sealed class Layers
		{
			private const double _patternChunkSize = 0.25;

			private readonly int _patternChunkHeight;
			private readonly int _patternChunkWidth;
			private readonly Random _r = new Random();

			private NonNullList<Layer> _layers;
			private List<HTMLImageElement> _patterns = new List<HTMLImageElement>();
			private List<HTMLImageElement> _patternChunks = new List<HTMLImageElement>();
			private bool patternChunksFinishedWith;

			public readonly int PatternHeight;
			public readonly int PatternWidth;
			public readonly int Height;
			public readonly int Width;

			public Layers(NonNullList<Layer> layers, int width, int height, double size)
			{
				if (layers == null)
					throw new ArgumentNullException(nameof(layers));
				if (width == 0)
					throw new ArgumentException(nameof(width));
				if (height == 0)
					throw new ArgumentException(nameof(height));
				if (size == 0)
					throw new ArgumentException(nameof(size));

				_layers = layers;
				Width = width;
				Height = height;
				PatternHeight = int.Parse((Height * size).ToString());
				PatternWidth = int.Parse((Width * size).ToString());
				_patternChunkHeight = int.Parse((PatternHeight * _patternChunkSize).ToString());
				_patternChunkWidth = int.Parse((PatternWidth * _patternChunkSize).ToString());
			}

			public void Render(CanvasRenderingContext2D context)
			{
				if (context == null)
					throw new ArgumentNullException(nameof(context));

				if (_patternChunks.Count < 100 && !patternChunksFinishedWith)
				{
					_patternChunks.Add(CreatePatternChunk());
					context.FillText($"Pattern Chunks: {_patternChunks.Count()}", 20, 80);
					return;
				}

				if (_patterns.Count < 500)
				{
					_patterns.Add(CreatePattern());
					context.FillText($"Patterns: {_patterns.Count()}", 20, 80);
					return;
				}

				if (!_patternChunks.Any())
				{
					patternChunksFinishedWith = true;
					_patternChunks.Clear();
				}

				_layers.ToList().ForEach(_ => _.Render(context, this));
			}

			public HTMLImageElement GetPattern()
			{
				return _patterns[_r.Next(0, _patterns.Count)];
			}

			public HTMLImageElement GetPatternChunk()
			{
				return _patternChunks[_r.Next(0, _patternChunks.Count)];
			}

			private HTMLImageElement CreatePatternChunk()
			{
				var canvas = new HTMLCanvasElement() { Height = _patternChunkHeight, Width = _patternChunkWidth };
				var context = canvas.GetContext(CanvasContext2DType.CanvasRenderingContext2D);
				for (var y = 0; y <= canvas.Height; y++)
					for (var x = 0; x <= canvas.Width; x++)
					{
						context.FillStyle = Pattern.GetRandomGrey(_r);
						context.FillRect(x, y, 1, 1);
					}
				return new HTMLImageElement() { Src = canvas.ToDataURL() };
			}
			private HTMLImageElement CreatePattern()
			{
				var canvas = new HTMLCanvasElement() { Height = PatternHeight, Width = PatternWidth };
				var context = canvas.GetContext(CanvasContext2DType.CanvasRenderingContext2D);

				var width = _patternChunkWidth;
				var height = _patternChunkHeight;
				for (var y = 0; y <= canvas.Height; y += height)
					for (var x = 0; x <= canvas.Width; x += width)
						context.DrawImage(GetPatternChunk(), x, y, width, height);

				return new HTMLImageElement() { Src = canvas.ToDataURL() };
			}
		}

		private sealed class Layer
		{
			public Layer() { }
			public void Render(CanvasRenderingContext2D context, Layers layers)
			{
				if (context == null)
					throw new ArgumentNullException(nameof(context));
				if (layers == null)
					throw new ArgumentNullException(nameof(layers));

				var width = layers.PatternWidth;
				var height = layers.PatternHeight;
				for (var y = 0; y <= layers.Height; y += height)
					for (var x = 0; x <= layers.Width; x += width)
						context.DrawImage(layers.GetPattern(), x, y, width, height);
			}
		}

		private static class Pattern
		{
			public static string GetRandomGrey(Random random)
			{
				if (random == null)
					throw new ArgumentNullException(nameof(random));

				var level = random.Next(100, 210);
				return $"rgb({level},{level},{level})";
			}
		}
	}
}