using System;
using ProductiveRage.Immutable;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stage
{
	public sealed class Touch : IStage
	{
		private ICore _core { get; }
		public int Id => Stage.Id.Touch;
		private bool _renderedControls { get; set; }

		public Touch(ICore core)
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

			levelContext.FillStyle = "orange";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height);
			levelContext.FillStyle = "white";
			levelContext.Font = resolution.MultiplyClamp(10).ToString() + "px Consolas, monospace";
			levelContext.FillText("Touch", resolution.MultiplyClamp(4), resolution.MultiplyClamp(12));

			if (!_renderedControls)
			{
				_core.RenderActions();
				_renderedControls = true;
			}

			if (_core.ActionEvents[DefaultActions.Right].PressedDown())
				levelContext.FillText("RIGHT", resolution.MultiplyClamp(4), resolution.MultiplyClamp(24));

			if (_core.ActionEvents[DefaultActions.Button1].PressedDown())
				levelContext.FillText("BUTTON1", resolution.MultiplyClamp(4), resolution.MultiplyClamp(36));
		}

		public int Update(int ms)
		{
			return Id;
		}
	}
}