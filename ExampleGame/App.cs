using System;
using Raspware.ExampleGame.Stage;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame
{
	public class App
	{
		public static void Main()
		{
			var resolution = new Resolution(Resolution.PixelSize._nHD, Resolution.OrientationTypes.Landscape);
			Game.CustomSettings()
				.SetResolution(resolution)
				.SetActions(DefaultActions.GetActionConfigurations(resolution))
				.SetStageFactory(StageFactory)
				.Run(Id.Zoom);
		}

		public static IStage StageFactory(ICore core, int id)
		{
			switch (id)
			{
				case Id.Level:
					return new Level(core);
				case Id.Zoom:
					return new Zoom(core);

				default:
					throw new ArgumentException(nameof(id));
			}
		}
	}
}