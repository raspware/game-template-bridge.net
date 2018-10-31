using System.Linq;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Rendering;
using static Bridge.Html5.CanvasTypes;

namespace Raspware.GameEngine
{
	public sealed class TextBox
	{
		private HTMLImageElement _image;
		private Resolution _resolution;
		public double X { private set; get; }
		public double Y { private set; get; }
		public double Width { private set; get; }
		public double Height { private set; get; }
		public NonBlankTrimmedString FillStyle { private set; get; }
		public TextBox(string message, double size, Resolution resolution, NonBlankTrimmedString fillStyle = null)
		{
			_image = new HTMLImageElement()
			{
				Src = CreateImage(message, size),
				OnLoad = (e) =>
				{
					Width = _image.Width;
					Height = _image.Height;
				}
			};

			_resolution = resolution;

			if (fillStyle != null)
				FillStyle = fillStyle;
			else
				FillStyle = new NonBlankTrimmedString("#000");
		}

		private string CreateImage(string message, double size)
		{
			var col = size * 0.575;
			var row = size;
			var lineArray = message.Split('\n');

			var width = (lineArray.Select(_ => _.Length).Max() * col);
			var height = lineArray.Length * row;

			var bufferCanvas = new HTMLCanvasElement() { Width = _resolution.Clamp(width), Height = _resolution.Clamp(height) };
			var bufferContext = bufferCanvas.GetContext(CanvasContext2DType.CanvasRenderingContext2D);

			double colAmount = 0;
			double rowAmount = 0;

			foreach (var line in lineArray)
			{
				foreach (var letter in line)
				{
					DrawLetter(letter, size, colAmount, rowAmount, bufferContext);
					colAmount += col;
				}
				rowAmount += row;
				colAmount = 0;
			}

			return bufferCanvas.ToDataURL();
		}

		private void DrawLetter(char letter, double size, double x, double y, CanvasRenderingContext2D context)
		{
			context.FillStyle = FillStyle.Value;
			context.Font = (size * 1.05) + "px Consolas, monospace";
			context.FillText(letter.ToString(), _resolution.Clamp(x + (size * 0.0045)), _resolution.Clamp(y + (size * 0.775)));
		}

		public void Draw(CanvasRenderingContext2D context)
		{
			context.DrawImage(
				_image,
				X,
				Y,
				Width,
				Height
			);
		}

		public void DrawFull(CanvasRenderingContext2D context, HTMLCanvasElement canvas)
		{
			context.DrawImage(
				_image,
				0,
				0,
				canvas.Width,
				canvas.Height
			);
		}

		public void UpdatePosition(double x, double y)
		{
			X = x;
			Y = y;
		}
	}
}