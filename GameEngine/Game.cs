using Raspware.GameEngine.Input;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine
{
	public static partial class Game
	{
		public static ICoreResolution CustomSettings()
		{
			return new Core();
		}

		public static ICoreStageFactory DefaultSettings()
		{
			var resolution = new Resolution(
				Resolution.PixelSize._FHD,
				Resolution.OrientationTypes.Landscape
			);

			return CustomSettings()
				.SetResolution(resolution)
				.SetActions(DefaultActions.GetActionConfigurations(resolution));
		}
	}
}