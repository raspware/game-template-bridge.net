using System;
using Bridge.Html5;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stages
{
	public sealed class Opening : IStage
	{
		private readonly Resolution _resolution;
		private readonly Layers _layers;
		private readonly Data _data;
		private string _message;
		private int _timePassed = 0;
		private bool _musicPlayed = false;

		private double _alpha = 1;

		public Id Id => Id.Opening;

		public Opening(Resolution resolution, Layers layers, Data data)
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

			_data.OpeningMusic.Play();
		}

		public void Draw()
		{
			if (_message == "")
				return;

			int brightness = 127;
			var levelContext = _layers.GetLayer(Layers.Id.Level).GetContext();

			levelContext.FillStyle = "rgb(0,0,0)";
			levelContext.FillRect(0, 0, _resolution.Width, _resolution.Height); // Clear

			levelContext.FillStyle = "white";

			levelContext.Font = _resolution.RenderAmount(24).ToString() + "px Consolas, monospace";
			levelContext.FillText("[RASPWARE]", _resolution.RenderAmount(23), _resolution.RenderAmount(50));

			levelContext.Font = _resolution.RenderAmount(6).ToString() + "px Consolas, monospace";
			levelContext.FillText(_message, _resolution.RenderAmount(4), _resolution.RenderAmount(96));

			levelContext.FillStyle = $"rgba(0,0,0,{_alpha})";
			levelContext.FillRect(0, 0, _resolution.Width, _resolution.Height); // Clear
		}

		public Id Update(int ms)
		{
			_timePassed += ms;
			_message = _timePassed.ToString();

			_alpha = 1 - (_timePassed / 1000);

			if (_timePassed > 1000)
				_alpha = 0;

			_data.OpeningMusic.AddEventListener(EventType.Ended, ev => {
				_musicPlayed = true;
			});

			if (_musicPlayed)
				return Id.Title;

			return Id;
		}
	}
}