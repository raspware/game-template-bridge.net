using System;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stage
{
	public sealed class PauseGame : IStage
	{
		private readonly IEvents _onEscape;
		private readonly Button _escape;
		private bool _displayedScreen = false;

		public int Id => Stage.Id.PauseGame;

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

			levelContext.FillStyle = "rgba(0,0,0,0.75)";
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height); // Clear

			levelContext.FillStyle = "white";
			levelContext.Font = resolution.RenderAmount(12).ToString() + "px Consolas, monospace";
			levelContext.FillText("Paused!", resolution.RenderAmount(110), resolution.RenderAmount(90));

			var controlLayer = Layers.Instance.GetLayer(Layers.Id.Controls);
			controlLayer.Clear();
			_escape.Render(controlLayer.GetContext());
			
			_displayedScreen = true;
		}

		public int Update(int ms)
		{
			if (_onEscape.OnceOnPressDown())
				return Stage.Id.Level;

			return Id;
		}
	}
}