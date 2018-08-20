using System;
using Bridge.Html5;

namespace Raspware.ExampleGame.Stage.RayCasterAdditional
{
	public sealed class Map
	{
		public double Size { get; set; }
		public Uint8Array WallGrid { get; set; }
		public Bitmap SkyBox { get; set; }
		public Bitmap WallTexture { get; set; }
		public double Light { get; set; }
		public Map(double size)
		{
			Size = size;
			WallGrid = new Uint8Array(size * size);
			SkyBox = new Bitmap("assets/deathvalley_panorama.jpg", 2000, 750);
			WallTexture = new Bitmap("assets/wall_texture.jpg", 1024, 1024);
			Light = 0;
		}
		public int Get(double x, double y)
		{
			x = Math.Floor(x);
			y = Math.Floor(y);

			if (x < 0 || x > Size - 1 || y < 0 || y > Size - 1)
				return -1;

			return (int)WallGrid[(y * Size + x).ToString()];
		}

		public void Randomise()
		{
			for (var i = 0; i < Size * Size; i++)
				WallGrid[i] = (byte)(Math.Random() < 0.3 ? 1 : 0);
		}

		public void Update(float seconds)
		{
			if (Light > 0)
				Light = Math.Max(Light - 10 * seconds, 0);
			else if (Math.Random() * 5 < seconds)
				Light = 2;
		}

		public Origin[] Cast(Player point, double angle, double range)
		{
			var self = this;
			var sin = Math.Sin(angle);
			var cos = Math.Cos(angle);

			return Ray(new Origin(point.X, point.Y, 0, 0), self, sin, cos, range);
		}

		private Origin[] Ray(Origin origin, Map map, double sin, double cos, double range)
		{
			var stepX = new Step(sin, cos, origin.X, origin.Y, false);
			var stepY = new Step(cos, sin, origin.Y, origin.Y, true);
			var nextStep = stepX.Length2 < stepY.Length2 ?
				Inspect(map, sin, cos, stepX, 1, 0, origin.Distance, stepX.Y)
			: Inspect(map, sin, cos, stepY, 0, 1, origin.Distance, stepY.X);

			if (nextStep.Distance > range) return new[] { origin };
			return new Origin[] { origin, new Origin(nextStep.X, nextStep.Y, nextStep.Height, nextStep.Distance) };
		}

		private Step Inspect(Map map, double sin, double cos, Step step, double shiftX, double shiftY, double distance, double offset)
		{
			var dx = cos < 0 ? shiftX : 0;
			var dy = sin < 0 ? shiftY : 0;
			step.Height = map.Get(step.X - dx, step.Y - dy);
			step.Distance = distance + Math.Sqrt(step.Length2);
			if (shiftX > 0) step.Shading = cos < 0 ? 2 : 0;
			else step.Shading = sin < 0 ? 2 : 1;
			step.Offset = offset - Math.Floor(offset);
			return step;
		}

		public void Update(double seconds)
		{
			if (Light > 0) Light = Math.Max(Light - 10 * seconds, 0);
			else if (Math.Random() * 5 < seconds) Light = 2;
		}
	}
}