using System;
using Raspware.GameEngine.Rendering;
using Raspware.GameEngine.Stages;

namespace Raspware.ExampleGame.Stages
{
	public sealed class GameComplete : IStage
	{
		private string _message;
		private int _timePassed = 0;
		private readonly Resolution _resolution;
		private readonly Layers _layers;

		public Id Id => Id.GameComplete;

		public GameComplete(Resolution resolution, Layers layers)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (layers == null)
				throw new ArgumentNullException(nameof(layers));

			_resolution = resolution;
			_layers = layers;
		}
		
		public void Draw()
		{
			if (_message == "")
				return;

			int brightness = 70;
			var levelContext = _layers.GetLayer(Layers.Id.Level).GetContext();

			levelContext.FillStyle = "rgb(" + (brightness * 2) + "," + (brightness * 2) + "," + (brightness) + ")";
			levelContext.FillRect(0, 0, _resolution.Width, _resolution.Height); // Clear

			levelContext.FillStyle = "white";

			levelContext.Font = _resolution.RenderAmount(30).ToString() + "px Consolas, monospace";
			levelContext.FillText("Completed", _resolution.RenderAmount(6), _resolution.RenderAmount(40));
			levelContext.FillText("Game!", _resolution.RenderAmount(40), _resolution.RenderAmount(75));

			levelContext.Font = _resolution.RenderAmount(6).ToString() + "px Consolas, monospace";
			levelContext.FillText(_message, _resolution.RenderAmount(4), _resolution.RenderAmount(96));
		}

		public Id Update(int ms)
		{
			_timePassed += ms;
			_message = _timePassed.ToString();

			if (_timePassed >= 3000)
				return Id.GameOver;

			return Id;
		}
	}
}