using System;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.ExampleGame.Resources;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;

namespace Raspware.ExampleGame.Stage
{
	public sealed class Zoom : IStage
	{
		private string _message;
		private bool _renderedControls;
		private bool _first;
		private bool _musicPlayed;
		private HTMLImageElement _image;
		private HTMLAudioElement _audio;
		private ResourcePool _resourcePool;
		private ICore _core { get; }

		public int Id => Stage.Id.Zoom;

		public Zoom(ICore core)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			_core = core;
			_core.Layers.Reset(NonNullList.Of(0));
			_core.DeactivateActions();
			_core.ActivateActions(NonNullList.Of(
				DefaultActions.Up,
				DefaultActions.Left,
				DefaultActions.Down,
				DefaultActions.Button1
			));

			_resourcePool = new ResourcePool();
		}

		public void Draw()
		{
			if (_message == "")
				return;

			int brightness = 0;
			var levelContext = _core.Layers.GetStageLayer(0).GetContext();
			var resolution = _core.Resolution;

			if (!_resourcePool.Loaded)
			{
				levelContext.FillStyle = "rgb(" + (brightness) + "," + (brightness) + "," + (brightness) + ")";
				levelContext.FillRect(0, 0, resolution.Width, resolution.Height); // Clear
				levelContext.FillStyle = "white";
				levelContext.Font = resolution.RenderAmount(10).ToString() + "px Consolas, monospace";
				levelContext.FillText("Loading...", resolution.RenderAmount(4), resolution.RenderAmount(12));
				return;
			}

			var data = Data.Instance;

			_image = _resourcePool.Images[Image.Background];
			_audio = _resourcePool.Audio[Audio.Theme];

			if (!_musicPlayed)
			{
				_musicPlayed = true;
				_audio.Play();
			}

			levelContext.DrawImage(_image, 0, 0);

			levelContext.FillStyle = "white";

			levelContext.Font = resolution.RenderAmount(10).ToString() + "px Consolas, monospace";
			levelContext.FillText("Playing Game", resolution.RenderAmount(4), resolution.RenderAmount(12));

			levelContext.Font = resolution.RenderAmount(20).ToString() + "px Consolas, monospace";
			levelContext.FillText("Score: " + data.Score, resolution.RenderAmount(4), resolution.RenderAmount(42));

			levelContext.Font = resolution.RenderAmount(4).ToString() + "px Consolas, monospace";
			levelContext.FillText("Press [UP] to win :)", resolution.RenderAmount(115), resolution.RenderAmount(36.5));

			levelContext.Font = resolution.RenderAmount(20).ToString() + "px Consolas, monospace";
			levelContext.FillText("Lives: " + data.Lives, resolution.RenderAmount(4), resolution.RenderAmount(72));

			levelContext.Font = resolution.RenderAmount(4).ToString() + "px Consolas, monospace";
			levelContext.FillText("Press [DOWN] to lose :(", resolution.RenderAmount(107), resolution.RenderAmount(67));

			levelContext.Font = resolution.RenderAmount(4).ToString() + "px Consolas, monospace";
			levelContext.FillText($"'{_image.Width}x{_image.Height}'", resolution.RenderAmount(10), resolution.RenderAmount(20));

			levelContext.Font = resolution.RenderAmount(4).ToString() + "px Consolas, monospace";
			levelContext.FillText($"'{_audio.Duration}'", resolution.RenderAmount(10), resolution.RenderAmount(25));

			levelContext.Font = resolution.RenderAmount(6).ToString() + "px Consolas, monospace";
			levelContext.FillText(_message, resolution.RenderAmount(4), resolution.RenderAmount(96));

			if (!_renderedControls)
			{
				_core.RenderActions();
				_renderedControls = true;
			}
		}

		public int Update(int ms)
		{
			var data = Data.Instance;

			if (!_first)
			{
				_resourcePool.Load("resources.json");
				_first = true;
			}

			if (!_resourcePool.Loaded)
				return Id;

			var up = _core.ActionEvents[DefaultActions.Up];
			var left = _core.ActionEvents[DefaultActions.Left];
			var down = _core.ActionEvents[DefaultActions.Down];
			var button1 = _core.ActionEvents[DefaultActions.Button1];

			data.TimePassed += ms;
			_message = data.TimePassed.ToString();

			if (up.OnceOnPressDown())
				data.Score++;

			if (button1.OnceOnPressDown())
				button1.As<IEventsFullscreen>().ApplyFullscreenOnPressUp();

			if (down.OnceOnPressDown())
				Console.WriteLine(EventsHelper.CurrentlyFullscreen());

			if (left.OnceOnPressDown())
				EventsHelper.ExitFullscreen();

			return Id;
		}
	}
}