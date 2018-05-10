using System;
using ProductiveRage.Immutable;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;

namespace Raspware.ExampleGame.Stage
{
	public sealed class Level : IStage
	{

		private string _message;
		public int Id => Stage.Id.Level;

		private ICore _core { get; }

		public Level(ICore core)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			_core = core;
			_core.Layers.Reset(NonNullList.Of(0));
		}

		public void Draw()
		{
			if (_message == "")
				return;

			var data = Data.Instance;
			int brightness = 70;
			var levelContext = _core.Layers.GetStageLayer(0).GetContext();
			var resolution = _core.Resolution;

			levelContext.FillStyle = "rgb(" + (brightness) + "," + (brightness * 2) + "," + (brightness) + ")";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height); // Clear

			levelContext.FillStyle = "white";

			levelContext.Font = resolution.RenderAmount(10).ToString() + "px Consolas, monospace";
			levelContext.FillText("Playing Game", resolution.RenderAmount(4), resolution.RenderAmount(12));

			levelContext.Font = resolution.RenderAmount(20).ToString() + "px Consolas, monospace";
			levelContext.FillText("Score: " + data.Score, resolution.RenderAmount(4), resolution.RenderAmount(42));

			levelContext.Font = resolution.RenderAmount(4).ToString() + "px Consolas, monospace";
			levelContext.FillText("Press [UP] to win :)", resolution.RenderAmount(115), resolution.RenderAmount(36.5));

			levelContext.Font = resolution.RenderAmount(20).ToString() + "px Consolas, monospace";
			levelContext.FillText("Lives: " + data.Lives, resolution.RenderAmount(4), resolution.RenderAmount(72));

			levelContext.Font = resolution.RenderAmount(4).ToString() + "px Consolas, monospace";
			levelContext.FillText("Press [DOWN] to lose :(", resolution.RenderAmount(107), resolution.RenderAmount(67));

			levelContext.Font = resolution.RenderAmount(6).ToString() + "px Consolas, monospace";
			levelContext.FillText(_message, resolution.RenderAmount(4), resolution.RenderAmount(96));

			_core.RenderEvent(DefaultActions.Up, levelContext);
		}

		public int Update(int ms)
		{
			var data = Data.Instance;
			var up = _core.ActionsEvents[DefaultActions.Up];

			data.TimePassed += ms;
			_message = data.TimePassed.ToString();

			if (up.OnceOnPressDown())
				data.Score++;

			return Id;
		}
	}
}