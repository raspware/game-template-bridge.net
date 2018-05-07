using System;
using Bridge.Html5;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine
{
	public static partial class Game
	{
		private sealed class Core : ICore, ICoreResolution, ICoreButtons, ICoreStageFactory, ICoreRun
		{
			private IStage _stage { get; set; }
			private int _lastFrame { get; set; }
			private Layers _layers { get; set; }
			private Func<int, IStage> _getStage { get; }
			public IActions ActionRaiser { get; }
			public IButtons Buttons { get; }
			public Layer Controls { get; private set; }
			private Resolution _resolution { get; set; }
			public ICoreStageFactory Resolution(Resolution resolution)
			{
				if (resolution == null)
					throw new ArgumentNullException(nameof(resolution));

				_resolution = resolution;

				// TODO: A
				return this;
			}

			private void InitaliseLayers()
			{
				if (_resolution == null)
					throw new ArgumentNullException(nameof(_resolution));

				if (_layers != null)
				{
					Console.WriteLine("Layers already initalised!");
					return;
				}

				_layers = new Layers(_resolution);
			}

			public void Run(int startStageId)
			{
				_stage = _getStage(startStageId);
				Tick();
			}

			public ICoreRun StageFactory(Func<ICore, int, IStage> stageFactory)
			{
				return this;
			}

			public Resolution GetResolution()
			{
				return _resolution;
			}

			private void Tick()
			{
				var now = (int)Window.Performance.Now();
				var ms = now - _lastFrame;

				_layers.Resize();
				var returnedId = _stage.Update(ms);
				if (_stage.Id == returnedId)
					_stage.Draw();
				else
					_stage = _getStage(returnedId);

				_lastFrame = now;
				Window.RequestAnimationFrame(Tick);
			}
		}
	}
}