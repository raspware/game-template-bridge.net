using System;
using Bridge.Html5;
using static Raspware.GameEngine.Rendering.Resolution;

namespace Raspware.GameEngine.Rendering
{
	public sealed class Layer
	{
		private HTMLCanvasElement CanvasElement { get; } = new HTMLCanvasElement();

		private OrientationTypes _orientation { get; }

		private double _width;
		private double _height;
		private double _top;
		private double _left;

		public CanvasRenderingContext2D GetContext() => CanvasElement.GetContext(CanvasTypes.CanvasContext2DType.CanvasRenderingContext2D);
		public Layers.Id Id { get; }
		public int Order { get; }

		public Layer(Resolution resolution, Layers.Id id, int order)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			_orientation = resolution.Orientation;
			Id = id;
			Order = order;
			CanvasElement.Width = resolution.Width;
			CanvasElement.Height = resolution.Height;
			CanvasElement.Id = Id.ToString();
			Document.Body.AppendChild(CanvasElement);
			Window.AddEventListener(EventType.Resize, Resize);
			Resize();
		}

		public void Resize()
		{
			_left = 0;
			_top = 0;

			if (_orientation == OrientationTypes.Square)
				ResizeSquare();
			else if (_orientation == OrientationTypes.Landscape)
				ResizeLandscape();
			else if (_orientation == OrientationTypes.Portrait)
				ResizePortrait();
			else
				throw new ArgumentException(nameof(_orientation));

			CanvasElement.Style.Left = _left + "px";
			CanvasElement.Style.Top = _top + "px";
			CanvasElement.Style.Height = _height + "px";
			CanvasElement.Style.Width = _width + "px";
		}

		public void ResizeSquare()
		{
			if (Window.InnerHeight > Window.InnerWidth)
			{
				_width = Window.InnerWidth;
				_height = Window.InnerWidth;
				_top = Math.Floor(((double)Window.InnerHeight - _height) * 0.5);
				return;
			}

			_width = Window.InnerHeight;
			_height = Window.InnerHeight;
			_left = Math.Floor(((double)Window.InnerWidth - _width) * 0.5);
		}

		public void ResizeLandscape()
		{
			var ratioPercent = ((double)Window.InnerHeight / (double)Window.InnerWidth); // target = 0.6
			if (ratioPercent > 0.55 && ratioPercent < 0.65) // This give a little bit of room
			{
				_width = Window.InnerWidth;
				_height = Window.InnerHeight;
			}
			else if (ratioPercent <= 0.55) // Too wide
			{
				_width = Math.Floor(Window.InnerHeight * 1.6);
				_height = Window.InnerHeight;
				_left = Math.Floor(((double)Window.InnerWidth - _width) * 0.5);
			}
			else // Too tall
			{
				_width = Window.InnerWidth;
				_height = Math.Floor(Window.InnerWidth * 0.6);
				_top = Math.Floor(((double)Window.InnerHeight - _height) * 0.5);
			}
		}

		public void ResizePortrait()
		{
			var ratioPercent = ((double)Window.InnerWidth / (double)Window.InnerHeight); // target = 0.6
			if (ratioPercent > 0.55 && ratioPercent < 0.65) // This give a little bit of room
			{
				_width = Window.InnerWidth;
				_height = Window.InnerHeight;
			}
			else if (ratioPercent >= 0.66) // Too wide
			{
				_height = Window.InnerHeight;
				_width = Math.Floor(Window.InnerHeight * 0.6);
				_left = Math.Floor(((double)Window.InnerWidth - _width) * 0.5);
			}
			else // Too tall
			{
				_height = Math.Floor(Window.InnerWidth * 1.6);
				_width = Window.InnerWidth;
				_top = Math.Floor(((double)Window.InnerHeight - _height) * 0.5);
			}
		}
	}
}
