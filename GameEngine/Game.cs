using System;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;
using static Raspware.GameEngine.Rendering.Resolution;

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
			return CustomSettings()
				.Resolution(new Resolution(PixelSize._FHD, OrientationTypes.Landscape));
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


		private Game(Func<int, IStage> getStage, IActions actionRaiser, IButtons buttons)
		{
			// TODO: Change these to tell the user they need to be initalised first
			if (Resolution.Instance == null)
				throw new ArgumentNullException(nameof(Resolution.Instance));
			if (Layers.Instance == null)
				throw new ArgumentNullException(nameof(Layers.Instance));
			if (getStage == null)
				throw new ArgumentNullException(nameof(getStage));

			ActionRaiser = actionRaiser;
			Buttons = buttons;
			_getStage = getStage;
		}

	}

}