using System;
using Bridge.Html5;
using static Raspware.GameEngine.Rendering.Resolution;

namespace Raspware.GameEngine.Rendering
{
	public sealed class Layer
	{
		private OrientationTypes _orientation { get; }

		private double _width;
		private double _height;
		private double _top;
		private double _left;

		private Resolution _resolution { get; }

		public HTMLCanvasElement CanvasElement { get; } = new HTMLCanvasElement();

		private HTMLDivElement _wrapper { get; }

		public CanvasRenderingContext2D GetContext() => CanvasElement.GetContext(CanvasTypes.CanvasContext2DType.CanvasRenderingContext2D);
		public int Id { get; }
		public Layer(Resolution resolution, HTMLDivElement wrapper, int id = 0)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (wrapper == null)
				throw new ArgumentNullException(nameof(wrapper));


			_resolution = resolution;
			_orientation = _resolution.Orientation;
			_wrapper = wrapper;

			Id = id;
			CanvasElement.Width = _resolution.Width;
			CanvasElement.Height = _resolution.Height;
			CanvasElement.Style.Position = "absolute";
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

		public void Clear()
		{
			GetContext().ClearRect(0, 0, _resolution.Width, _resolution.Height);
		}

		private void ResizeSquare()
		{
			if (_wrapper.ClientHeight > _wrapper.ClientWidth)
			{
				_width = _wrapper.ClientWidth;
				_height = _wrapper.ClientWidth;
				_top = Math.Floor(((double)_wrapper.ClientHeight - _height) * 0.5);
				return;
			}

			_width = _wrapper.ClientHeight;
			_height = _wrapper.ClientHeight;
			_left = Math.Floor(((double)_wrapper.ClientWidth - _width) * 0.5);
		}

		private void ResizeLandscape()
		{
			var ratioPercent = ((double)_wrapper.ClientHeight / (double)_wrapper.ClientWidth); // target = 0.6
			if (ratioPercent > 0.55 && ratioPercent < 0.65) // This give a little bit of room
			{
				_width = _wrapper.ClientWidth;
				_height = _wrapper.ClientHeight;
			}
			else if (ratioPercent <= 0.55) // Too wide
			{
				_width = Math.Floor(_wrapper.ClientHeight * 1.6);
				_height = _wrapper.ClientHeight;
				_left = Math.Floor(((double)_wrapper.ClientWidth - _width) * 0.5);
			}
			else // Too tall
			{
				_width = _wrapper.ClientWidth;
				_height = Math.Floor(_wrapper.ClientWidth * 0.6);
				_top = Math.Floor(((double)_wrapper.ClientHeight - _height) * 0.5);
			}
		}

		private void ResizePortrait()
		{
			var ratioPercent = ((double)_wrapper.ClientWidth / (double)_wrapper.ClientHeight); // target = 0.6
			if (ratioPercent > 0.55 && ratioPercent < 0.65) // This give a little bit of room
			{
				_width = _wrapper.ClientWidth;
				_height = _wrapper.ClientHeight;
			}
			else if (ratioPercent >= 0.66) // Too wide
			{
				_height = _wrapper.ClientHeight;
				_width = Math.Floor(_wrapper.ClientHeight * 0.6);
				_left = Math.Floor(((double)_wrapper.ClientWidth - _width) * 0.5);
			}
			else // Too tall
			{
				_height = Math.Floor(_wrapper.ClientWidth * 1.6);
				_width = _wrapper.ClientWidth;
				_top = Math.Floor(((double)_wrapper.ClientHeight - _height) * 0.5);
			}
		}
	}
}
