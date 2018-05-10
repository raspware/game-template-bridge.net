using System;
using Raspware.GameEngine;

namespace Raspware.TestZone
{
	public static class Initalise_Test
	{
		public static void Go()
		{
			Game.DefaultSettings()
				.SetStageFactory(StageFactory)
				.Run(0);
				
		}

		private static IStage StageFactory(ICore core, int stageId)
		{
			var resolution = core.Resolution;


			throw new NotImplementedException();
		}
	}
}
