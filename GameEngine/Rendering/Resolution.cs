using System;

namespace Raspware.GameEngine.Rendering
{
	public sealed class Resolution
	{
		public enum PixelSize
		{
			/// <summary>(640x360 or 360x360)</summary>
			_nHD = 1,
			/// <summary>(1280x720 or 720x720)</summary>
			_HD,
			/// <summary>(1920x1080 or 1080x1080)</summary>
			_FHD,
			/// <summary>(2560x1440 or 1440x1440)</summary>
			_QHD,
			/// <summary>(3200x1800 or 1800x1800)</summary>
			_QHDplus,
			/// <summary>(3820x2160 or 2160x2160)</summary>
			_4K_UHD
		}

		public enum OrientationTypes
		{
			Landscape,
			Portrait,
			Square
		}

		private static bool _configured { get; set; } = false;
		public static Resolution Instance { get; private set; } = null;
		private Resolution(PixelSize size, OrientationTypes orientation)
		{
			Orientation = orientation;

			switch (orientation)
			{
				case OrientationTypes.Landscape:
					Height = 360 * (int)size;
					Amount = Height * 0.01;
					Width = 640 * (int)size;
					break;
				case OrientationTypes.Portrait:
					Height = 640 * (int)size;
					Width = 360 * (int)size;
					Amount = Width * 0.01;
					break;
				case OrientationTypes.Square:
					Height = 360 * (int)size;
					Amount = Height * 0.01;
					Width = Height;
					break;
				default:
					throw new ArgumentException(nameof(orientation));
			}
		}
		public static void ConfigureInstance(PixelSize size, OrientationTypes orientation = OrientationTypes.Landscape)
		{
			if (_configured)
				throw new Exception($"'{nameof(Instance)}' has already been configured!");

			Instance = new Resolution(size, orientation);
			_configured = true;
		}

		public int Width { get; }
		public int Height { get; }
		public double Amount { get; }
		public OrientationTypes Orientation { get; }

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