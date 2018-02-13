using ProductiveRage.Immutable;
using Raspware.Shooter.Input;
using Raspware.Shooter.Rendering;

namespace Raspware.Shooter
{
	public static class App
	{
		private static void Main()
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