using System;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stages
{
	public sealed class PauseGame : IStage
	{
		private readonly IEvents _onEscape;
		private readonly Button _escape;
		private bool _displayedScreen = false;

		public Id Id => Id.PauseGame;

		public PauseGame(IEvents onCancel, Button cancel)
		{
			if (onCancel == null)
				throw new ArgumentNullException(nameof(onCancel));
			if (cancel == null)
				throw new ArgumentNullException(nameof(cancel));

			_onEscape = onCancel;
			_escape = cancel;
		}

		public void Draw()
		{
			if (_displayedScreen)
				return;

			var levelContext = Layers.Instance.GetLayer(Layers.Id.Level).GetContext();
			var resolution = Resolution.Instance;

			// TODO: Draw escape

			levelContext.FillStyle = "rgba(0,0,0,0.75)";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height); // Clear

			levelContext.FillStyle = "white";
			levelContext.Font = resolution.RenderAmount(12).ToString() + "px Consolas, monospace";
			levelContext.FillText("Paused!", resolution.RenderAmount(110), resolution.RenderAmount(90));

			// render buttons
			var controlsContext = Layers.Instance.GetLayer(Layers.Id.Controls).GetContext();
			_escape.Render(controlsContext);
			_displayedScreen = true;
		}

		public Id Update(int ms)
		{
			if (_onEscape.OnceOnPressDown())
				return Id.Level;

			return Id;
		}
	}
}