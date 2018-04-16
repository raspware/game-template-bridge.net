using System;
using Bridge.Html5;

namespace Raspware.TestZone
{
	public static class Touch
	{
		private const string CSS = @"
			 <style>
				html, body, div, canvas, h2 {
					margin: 0;
					padding:0;
				}
				h2 {
					margin: 20px 0;
				}
				div {
					position: relative;
					background-color: #000;
					width: 600px;
					height: 480px;
					margin: 70px 0px 0 80px;
				}
				canvas {
					position: absolute;
					background-color: #7777FF;
					width: 100%;
					height: 80%;
					top: 30px;
				}
			</style>";

		private const int Width = 1980;
		private const int Height = 1080;

		public static void Run()
		{
			var title = new HTMLHeadingElement(HeadingType.H2)
			{
				TextContent = "I am the title"
			};

			var wrapper = new HTMLDivElement()
			{
				Id = "wrapper",
				OnTouchStart = CancelDefault,
				OnTouchEnd = CancelDefault,
				OnTouchCancel = CancelDefault,
				OnTouchEnter = CancelDefault,
				OnTouchLeave = CancelDefault,
				OnTouchMove = CancelDefault
			};

			var background = new HTMLCanvasElement()
			{
				Id = "Background",
				Width = Width,
				Height = Height,
				OnTouchStart = CancelDefault,
				OnTouchEnd = CancelDefault,
				OnTouchCancel = CancelDefault,
				OnTouchEnter = CancelDefault,
				OnTouchLeave = CancelDefault,
				OnTouchMove = CancelDefault
			};

			var foreground = new HTMLCanvasElement()
			{
				Id = "Foreground",
				Width = Width,
				Height = Height,
				OnTouchStart = CancelDefault,
				OnTouchEnd = CancelDefault,
				OnTouchCancel = CancelDefault,
				OnTouchEnter = CancelDefault,
				OnTouchLeave = CancelDefault,
				OnTouchMove = CancelDefault
			};

			var controls = new HTMLCanvasElement()
			{
				Id = "Controls",
				Width = Width,
				Height = Height,
				OnTouchStart = CancelDefault,
				OnTouchEnd = CancelDefault,
				OnTouchCancel = CancelDefault,
				OnTouchEnter = CancelDefault,
				OnTouchLeave = CancelDefault,
				OnTouchMove = CancelDefault,
				OnMouseMove = (ev) =>
				{
					var xPercentage = (double)(ev.PageX - (ev.Target.OffsetLeft + wrapper.OffsetLeft)) / ev.Target.OffsetWidth;
					var x = Convert.ToInt32(Math.Floor(Width * xPercentage));
					var yPercentage = (double)(ev.PageY - (ev.Target.OffsetTop + wrapper.OffsetTop)) / ev.Target.OffsetHeight;
					var y = Convert.ToInt32(Math.Floor(Height * yPercentage));
					var context = ev.Target.GetContext("2d").As<CanvasRenderingContext2D>();
					context.ClearRect(0, 0, Width, Height);
					context.BeginPath();
					context.Arc(x, y, 10, Math.PI * 2, 0);
					context.FillStyle = "red";
					context.ClosePath();
					context.Fill();
				}
			};

			wrapper.AppendChild(background);
			wrapper.AppendChild(foreground);
			wrapper.AppendChild(controls);

			Document.Body.InnerHTML = CSS;
			Document.Body.AppendChild(title);
			Document.Body.AppendChild(wrapper);
			
		}

		private static void CancelDefault(Event e)
		{
			var ev = e.As<TouchEvent>();
			ev.PreventDefault();
		}
	}
}