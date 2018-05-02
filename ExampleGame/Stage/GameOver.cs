using Raspware.GameEngine;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stage
{
	public sealed class GameOver : IStage
	{
		private string _message;
		private int _timePassed = 0;
		public int Id => Stage.Id.GameOver;

		public GameOver() { }

		public void Draw()
		{
			if (_message == "")
				return;

			int brightness = 127;
			var levelContext = Layers.Instance.GetLayer(Layers.GenericLayerIds.Level).GetContext();
			var resolution = Resolution.Instance;

			levelContext.FillStyle = "rgb(" + (brightness * 2) + "," + (brightness) + "," + (brightness) + ")";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height); // Clear

			levelContext.FillStyle = "white";
			levelContext.Font = resolution.RenderAmount(10).ToString() + "px Consolas, monospace";
			levelContext.FillText("Game Over", resolution.RenderAmount(4), resolution.RenderAmount(12));

			levelContext.Font = resolution.RenderAmount(20).ToString() + "px Consolas, monospace";
			levelContext.FillText("You Scored " + Data.Instance.Score + "!", resolution.RenderAmount(4), resolution.RenderAmount(42));

			levelContext.Font = resolution.RenderAmount(6).ToString() + "px Consolas, monospace";
			levelContext.FillText(_message, resolution.RenderAmount(4), resolution.RenderAmount(96));
		}

		public int Update(int ms)
		{
			_timePassed += ms;
			_message = _timePassed.ToString();

			if (_timePassed >= 3000)
			{
				Data.Instance.Reset(); // Since as this data is no longer needed, reset it.
				return Stage.Id.Title;
			}

			return Id;
		}
	}
}