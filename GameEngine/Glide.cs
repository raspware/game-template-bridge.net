using System;

namespace Raspware.GameEngine
{
	public sealed class Glide
	{
		private readonly DynamicDoubleWithConstraints _decrease;
		private readonly DynamicDoubleWithConstraints _increase;

		public Glide(double max, double increaseAmount, double decreaseAmount = -1)
		{
			if (increaseAmount <= 0)
				throw new ArgumentException($"{nameof(increaseAmount)} must be greater than 0");
			if (decreaseAmount <= 0 && decreaseAmount != -1)
				throw new ArgumentException($"{nameof(decreaseAmount)} must be greater than 0");

			_decrease = new DynamicDoubleWithConstraints(max, increaseAmount, decreaseAmount);
			_increase = new DynamicDoubleWithConstraints(max, increaseAmount, decreaseAmount);
		}

		public void Update(int ms)
		{
			if (ms < 1)
				return;

			_increase.Update(ms);
			_decrease.Update(ms);

			Value = _increase.Value - _decrease.Value;
		}

		public void Increase()
		{
			_increase.Increase();
		}

		public void Decrease()
		{
			_decrease.Increase();
		}

		public void Reset()
		{
			_increase.Reset();
			_decrease.Reset();
			Value = 0;
		}

		public double Value { private set; get; }
	}
}