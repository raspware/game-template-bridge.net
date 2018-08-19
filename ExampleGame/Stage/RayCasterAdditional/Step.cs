using System;

namespace Raspware.ExampleGame.Stage.RayCasterAdditional
{
	public sealed class Step
	{
		public Step(double rise, double run, double x, double y, bool inverted)
		{
			if (run == 0)
			{
				// no wall
				Length2 = double.PositiveInfinity;
				return;
			}

			var dx = run > 0 ? Math.Floor(x + 1) - x : Math.Ceiling(x - 1) - x;
			var dy = dx * (rise / run);

			X = inverted ? y + dy : x + dx;
			Y = inverted ? x + dx : y + dy;
			Length2 = dx * dx + dy * dy;
		}

		public double X { get; set; }
		public double Y { get; set; }
		public double Length2 { get; set; }
		public double Shading { get; set; }
		public double Offset { get; set; }
		public double Distance { get; set; }
		public double Height { get; set; }
	}
}
