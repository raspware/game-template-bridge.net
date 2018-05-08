using System;
using Bridge.Html5;
using ProductiveRage.Immutable;
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
			private IButtons _buttons { get; set; }
			private Func<ICore, int, IStage> _stageFactory { get; set; }
			public IActions ActionRaiser { get; private set; }
			public Layer Controls { get; private set; }
			private Resolution _resolution { get; set; }
			public ICoreButtons Resolution(Resolution resolution)
			{
				if (resolution == null)
					throw new ArgumentNullException(nameof(resolution));

				_resolution = resolution;

				InitaliseLayers();

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
				_stage = _stageFactory(this, startStageId);
				Tick();
			}

			public ICoreRun StageFactory(Func<ICore, int, IStage> stageFactory)
			{
				if (stageFactory == null)
					throw new ArgumentNullException(nameof(stageFactory));

				_stageFactory = stageFactory;
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
					_stage = _stageFactory(this, returnedId);

				_lastFrame = now;
				Window.RequestAnimationFrame(Tick);
			}

			public ICoreStageFactory Buttons(IButtons buttons)
			{
				if (buttons == null)
					throw new ArgumentNullException(nameof(buttons));

				_buttons = buttons;

				InitaliseActions();

				return this;
			}

			private void InitaliseActions()
			{
				Input.Mouse.Actions.ConfigureInstance(
				   _buttons,
				   _layers.Controls
			   );
				Input.Touch.Actions.ConfigureInstance(
				 _buttons,
				   _layers.Controls
				);

				ActionRaiser = new Input.Combined.Actions(
						NonNullList.Of(
							Input.Keyboard.Actions.Instance,
							Input.Mouse.Actions.Instance,
							Input.Touch.Actions.Instance
						)
					);
			}
		}
	}
}