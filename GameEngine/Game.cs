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
				.SetActions(new DefaultActions(resolution).Actions);
		}


		/*private static void ConfigureDefaults(Func<int, IStage> getStage)
		{
			Resolution.ConfigureInstance(Resolution.PixelSize._FHD, Resolution.OrientationTypes.Landscape);
			Layers.ConfigureInstance();
			DefaultButtons.ConfigureInstance();

			Input.Mouse.Actions.ConfigureInstance(
				DefaultButtons.Instance,
				Layers.Instance.GetLayer(Layers.GenericLayerIds.Controls)
			);
			Input.Touch.Actions.ConfigureInstance(
				DefaultButtons.Instance,
				Layers.Instance.GetLayer(Layers.GenericLayerIds.Controls)
			);

			var actionRaiser = new Input.Combined.Actions(
					NonNullList.Of(
						Input.Keyboard.Actions.Instance,
						Input.Mouse.Actions.Instance,
						Input.Touch.Actions.Instance
					)
				);
			ConfigureInstance(getStage, actionRaiser, DefaultButtons.Instance);
		}*/
	}

}