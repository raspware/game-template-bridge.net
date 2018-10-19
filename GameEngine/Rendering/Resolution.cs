using System;

namespace Raspware.GameEngine.Rendering
{
	public sealed class Resolution
	{
		public enum PixelSize
		{
			/// <summary>(80x45 or 45x45)</summary>
			_xxsmall = 1,

			/// <summary>(160x90 or 90x90)</summary>
			_xsmall,

			/// <summary>(320x180 or 180x180)</summary>
			_small = 4,

			/// <summary>(640x360 or 360x360)</summary>
			_nHD = 8,

			/// <summary>(1280x720 or 720x720)</summary>
			_HD = 16,

			/// <summary>(1920x1080 or 1080x1080)</summary>
			_FHD = 24,

			/// <summary>(2560x1440 or 1440x1440)</summary>
			_QHD = 32,

			/// <summary>(3200x1800 or 1800x1800)</summary>
			_QHDplus = 40,

			/// <summary>(3820x2160 or 2160x2160)</summary>
			_4K_UHD = 48
		}

		public enum OrientationTypes
		{
			Landscape,
			Portrait,
			Square
		}

		private static bool _configured { get; set; } = false;

		public Resolution(PixelSize size, OrientationTypes orientation)
		{
			Orientation = orientation;

			switch (orientation)
			{
				case OrientationTypes.Landscape:
					Height = 45 * (int)size;
					Amount = Height * 0.01;
					Width = 80 * (int)size;
					break;
				case OrientationTypes.Portrait:
					Height = 80 * (int)size;
					Width = 45 * (int)size;
					Amount = Width * 0.01;
					break;
				case OrientationTypes.Square:
					Height = 45 * (int)size;
					Amount = Height * 0.01;
					Width = Height;
					break;
				default:
					throw new ArgumentException(nameof(orientation));
			}
		}

		public int Width { get; }
		public int Height { get; }
		public double Amount { get; }
		public OrientationTypes Orientation { get; }
	}
}