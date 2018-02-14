using System;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Rendering;
using Raspware.GameEngine.Stages;
namespace Raspware.ExampleGame.Stages
{
	public sealed class Level : IStage
	{
		private readonly IActions _actionRaiser;
		private readonly Resolution _resolution;
		private readonly Layers _layers;
		private string _message;
		private Data _data;

		public Id Id => Id.Level;

		public Level(Resolution resolution, Layers layers, IActions actionRaiser, Data data)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (layers == null)
				throw new ArgumentNullException(nameof(layers));
			if (actionRaiser == null)
				throw new ArgumentNullException(nameof(actionRaiser));
			if (data == null)
				throw new ArgumentNullException(nameof(data));
			
			_resolution = resolution;
			_layers = layers;
			_actionRaiser = actionRaiser;
			_data = data;
		}
		
		public void Draw()
		{
			if (_message == "")
				return;

			int brightness = 70;
			var levelContext = _layers.GetLayer(Layers.Id.Level).GetContext();

			levelContext.FillStyle = "rgb(" + (brightness) + "," + (brightness * 2) + "," + (brightness) + ")";
			levelContext.FillRect(0, 0, _resolution.Width, _resolution.Height); // Clear

			levelContext.FillStyle = "white";

			levelContext.DrawImage(_data.Image, 0, 0);

			levelContext.Font = _resolution.RenderAmount(10).ToString() + "px Consolas, monospace";
			levelContext.FillText("Playing Game", _resolution.RenderAmount(4), _resolution.RenderAmount(12));

			levelContext.Font = _resolution.RenderAmount(20).ToString() + "px Consolas, monospace";
			levelContext.FillText("Score: " + _data.Score, _resolution.RenderAmount(4), _resolution.RenderAmount(42));

			levelContext.Font = _resolution.RenderAmount(4).ToString() + "px Consolas, monospace";
			levelContext.FillText("Press [UP] to win :)", _resolution.RenderAmount(115), _resolution.RenderAmount(36.5));

			levelContext.Font = _resolution.RenderAmount(20).ToString() + "px Consolas, monospace";
			levelContext.FillText("Lives: " + _data.Lives, _resolution.RenderAmount(4), _resolution.RenderAmount(72));

			levelContext.Font = _resolution.RenderAmount(4).ToString() + "px Consolas, monospace";
			levelContext.FillText("Press [DOWN] to lose :(", _resolution.RenderAmount(107), _resolution.RenderAmount(67));

			levelContext.Font = _resolution.RenderAmount(6).ToString() + "px Consolas, monospace";
			levelContext.FillText(_message, _resolution.RenderAmount(4), _resolution.RenderAmount(96));
		}

		public Id Update(int ms)
		{
			_data.TimePassed += ms;
			_message = _data.TimePassed.ToString();

			if (_actionRaiser.Escape.OnceOnPressDown())
				return Id.PauseGame;

			if (_actionRaiser.Up.OnceOnPressDown())
				_data.Score++;

			if (_data.Score == 5)
				return Id.GameComplete;

			if (_actionRaiser.Down.OnceOnPressDown())
				_data.Lives--;

			if (_data.Lives == 0)
				return Id.GameOver;

			return Id;
		}
	}
}