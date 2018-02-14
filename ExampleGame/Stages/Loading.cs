using System;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stages
{
	public sealed class Loading : IStage
	{
		private readonly Resolution _resolution;
		private readonly Layers _layers;
		private Data _data;
		private bool _displayedScreen = false;

		public Id Id => Id.Loading;

		public Loading(Resolution resolution, Layers layers, Data data)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (layers == null)
				throw new ArgumentNullException(nameof(layers));
			if (data == null)
				throw new ArgumentNullException(nameof(data));

			_resolution = resolution;
			_layers = layers;
			_data = data;
		}

		public void Draw()
		{
			if (_displayedScreen)
				return;

			var levelContext = _layers.GetLayer(Layers.Id.Level).GetContext();

			levelContext.FillStyle = "rgb(0,0,0)";
			levelContext.FillRect(0, 0, _resolution.Width, _resolution.Height); // Clear

			levelContext.FillStyle = "white";
			levelContext.Font = _resolution.RenderAmount(12).ToString() + "px Consolas, monospace";
			levelContext.FillText("Loading...", _resolution.RenderAmount(100), _resolution.RenderAmount(90));

			_displayedScreen = true;
		}

		public Id Update(int ms)
		{
			if (_displayedScreen)
			{
				_data.LoadImage();
				return Id.Opening;
			}

			return Id;
		}
	}
}