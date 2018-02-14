using System;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stages
{
	public sealed class GameOver : IStage
	{
		private string _message;
		private int _timePassed = 0;
		private readonly Resolution _resolution;
		private readonly Layers _layers;
		private Data _data;

		public Id Id => Id.GameOver;

		public GameOver(Resolution resolution, Layers layers, Data data)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (layers == null)
				throw new ArgumentNullException(nameof(layers));
			if (data == null)
				throw new ArgumentNullException(nameof(data));

			_data = data;
			_resolution = resolution;
			_layers = layers;
		}
		
		public void Draw()
		{
			if (_message == "")
				return;

			int brightness = 127;
			var levelContext = _layers.GetLayer(Layers.Id.Level).GetContext();

			levelContext.FillStyle = "rgb(" + (brightness * 2) + "," + (brightness) + "," + (brightness) + ")";
			levelContext.FillRect(0, 0, _resolution.Width, _resolution.Height); // Clear

			levelContext.FillStyle = "white";
			levelContext.Font = _resolution.RenderAmount(10).ToString() + "px Consolas, monospace";
			levelContext.FillText("Game Over", _resolution.RenderAmount(4), _resolution.RenderAmount(12));

			levelContext.Font = _resolution.RenderAmount(20).ToString() + "px Consolas, monospace";
			levelContext.FillText("You Scored " + _data.Score + "!", _resolution.RenderAmount(4), _resolution.RenderAmount(42));

			levelContext.Font = _resolution.RenderAmount(6).ToString() + "px Consolas, monospace";
			levelContext.FillText(_message, _resolution.RenderAmount(4), _resolution.RenderAmount(96));
		}

		public Id Update(int ms)
		{
			_timePassed += ms;
			_message = _timePassed.ToString();

			if (_timePassed >= 3000)
			{
				_data.Reset(); // Since as this data is no longer needed, reset it.
				return Id.Opening;
			}

			return Id;
		}
	}
}