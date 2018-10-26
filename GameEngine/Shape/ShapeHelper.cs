using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspware.GameEngine.Shape
{
	public static class ShapeHelper
	{
		public static bool Collision(this Circle circle, Circle otherCircle)
		{
			var dx = circle.X - otherCircle.X;
			var dy = circle.Y - otherCircle.Y;
			var distance = Math.Sqrt(dx * dx + dy * dy);

			return distance < circle.Radius + otherCircle.Radius;
		}

		public static bool Collision(this Box box, Box otherBox)
		{
			return box.X < otherBox.X + otherBox.Width &&
			   box.X + box.Width > otherBox.X &&
			   box.Y < otherBox.Y + otherBox.Height &&
			   box.Height + box.Y > otherBox.Y;
		}

		public static bool Collision(this Box box, Circle circle)
		{
			return boxToCircleCollision(box, circle);
		}

		public static bool Collision(this Circle circle, Box box)
		{
			return boxToCircleCollision(box, circle);
		}

		private static bool boxToCircleCollision(Box box, Circle circle)
		{
			var DeltaX = circle.X - Math.Max(box.X, Math.Min(circle.X, box.X + box.Width));
			var DeltaY = circle.Y - Math.Max(box.Y, Math.Min(circle.Y, box.Y + box.Height));
			return (DeltaX * DeltaX + DeltaY * DeltaY) < (circle.Radius * circle.Radius);
		}
	}
}