using System;

namespace Raspware.GameEngine.Rendering
{
	public class Camera
	{
		public Camera(NumberWithConstraints zoom, NumberWithConstraints moveX, NumberWithConstraints moveY)
		{
			if (zoom == null)
				throw new ArgumentNullException(nameof(zoom));
			if (moveX == null)
				throw new ArgumentNullException(nameof(moveX));
			if (moveY == null)
				throw new ArgumentNullException(nameof(moveY));

			Zoom = zoom;
			MoveX = moveX;
			MoveY = moveY;
			X = 0;
			Y = 0;
			ZoomAmount = 1;
		}

		private bool _up;
		private bool _zoomIn;
		private bool _left;

		public NumberWithConstraints Zoom { private set; get; }
		public NumberWithConstraints MoveX { private set; get; }
		public NumberWithConstraints MoveY { private set; get; }
		public int X { private set; get; }
		public int Y { private set; get; }
		public double ZoomAmount { private set; get; }
		public void UpdateYDirection(bool up = false)
		{
			_up = up;
		}

		public void UpdateXDirection(bool left = false)
		{
			_left = left;
		}

		public void UpdateZoom(bool zoomIn = false)
		{
			_zoomIn = zoomIn;
		}

		public void Update()
		{
			// Back to an int as we are dealing with 1px pixels.
			if (_up)
				Y -= (int)MoveY.Current;
			else
				Y += (int)MoveY.Current;

			if (_left)
				X += (int)MoveX.Current;
			else
				X -= (int)MoveX.Current;

			if (_zoomIn)
				ZoomAmount += Zoom.Current;
			else
				ZoomAmount -= Zoom.Current;

			if (ZoomAmount < 1)
				ZoomAmount = 1;

			if (ZoomAmount > 2)
				ZoomAmount = 2;
		}
	}
}