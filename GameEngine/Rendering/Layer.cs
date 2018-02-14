using System;
using Bridge.Html5;

namespace Raspware.GameEngine.Rendering
{
	public sealed class Layer
	{
		private HTMLCanvasElement CanvasElement { get; } = new HTMLCanvasElement();

		public CanvasRenderingContext2D GetContext() => CanvasElement.GetContext(CanvasTypes.CanvasContext2DType.CanvasRenderingContext2D);
		public Layers.Id Id { get; }
		public int Order { get; }

		public Layer(Resolution resolution, Layers.Id id, int order)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

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
			var ratioPercent = ((double)Window.InnerHeight / (double)Window.InnerWidth); // target = 0.6
			double width;
			double height;
			double left = 0;
			double top = 0;

			if (ratioPercent > 0.55 && ratioPercent < 0.65) // This give a little bit of room
			{
				width = Window.InnerWidth;
				height = Window.InnerHeight;
			}
			else if (ratioPercent <= 0.55) // Too wide
			{
				width = Math.Floor(Window.InnerHeight * 1.6);
				height = Window.InnerHeight;
				left = Math.Floor(((double)Window.InnerWidth - width) * 0.5);
			}
			else // Too tall
			{
				width = Window.InnerWidth;
				height = Math.Floor(Window.InnerWidth * 0.6);
				top = Math.Floor(((double)Window.InnerHeight - height) * 0.5);
			}

			CanvasElement.Style.Left = left + "px";
			CanvasElement.Style.Top = top + "px";
			CanvasElement.Style.Height = height + "px";
			CanvasElement.Style.Width = width + "px";
		}
	}
}
