using System;
using Raspware.ExampleGame.Stage;
using Raspware.GameEngine;

namespace Raspware.ExampleGame
{
	public class App
	{
		public static void Main()
		{
			Game.DefaultSettings()
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