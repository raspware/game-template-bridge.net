using System;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.ExampleGame.Resources;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stage
{
	public sealed class Level : IStage
	{
		private string _message;
		private bool _renderedControls;
		private bool _first;
		private HTMLImageElement _image;
		private ResourcePool _resourcePool;
		private ICore _core { get; }

		public int Id => Stage.Id.Level;

		public Level(ICore core)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			_core = core;
			_core.Layers.Reset(NonNullList.Of(0));
			_core.ActivateActions();
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
				levelContext.Font = resolution.MultiplyClamp(10) + "px Consolas, monospace";
				levelContext.FillText("Loading...", resolution.MultiplyClamp(4), resolution.MultiplyClamp(12));
				return;
			}

			_image = _resourcePool.Images[Image.Background];

			levelContext.ClearRect(0, 0, resolution.Width, resolution.Height);

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

			data.TimePassed += ms;
			_message = data.TimePassed.ToString();

			return Id;
		}
	}
}