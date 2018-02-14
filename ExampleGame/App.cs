using ProductiveRage.Immutable;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame
{
	public class App
	{
		public static void Main()
		{
			var resolution = Resolution.Instance;
			TouchActions.ConfigureInstance(resolution);
			Layers.ConfigureInstance(resolution);

			new Game(
				Data.Instance,
				Layers.Instance,
				resolution,
				new CombinedActions(NonNullList.Of(KeyboardActions.Instance, TouchActions.Instance))
			);
		}
	}
}