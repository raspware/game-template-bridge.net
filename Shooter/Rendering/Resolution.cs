using System;

namespace Raspware.Shooter.Rendering
{
	public sealed class Resolution
	{
		public static Resolution Instance { get;} = new Resolution();
		private Resolution() {
			Width = 1800;
			Height = 1080;
			Amount = Height * 0.01;
		}
		public int Width { get; }
		public int Height { get; }
		public double Amount { get; }

		public int RenderAmount(int multiply)
		{
			return RenderAmount((double)multiply);
		}

		public int RenderAmount(double multiply)
		{
			return Convert.ToInt32(Math.Floor(multiply * Amount));
		}
	}
}