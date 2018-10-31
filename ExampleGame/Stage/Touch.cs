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
		private TextBox _textBox;
		public int Id => Stage.Id.Touch;
		private bool _renderedControls { get; set; }

		public Touch(ICore core)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			_core = core;
			_core.Layers.Reset(NonNullList.Of(0));
			_core.Layers.Controls.Clear();

			_textBox = new TextBox("Touch", _core.Resolution.MultiplyClamp(10), _core.Resolution);
			_textBox.UpdatePosition(_core.Resolution.MultiplyClamp(4), _core.Resolution.MultiplyClamp(4));
		}

		public void Draw()
		{
			if (!_renderedControls)
			{
				_core.RenderActions();
				_renderedControls = true;
			}

			var levelContext = _core.Layers.GetStageLayer(0).GetContext();
			var resolution = _core.Resolution;

			levelContext.FillStyle = "orange";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height);

			_textBox.Draw(levelContext);

			levelContext.FillStyle = "black";
			levelContext.Font = resolution.MultiplyClamp(10) + "px Consolas";

			if (_core.ActionEvents[DefaultActions.Menu].PressedDown())
				levelContext.FillText("MENU", resolution.MultiplyClamp(70), resolution.MultiplyClamp(24));

			if (_core.ActionEvents[DefaultActions.Up].PressedDown())
				levelContext.FillText("UP", resolution.MultiplyClamp(20), resolution.MultiplyClamp(24));

			if (_core.ActionEvents[DefaultActions.Down].PressedDown())
				levelContext.FillText("DOWN", resolution.MultiplyClamp(20), resolution.MultiplyClamp(48));

			if (_core.ActionEvents[DefaultActions.Left].PressedDown())
				levelContext.FillText("LEFT", resolution.MultiplyClamp(4), resolution.MultiplyClamp(36));

			if (_core.ActionEvents[DefaultActions.Right].PressedDown())
				levelContext.FillText("RIGHT", resolution.MultiplyClamp(30), resolution.MultiplyClamp(36));

			if (_core.ActionEvents[DefaultActions.Button1].PressedDown())
				levelContext.FillText("BUTTON1", resolution.MultiplyClamp(70), resolution.MultiplyClamp(36));
		}

		public int Update(int ms)
		{
			return Id;
		}
	}
}