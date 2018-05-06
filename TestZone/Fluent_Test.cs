using System;

namespace Raspware.TestZone.GameEngine
{
	public static class Game
	{
		public static IGameEngineResolution CustomSettings()
		{
			return new GameEngine();
		}

		public static IGameEngineStageFactory DefaultSettings()
		{
			return CustomSettings().Resolution(new Resolution());
		}

		private class GameEngine: IGameEngine, IGameEngineResolution, IGameEngineStageFactory, IGameEngineRun
		{
			private Resolution _resolution;
			private Func<IGameEngine, int, IStage> _stageFactory;

			public GameEngine() { }
			private GameEngine(Resolution resolution)
			{
				_resolution = resolution;
			}

			public Resolution GetResolution()
			{
				return _resolution;
			}

			public IGameEngineStageFactory Resolution(Resolution resolution)
			{
				_resolution = resolution;
				return this;
			}

			public IGameEngineRun StageFactory(Func<IGameEngine, int, IStage> stageFactory)
			{
				_stageFactory = stageFactory;
				return this;
			}

			public void Run(int startStageId)
			{
				var stage = _stageFactory(this, startStageId);
			}
		}
	}

	public interface IGameEngine
	{
		Resolution GetResolution();
	}

	public interface IGameEngineResolution
	{
		IGameEngineStageFactory Resolution(Resolution resolution);
	}

	public interface IGameEngineStageFactory
	{
		IGameEngineRun StageFactory(Func<IGameEngine, int, IStage> stageFactory);
	}

	public interface IGameEngineRun
	{
		void Run(int startStageId);
	}

	public static class Fluent_Test
	{
		public static void Go()
		{
			/*Game.Custom()
				.Resolution(new Resolution("custom"))
				.StageFactory(StageFactory)
				.Run(0);*/

			Game.DefaultSettings()
				.StageFactory(StageFactory)
				.Run(0);
		}

		private static IStage StageFactory(IGameEngine gameEngine, int stageId)
		{
			var resolution = gameEngine.GetResolution();
			return new ExampleStage(resolution);
		}
	}

	public class Resolution
	{
		private readonly string _text;
		public Resolution(string text = "default") { _text = text; }
		public void Render() { Console.WriteLine(_text); }
	}

	public interface IStage
	{
		int Update(int ms);
		void Draw();
	}

	public class ExampleStage : IStage
	{
		private readonly Resolution _resolution;
		public ExampleStage(Resolution resolution) { _resolution = resolution; }
		public int Update(int ms) { return 0; }
		public void Draw() { _resolution.Render(); }
	}
}