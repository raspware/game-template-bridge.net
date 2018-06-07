namespace Raspware.GameEngine
{
	public class Speed
	{
		public readonly double Amount;
		public double Current { private set; get; }
		public readonly double Max;
		public readonly double Min;

		public Speed(double amount, double max, double min = 0)
		{
			Amount = amount;
			Max = max;
			Min = min;
			Current = 0;
		}

		public void Update(int ms, bool increase = false)
		{
			Update((double)ms, increase);
		}

		public void Update(double ms, bool increase = false)
		{
			var frameAmount = ms * Amount;
			if (increase)
				Current += frameAmount;
			else
				Current -= frameAmount;

			if (Current < Min)
				Current = Min;
			else if (Current > Max)
				Current = Max;
		}
	}
}