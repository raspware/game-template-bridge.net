using System;
using Raspware.ExampleGame.Stage;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Rendering;
using Raspware.TestZone;

namespace Raspware.ExampleGame
{
	public class App
	{
		public static void Main()
		{
			// TODO: Break 'TestZone' into its own solution
			// TODO: Break 'Base64ResourceEncoder' into its own solution and start a serialisation component.
			// TODO: As well as creating a JSON file, create an ENUM/String Literal generator based on the file name to be used as a key
			AsyncAjax.Main2();

			return;

			var resolution = new Resolution(Resolution.PixelSize._nHD, Resolution.OrientationTypes.Landscape);
			Game.CustomSettings()
				.SetResolution(resolution)
				.SetActions(DefaultActions.GetActionConfigurations(resolution))
				.SetStageFactory(StageFactory)
				.Run(Id.Level);
		}

		public static IStage StageFactory(ICore core, int id)
		{
			switch (id)
			{
				case Id.Level:
					return new Level(core);

				default:
					throw new ArgumentException(nameof(id));
			}
		}
	}
}