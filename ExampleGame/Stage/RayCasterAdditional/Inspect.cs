using System;

namespace Raspware.ExampleGame.Stage.RayCasterAdditional
{
	public sealed class Inspect
	{
		public Inspect(Step step, double shiftX, double shiftY, double distance, double offset) {

			var dx = cos < 0 ? shiftX : 0;
			var dy = sin < 0 ? shiftY : 0;
			step.Height = self.get(step.X - dx, step.Y - dy);
			step.Distance = distance + Math.Sqrt(step.Length2);
			if (shiftX > 0) step.Shading = cos < 0 ? 2 : 0;
			else step.Shading = sin < 0 ? 2 : 1;
			step.Offset = offset - Math.Floor(offset);
			return step;


		}

	}
}
