using System;

namespace Raspware.GameEngine.Input
{
	public static class CollisionHelper
	{
		public static bool Collision(this IPoint point1, IPoint point2)
		{
			var dx = point1.X - point2.X;
			var dy = point1.Y - point2.Y;
			var distance = Math.Sqrt(dx * dx + dy * dy);
			return distance < point1.Radius + point2.Radius;
		}
	}
}
