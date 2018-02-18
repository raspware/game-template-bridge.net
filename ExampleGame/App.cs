using ProductiveRage.Immutable;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame
{
	public class App
	{
		public static void Main()
		{
			Resolution.ConfigureInstance(Resolution.PixelSize._FHD);
			var resolution = Resolution.Instance;

			GameEngine.Input.Touch.Actions.ConfigureInstance(resolution);
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
				)
			);
		}
	}
}