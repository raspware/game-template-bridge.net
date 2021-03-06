﻿using System;
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
			var resolution = new Resolution(Resolution.PixelSize._FHD, Resolution.OrientationTypes.Landscape);
			Game.CustomSettings()
				.SetResolution(resolution)
				.SetActions(DefaultActions.GetActionConfigurations(resolution))
				.SetStageFactory(StageFactory)
				.Run(Id.Touch);
		}

		public static IStage StageFactory(ICore core, int id)
		{
			switch (id)
			{
				case Id.Level:
					return new Level(core);
				case Id.Zoom:
					return new Zoom(core);
				case Id.RayCaster:
					return new RayCaster(core);
				case Id.One:
					return new One(core);
				case Id.Two:
					return new Two(core);
				case Id.Touch:
					return new Touch(core);

				default:
					throw new ArgumentException(nameof(id));
			}
		}
	}
}