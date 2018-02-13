using System;
using Raspware.Shooter.Input;
using Raspware.Shooter.Rendering;

namespace Raspware.Shooter.Stages
{
	public sealed class PauseGame : IStage
	{
		private readonly IEvents _onEscape;
		private readonly Resolution _resolution;
		private readonly Layers _layers;
		private bool _displayedScreen = false;

		public Id Id => Id.PauseGame;

		public PauseGame(Resolution resolution, Layers layers, IEvents onEscape)
		{
			
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (layers == null)
				throw new ArgumentNullException(nameof(layers));
			if (onEscape == null)
				throw new ArgumentNullException(nameof(onEscape));
			
			_resolution = resolution;
			_layers = layers;
			_onEscape = onEscape;
		}

		public void Draw()
		{
			if (_displayedScreen)
				return;

			var levelContext = _layers.GetLayer(Layers.Id.Level).GetContext();

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