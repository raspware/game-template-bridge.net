using System;

namespace Raspware.GameEngine
{
	public class NumberWithConstraints
	{
		public double Current { private set; get; }
		public readonly double Max;
		public readonly double IncreaseBy;

		public NumberWithConstraints(double max, double increaseBy)
		{
			if (increaseBy <= 0)
				throw new ArgumentException($"{nameof(increaseBy)} must be greater than zero.");
			if (max <= 0)
				throw new ArgumentException($"{nameof(max)} must be greater than zero.");

			Max = max;
			IncreaseBy = increaseBy;
		}

		public void Update(int ms, bool increase = false, bool stopping = false)
		{
			Update((double)ms, increase, stopping);
		}

		public void Update(double ms, bool increase = false, bool stopping = false)
		{
			if (stopping)
			{
				if (Current == 0)
					return;
				else if (Current < 0)
				{
					Current += (ms * (IncreaseBy * 2));
					if (Current > 0)
						Current = 0;
				}
				else if (Current > 0)
				{
					Current -= (ms * (IncreaseBy * 2));
					if (Current < 0)
						Current = 0;
				}
			}
			else if (increase)
			{
				Current += (ms * IncreaseBy);
				if (Current > Max)
					Current = Max;
			}
			else
			{
				Current -= (ms * IncreaseBy);
				if (Current < -Max)
					Current = -Max;
			}
		}
	}
}