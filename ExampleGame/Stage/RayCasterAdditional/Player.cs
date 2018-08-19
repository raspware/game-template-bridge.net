using System;

namespace Raspware.ExampleGame.Stage.RayCasterAdditional
{
	public sealed class Player
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Direction { get; set; }
		public double Paces { get; set; }
		public Bitmap Weapon { get; set; }


		public Player(double x, double y, double direction) // direction may not be correct
		{
			X = x;
			Y = y;
			Direction = direction;
			Weapon = new Bitmap("assets/knife_hand.png", 319, 320);
			Paces = 0;
		}

		public void Rotate(float angle)
		{
			Direction = (Direction + angle + RayCaster.Circle) % RayCaster.Circle;
		}

		public void Walk(double distance, Map map)
		{
			var dx = Math.Cos(Direction) * distance;
			var dy = Math.Sin(Direction) * distance;

			if (map.Get(X + dx, Y) <= 0)
				X += dx;
			if (map.Get(X, Y + dy) <= 0)
				Y += dy;

			Paces += distance;
		}


	}
}
