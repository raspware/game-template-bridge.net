using System;
using ProductiveRage.Immutable;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;

namespace Raspware.ExampleGame.Stage
{
	public sealed class One : IStage
	{
		private ICore _core { get; }
		public int Id => Stage.Id.One;
		private bool _renderedControls { get; set; }

		public One(ICore core)
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

			levelContext.FillStyle = "red";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height);
			levelContext.FillStyle = "white";
			levelContext.Font = resolution.RenderAmount(10).ToString() + "px Consolas, monospace";
			levelContext.FillText("One", resolution.RenderAmount(4), resolution.RenderAmount(12));

			if (!_renderedControls)
			{
				_core.RenderActions();
				_renderedControls = true;
			}
		}

		public int Update(int ms)
		{
			if (_core.ActionEvents[DefaultActions.Up].PostPressedDown())
				return Stage.Id.Two;

			return Id;
		}
	}
}