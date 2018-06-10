using System;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.ExampleGame.Resources;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame.Stage
{
	public sealed class Zoom : IStage
	{
		private string _message;
		private bool _renderedControls;
		private bool _first;
		private HTMLImageElement _image;
		private Camera _camera;
		private ResourcePool _resourcePool;
		private ICore _core { get; }

		public int Id => Stage.Id.Level;

		public Zoom(ICore core)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			_core = core;
			_core.Layers.Reset(NonNullList.Of(0));
			_core.ActivateActions();
			_resourcePool = new ResourcePool();
			_camera = new Camera(
				new NumberWithConstraints(0.02, 0.00001),
				4,
				new NumberWithConstraints(5, 0.01),
				new NumberWithConstraints(5, 0.01)
			);
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

			_image = _resourcePool.Images[Image.Background];

			levelContext.ClearRect(0, 0, resolution.Width, resolution.Height);
			levelContext.DrawImage(
				_image,
				(int)(_camera.X * _camera.ZoomAmount),
				(int)(_camera.Y * _camera.ZoomAmount),
				(int)(_image.Width * _camera.ZoomAmount),
				(int)(_image.Height * _camera.ZoomAmount)
			);

			levelContext.FillStyle = "white";
			levelContext.Font = resolution.RenderAmount(10).ToString() + "px Consolas, monospace";
			levelContext.FillText("zoom: " + _camera.Zoom.Current.ToString(), resolution.RenderAmount(4), resolution.RenderAmount(12));
			levelContext.FillText("x: " + _camera.MoveX.Current.ToString(), resolution.RenderAmount(4), resolution.RenderAmount(18));
			levelContext.FillText("y: " + _camera.MoveY.Current.ToString(), resolution.RenderAmount(4), resolution.RenderAmount(24));

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
			var down = _core.ActionEvents[DefaultActions.Down];
			var left = _core.ActionEvents[DefaultActions.Left];
			var right = _core.ActionEvents[DefaultActions.Right];
			var button1 = _core.ActionEvents[DefaultActions.Button1];
			var menu = _core.ActionEvents[DefaultActions.Menu];

			if (up.PressedDown() || down.PressedDown())
				_camera.MoveY.Update(ms, true);
			else
				_camera.MoveY.Update(ms, false, true);

			if (left.PressedDown() || right.PressedDown())
				_camera.MoveX.Update(ms, true);
			else
				_camera.MoveX.Update(ms, false, true);

			if (button1.PressedDown() || menu.PressedDown())
				_camera.Zoom.Update(ms, true);
			else
				_camera.Zoom.Update(ms, false, true);


			if (down.PressedDown())
			{
				_camera.UpdateYDirection(true);
			}

			if (up.PressedDown())
			{
				_camera.UpdateYDirection();
			}

			if (left.PressedDown())
			{
				_camera.UpdateXDirection(true);
			}

			if (right.PressedDown())
			{
				_camera.UpdateXDirection();
			}

			if (button1.PressedDown())
			{
				_camera.UpdateZoom(true);
			}

			if (menu.PressedDown())
			{
				_camera.UpdateZoom();
			}

			_camera.Update();

			data.TimePassed += ms;
			_message = data.TimePassed.ToString();

			return Id;
		}
	}
}