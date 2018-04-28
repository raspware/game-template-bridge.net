using System;
using System.Linq;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stage
{
	public sealed class Level : IStage
	{
		private readonly IActions _actionRaiser;
		private readonly NonNullList<Button> _buttons;
		private readonly HTMLImageElement _image;

		private string _message;
		public int Id => Stage.Id.Level;

		public Level(IActions actionRaiser, NonNullList<Button> buttons)
		{
			if (actionRaiser == null)
				throw new ArgumentNullException(nameof(actionRaiser));
			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));

			_actionRaiser = actionRaiser;
			_buttons = buttons;
			_image = new HTMLImageElement() { Src = Resource.Image.Test };
		}

		public void Draw()
		{
			if (_message == "")
				return;

			var data = Data.Instance;
			int brightness = 70;
			var levelContext = Layers.Instance.GetLayer(Layers.Id.Level).GetContext();
			var resolution = Resolution.Instance;

			levelContext.FillStyle = "rgb(" + (brightness) + "," + (brightness * 2) + "," + (brightness) + ")";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height); // Clear

			levelContext.FillStyle = "white";

			levelContext.DrawImage(_image, 0, 0);

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

			// render buttons
			var controlsContext = Layers.Instance.GetLayer(Layers.Id.Controls).GetContext();
			_buttons.ToList().ForEach(_ => _.Render(controlsContext));
		}

		public int Update(int ms)
		{
			var data = Data.Instance;

			data.TimePassed += ms;
			_message = data.TimePassed.ToString();

			if (_actionRaiser.Cancel.OnceOnPressDown())
				return Stage.Id.PauseGame;

			if (_actionRaiser.Up.OnceOnPressDown())
				data.Score++;

			if (data.Score == 5)
				return Stage.Id.GameComplete;

			if (_actionRaiser.Down.OnceOnPressDown())
				data.Lives--;

			if (data.Lives == 0)
				return Stage.Id.GameOver;

			return Id;
		}
	}
}