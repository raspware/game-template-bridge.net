using System;
using Bridge.Html5;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Shared
{
	public sealed class Position
	{
		public static Position Instance => new Position();

		private Position() { }

		public int GetEventX(MouseEvent<HTMLCanvasElement> mouseEvent)
		{
			return _GetEventXOrY(Resolution.Instance.Width, _GetXorYPercentage(mouseEvent.PageX, mouseEvent.Target.OffsetLeft, mouseEvent.Target.OffsetWidth, Layers.Wrapper.OffsetLeft));
		}

		public int GetEventX(Bridge.Html5.Touch touch)
		{
			return _GetEventXOrY(Resolution.Instance.Width, _GetXorYPercentage(touch.PageX, touch.Target.OffsetLeft, touch.Target.OffsetWidth, Layers.Wrapper.OffsetLeft));
		}

		public int GetEventY(MouseEvent<HTMLCanvasElement> mouseEvent)
		{
			return _GetEventXOrY(Resolution.Instance.Height, _GetXorYPercentage(mouseEvent.PageY, mouseEvent.Target.OffsetTop, mouseEvent.Target.OffsetHeight, Layers.Wrapper.OffsetTop));
		}

		public int GetEventY(Bridge.Html5.Touch touch)
		{
			return _GetEventXOrY(Resolution.Instance.Height, _GetXorYPercentage(touch.PageY, touch.Target.OffsetTop, touch.Target.OffsetHeight, Layers.Wrapper.OffsetTop));
		}

		private int _GetEventXOrY(int widthOrHeight, double xOrYPercentage)
		{
			return Convert.ToInt32(Math.Floor(widthOrHeight * xOrYPercentage));
		}

		private double _GetXorYPercentage(int pageXorY, int targetOffsetLeftOrTop, int targetOffsetWidthOrHeight, int wrapperOffsetLeftOrTop)
		{
			return (double)(pageXorY - (targetOffsetLeftOrTop + wrapperOffsetLeftOrTop)) / targetOffsetWidthOrHeight;
		}
	}
}
