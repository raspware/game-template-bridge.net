using System;
using Bridge.Html5;

namespace Raspware.TestZone.ReusableObjects
{
	public static class Position
	{
		public static int GetEventX(int width, MouseEvent<HTMLCanvasElement> mouseEvent, HTMLDivElement wrapper)
		{
			return _GetEventXOrY(width, _GetXorYPercentage(mouseEvent.PageX, mouseEvent.Target.OffsetLeft, mouseEvent.Target.OffsetWidth, wrapper.OffsetLeft));
		}

		public static int GetEventX(int width, TouchEvent<HTMLCanvasElement> touchEvent, HTMLDivElement wrapper)
		{
			return _GetEventXOrY(width, _GetXorYPercentage(touchEvent.PageX, touchEvent.Target.OffsetLeft, touchEvent.Target.OffsetWidth, wrapper.OffsetLeft));
		}

		public static int GetEventY(int height, MouseEvent<HTMLCanvasElement> mouseEvent, HTMLDivElement wrapper)
		{
			return _GetEventXOrY(height, _GetXorYPercentage(mouseEvent.PageY, mouseEvent.Target.OffsetTop, mouseEvent.Target.OffsetHeight, wrapper.OffsetTop));
		}

		public static int GetEventY(int height, TouchEvent<HTMLCanvasElement> touchEvent, HTMLDivElement wrapper)
		{
			return _GetEventXOrY(height, _GetXorYPercentage(touchEvent.PageY, touchEvent.Target.OffsetTop, touchEvent.Target.OffsetHeight, wrapper.OffsetTop));
		}

		private static int _GetEventXOrY(int widthOrHeight, double xOrYPercentage)
		{
			return Convert.ToInt32(Math.Floor(widthOrHeight * xOrYPercentage));
		}

		private static double _GetXorYPercentage(int pageXorY, int targetOffsetLeftOrTop, int targetOffsetWidthOrHeight, int wrapperOffsetLeftOrTop)
		{
			return (double)(pageXorY - (targetOffsetLeftOrTop + wrapperOffsetLeftOrTop)) / targetOffsetWidthOrHeight;
		}
	}
}
