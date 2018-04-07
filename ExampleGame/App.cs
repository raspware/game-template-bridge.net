using ProductiveRage.Immutable;
using Raspware.GameEngine.Input.Touch.Buttons;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame
{
	public class App
	{
		public static void Main()
		{
			Resolution.ConfigureInstance(Resolution.PixelSize._FHD, Resolution.OrientationTypes.Landscape);
			var resolution = Resolution.Instance;

			// Resolution Defaults
			DefaultButtons.ConfigureInstance(resolution);
			var buttons = DefaultButtons.Instance;

			GameEngine.Input.Touch.Actions.ConfigureInstance(resolution, buttons);

			Layers.ConfigureInstance(resolution);
			new Game(
				Data.Instance,
				Layers.Instance,
				resolution,
				new GameEngine.Input.Combined.Actions(
					NonNullList.Of(
						GameEngine.Input.Keyboard.Actions.Instance,
						GameEngine.Input.Touch.Actions.Instance
					)
				),
				buttons
			);
		}
	}
}