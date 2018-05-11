using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Keyboard;
using Raspware.GameEngine.Input.Mouse;
using Raspware.GameEngine.Input.Touch;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine
{
	public static partial class Game
	{
		private sealed class Core : ICore, ICoreResolution, ICoreActions, ICoreStageFactory, ICoreRun, ICoreActionRenderers
		{
			private IStage _stage { get; set; }
			private int _lastFrame { get; set; }

			private Func<ICore, int, IStage> _stageFactory { get; set; }

			public ICoreActions SetResolution(Resolution resolution)
			{
				if (resolution == null)
					throw new ArgumentNullException(nameof(resolution));

				Resolution = resolution;

				InitaliseLayers();

				return this;
			}

			private void InitaliseLayers()
			{
				if (Resolution == null)
					throw new ArgumentNullException(nameof(Resolution));

				Layers = new Layers(Resolution);
			}

			public void Run(int startStageId)
			{
				_stage = _stageFactory(this, startStageId);
				Tick();
			}

			public ICoreRun SetStageFactory(Func<ICore, int, IStage> stageFactory)
			{
				if (stageFactory == null)
					throw new ArgumentNullException(nameof(stageFactory));

				_stageFactory = stageFactory;
				return this;
			}
			private void Tick()
			{
				var now = (int)Window.Performance.Now();
				var ms = now - _lastFrame;

				Layers.Resize();
				var returnedId = _stage.Update(ms);
				if (_stage.Id == returnedId)
					_stage.Draw();
				else
					_stage = _stageFactory(this, returnedId);

				_lastFrame = now;
				Window.RequestAnimationFrame(Tick);
			}

			public ICoreStageFactory SetActions(NonNullList<ActionConfiguration> actions)
			{
				if (actions == null)
					throw new ArgumentNullException(nameof(actions));

				ActionEvents = new Input.Combined.ActionsRaisers(
					actions.ToNonNullList<IActionConfiguration>(),
					NonNullList.Of<IActionsRaisers>(
						new Input.Keyboard.ActionsRaisers(Layers.Controls.CanvasElement, actions.ToNonNullList<IActionConfigurationKeyboard>()),
						new Input.Mouse.ActionsRaisers(Resolution, Layers, actions.ToNonNullList<IActionConfigurationMouse>()),
						new Input.Touch.ActionsRaisers(Resolution, Layers, actions.ToNonNullList<IActionConfigurationTouch>())
					)
				).Events;

				ActionsRenders = actions.ToDictionary(
					actionConfiguration => actionConfiguration.Id,
					actionConfiguration => actionConfiguration.As<IActionConfigurationRenderer>()
				);

				return this;
			}

			public Dictionary<int, IEvents> ActionEvents { get; private set; }
			public Dictionary<int, IActionConfigurationRenderer> ActionsRenders { get; private set; }
			public Resolution Resolution { get; private set; }
			public Layers Layers { get; private set; }
		}
	}
}