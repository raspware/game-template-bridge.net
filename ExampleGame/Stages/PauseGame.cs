using System;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Touch.Buttons;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stages
{
	public sealed class PauseGame : IStage
	{
		private readonly IEvents _onEscape;
		private readonly Button _escape;
		private readonly Resolution _resolution;
		private readonly Layers _layers;
		private bool _displayedScreen = false;

		public Id Id => Id.PauseGame;

		public PauseGame(Resolution resolution, Layers layers, IEvents onCancel, Button cancel)
		{
			
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (layers == null)
				throw new ArgumentNullException(nameof(layers));
			if (onCancel == null)
				throw new ArgumentNullException(nameof(onCancel));
			if (cancel == null)
				throw new ArgumentNullException(nameof(cancel));

			_resolution = resolution;
			_layers = layers;
			_onEscape = onCancel;
			_escape = cancel;
		}

		public void Draw()
		{
			if (_displayedScreen)
				return;

			var levelContext = _layers.GetLayer(Layers.Id.Level).GetContext();

			// TODO: Draw escape

			levelContext.FillStyle = "rgba(0,0,0,0.75)";
			levelContext.FillRect(0, 0, _resolution.Width, _resolution.Height); // Clear

			levelContext.FillStyle = "white";
			levelContext.Font = _resolution.RenderAmount(12).ToString() + "px Consolas, monospace";
			levelContext.FillText("Paused!", _resolution.RenderAmount(110), _resolution.RenderAmount(90));
			
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