using System;

namespace Raspware.GameEngine
{
	public sealed class DynamicDoubleWithConstraints
	{
		private readonly double _max;
		private readonly double _decreaseAmount;
		private readonly double _increaseAmount;
		private States _state;

		public DynamicDoubleWithConstraints(double max, double increaseAmount, double decreaseAmount = -1)
		{
			if (increaseAmount <= 0)
				throw new ArgumentException($"{nameof(increaseAmount)} must be greater than 0");
			if (decreaseAmount <= 0 && decreaseAmount != -1)
				throw new ArgumentException($"{nameof(decreaseAmount)} must be greater than 0");

			_max = max;
			_increaseAmount = increaseAmount;
			_decreaseAmount = decreaseAmount <= 0 ? increaseAmount : decreaseAmount;
			_state = States.None;
			Value = 0;
		}

		public void Update(int ms)
		{
			if (ms < 1)
				return;

			if (_state == States.Increase)
			{
				Value += ms * _increaseAmount;

				if (Value > _max)
					Value = _max;

				_state = States.Decrease;
			}
			else if (_state == States.Decrease)
			{
				Value -= ms * _decreaseAmount;

				if (Value < 0)
				{
					Value = 0;
					_state = States.None;
				}
			}
		}

		public void Increase()
		{
			_state = States.Increase;
		}

		private enum States
		{
			Increase,
			Decrease,
			None
		}

		public void Reset()
		{
			_state = States.None;
			Value = 0;
		}

		public double Value { private set; get; }
	}
}
