using System;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stages
{
	public sealed class Title : IStage
	{
		private readonly Resolution _resolution;
		private readonly Layers _layers;
		private string _message;
		private int _timePassed = 0;

		public Id Id => Id.Title;

		public Title(Resolution resolution, Layers layers)
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

			int brightness = 127;
			var levelContext = _layers.GetLayer(Layers.Id.Level).GetContext();

			levelContext.FillStyle = "rgb(" + (brightness) + "," + (brightness) + "," + (brightness * 2) + ")";
			levelContext.FillRect(0, 0, _resolution.Width, _resolution.Height); // Clear

			levelContext.FillStyle = "white";

			levelContext.Font = _resolution.RenderAmount(24).ToString() + "px Consolas, monospace";
			levelContext.FillText("Game Title!", _resolution.RenderAmount(12), _resolution.RenderAmount(50));

			levelContext.Font = _resolution.RenderAmount(6).ToString() + "px Consolas, monospace";
			levelContext.FillText(_message, _resolution.RenderAmount(4), _resolution.RenderAmount(96));
		}

		public Id Update(int ms)
		{
			_timePassed += ms;
			_message = _timePassed.ToString();

			if (_timePassed >= 3000)
				return Id.Level;

			return Id;
		}
	}
}