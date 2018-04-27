using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stage
{
	public sealed class Title : IStage
	{
		private string _message;
		private int _timePassed = 0;

		public int Id => Stage.Id.Title;

		public Title() { }

		public void Draw()
		{
			if (_message == "")
				return;

			int brightness = 127;
			var levelContext = Layers.Instance.GetLayer(Layers.Id.Level).GetContext();
			var resolution = Resolution.Instance;

			levelContext.FillStyle = "rgb(" + (brightness) + "," + (brightness) + "," + (brightness * 2) + ")";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height); // Clear

			levelContext.FillStyle = "white";

			levelContext.Font = resolution.RenderAmount(24).ToString() + "px Consolas, monospace";
			levelContext.FillText("Game Title!", resolution.RenderAmount(12), resolution.RenderAmount(50));

			levelContext.Font = resolution.RenderAmount(6).ToString() + "px Consolas, monospace";
			levelContext.FillText(_message, resolution.RenderAmount(4), resolution.RenderAmount(96));
		}

		public int Update(int ms)
		{
			_timePassed += ms;
			_message = _timePassed.ToString();

			if (_timePassed >= 3000)
				return Stage.Id.Level;

			return Id;
		}
	}
}