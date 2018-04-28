using Raspware.GameEngine;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stage
{
	public sealed class GameComplete : IStage
	{
		private string _message;
		private int _timePassed = 0;
		public int Id => Stage.Id.GameComplete;

		public GameComplete() { }

		public void Draw()
		{
			if (_message == "")
				return;

			int brightness = 70;
			var levelContext = Layers.Instance.GetLayer(Layers.Id.Level).GetContext();
			var resolution = Resolution.Instance;

			levelContext.FillStyle = "rgb(" + (brightness * 2) + "," + (brightness * 2) + "," + (brightness) + ")";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height); // Clear

			levelContext.FillStyle = "white";

			levelContext.Font = resolution.RenderAmount(30).ToString() + "px Consolas, monospace";
			levelContext.FillText("Completed", resolution.RenderAmount(6), resolution.RenderAmount(40));
			levelContext.FillText("Game!", resolution.RenderAmount(40), resolution.RenderAmount(75));

			levelContext.Font = resolution.RenderAmount(6).ToString() + "px Consolas, monospace";
			levelContext.FillText(_message, resolution.RenderAmount(4), resolution.RenderAmount(96));
		}

		public int Update(int ms)
		{
			_timePassed += ms;
			_message = _timePassed.ToString();

			if (_timePassed >= 3000)
				return Stage.Id.GameOver;

			return Id;
		}
	}
}