using System;

namespace Raspware.GameEngine.Rendering
{
	public static class ResolutionExtensions
	{
		public static int Clamp(this Resolution resolution, double value)
		{
			return Convert.ToInt32(Math.Floor(value));
		}

		public static double Multiply(this Resolution resolution, double value)
		{
			return value * resolution.Amount;
		}

		public static double Multiply(this Resolution resolution, int value)
		{
			return resolution.Multiply((double)value);
		}

		public static int MultiplyClamp(this Resolution resolution, double value)
		{
			return resolution.Clamp(resolution.Multiply(value));
		}

		public static int MultiplyClamp(this Resolution resolution, int value)
		{
			return resolution.MultiplyClamp((double)value);
		}
	}
}