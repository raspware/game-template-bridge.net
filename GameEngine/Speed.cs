using System;

namespace Raspware.GameEngine
{
	public class Speed
	{
		public double Current { private set; get; }
		public readonly double Max;
		public readonly double Min;
		public readonly double IncreaseBy;
		public readonly double DecreaseBy;

		public Speed(double max, double increaseBy, double min = -1, double decreaseBy = -1)
		{
			if (increaseBy <= 0)
				throw new ArgumentException($"{nameof(increaseBy)} must be greater than zero.");
			if (max <= 0)
				throw new ArgumentException($"{nameof(max)} must be greater than zero.");

			if (min < 0)
				min = 0;

			if (decreaseBy <= 0)
				decreaseBy = increaseBy;

			Max = max;
			Min = min;
			DecreaseBy = decreaseBy;
			IncreaseBy = increaseBy;
		}

		public void Update(int ms, bool increase = false)
		{
			Update((double)ms, increase);
		}

		private void Update(double ms, bool increase = false)
		{
			if (increase)
				Current += (ms * IncreaseBy);
			else
				Current -= (ms * DecreaseBy);

			if (Current < Min)
				Current = Min;
			else if (Current > Max)
				Current = Max;
		}
	}
}