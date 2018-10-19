using System;
using ProductiveRage.Immutable;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stage
{
	public sealed class Two : IStage
	{
		private ICore _core { get; }
		public int Id => Stage.Id.Two;
		private bool _renderedControls { get; set; }
		private int _count = 0;

		public Two(ICore core)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			_core = core;
			_core.Layers.Reset(NonNullList.Of(0));
			_core.Layers.Controls.Clear();
		}

		public void Draw()
		{
			var levelContext = _core.Layers.GetStageLayer(0).GetContext();
			var resolution = _core.Resolution;

			levelContext.FillStyle = "blue";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height);
			levelContext.FillStyle = "white";
			levelContext.Font = resolution.MultiplyClamp(10) + "px Consolas, monospace";
			levelContext.FillText("Two", resolution.MultiplyClamp(4), resolution.MultiplyClamp(12));

			levelContext.FillText(_count.ToString(),resolution.MultiplyClamp(4), resolution.MultiplyClamp(20));
			
			if (!_renderedControls)
			{
				_core.Layers.Controls.Clear();
				_core.RenderActions();
				_renderedControls = true;
			}
		}

		public int Update(int ms)
		{
			if (_core.ActionEvents[DefaultActions.Menu].OnceOnPressDown())
				return Stage.Id.One;

			if (_core.ActionEvents[DefaultActions.Up].OnceOnPressDown())
				_count++;

			if (_core.ActionEvents[DefaultActions.Down].PostPressedDown())
				_count++;

			if (_core.ActionEvents[DefaultActions.Left].PressedDown())
				_count++;

			if (_core.ActionEvents[DefaultActions.Right].OnceOnPressDown())
				_core.ActionEvents[DefaultActions.Right].As<IEventsFullscreen>().ApplyFullscreenOnPressUp();

			return Id;
		}
	}
}