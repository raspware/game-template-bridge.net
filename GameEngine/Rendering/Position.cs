using System;
using Bridge.Html5;

namespace Raspware.GameEngine.Rendering
{
	public static class ResolutionHelper
	{
		public static int GetEventX(this Resolution resolution, HTMLDivElement wrapper, MouseEvent<HTMLCanvasElement> mouseEvent)
		{
			if (wrapper == null)
				throw new ArgumentNullException(nameof(wrapper));

			return _GetEventXOrY(resolution.Width, _GetXorYPercentage(mouseEvent.PageX, mouseEvent.Target.OffsetLeft, mouseEvent.Target.OffsetWidth, wrapper.OffsetLeft));
		}

		public static int GetEventX(this Resolution resolution, HTMLDivElement wrapper, Touch touch)
		{
			if (wrapper == null)
				throw new ArgumentNullException(nameof(wrapper));

			return _GetEventXOrY(resolution.Width, _GetXorYPercentage(touch.PageX, touch.Target.OffsetLeft, touch.Target.OffsetWidth, wrapper.OffsetLeft));
		}

		public static int GetEventY(this Resolution resolution, HTMLDivElement wrapper, MouseEvent<HTMLCanvasElement> mouseEvent)
		{
			if (wrapper == null)
				throw new ArgumentNullException(nameof(wrapper));

			return _GetEventXOrY(resolution.Height, _GetXorYPercentage(mouseEvent.PageY, mouseEvent.Target.OffsetTop, mouseEvent.Target.OffsetHeight, wrapper.OffsetTop));
		}

		public static int GetEventY(this Resolution resolution, HTMLDivElement wrapper, Touch touch)
		{
			if (wrapper == null)
				throw new ArgumentNullException(nameof(wrapper));

			return _GetEventXOrY(resolution.Height, _GetXorYPercentage(touch.PageY, touch.Target.OffsetTop, touch.Target.OffsetHeight, wrapper.OffsetTop));
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