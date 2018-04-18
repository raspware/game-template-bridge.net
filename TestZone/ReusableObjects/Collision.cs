using System;

namespace Raspware.TestZone.ReusableObjects
{
	public static class Collision
	{
		public static bool Circle(Circle circle1, Circle circle2)
		{
			var dx = circle1.X - circle2.X;
			var dy = circle1.Y - circle2.Y;
			var distance = Math.Sqrt(dx * dx + dy * dy);

			return distance < circle1.Radius + circle2.Radius;
		}
	}
}
