using System;
using ProductiveRage.Immutable;
using Raspware.ExampleGame.Stage.RayCasterAdditional;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;

namespace Raspware.ExampleGame.Stage
{
	public sealed class RayCaster : IStage
	{
		private string _message;
		private bool _renderedControls;
		public static readonly float Circle = (float)(Math.PI * 2);
		private Player _player;
		private Map _map;
		private ICore _core { get; }

		public int Id => Stage.Id.RayCaster;

		public RayCaster(ICore core)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			_core = core;
			_core.Layers.Reset(NonNullList.Of(0));
			_core.ActivateActions();

			
			_player = new Player(15.3, -1.2, Math.PI * 0.3);
			_map = new Map(32);

			/*var display = Document.getElementById('display');
			var controls = new Controls();
			var camera = new Camera(display, MOBILE ? 160 : 320, 0.8);
			var loop = new GameLoop();

			map.randomize();*/

			// taken from https://github.com/hunterloftis/playfuljs-demos/blob/gh-pages/raycaster/index.html
		}

		public void Draw()
		{
			if (_message == "")
				return;

			var levelContext = _core.Layers.GetStageLayer(0).GetContext();
			var resolution = _core.Resolution;
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

			/*map.update(seconds);
			player.update(controls.states, map, seconds);
			camera.render(player, map);*/

			var up = _core.ActionEvents[DefaultActions.Up];
			var down = _core.ActionEvents[DefaultActions.Down];
			var left = _core.ActionEvents[DefaultActions.Left];
			var right = _core.ActionEvents[DefaultActions.Right];
			var button1 = _core.ActionEvents[DefaultActions.Button1];
			var menu = _core.ActionEvents[DefaultActions.Menu];

			if (left.PressedDown())
				_player.Rotate(-(float)Math.PI * ms);

			if (right.PressedDown())
				_player.Rotate((float)Math.PI * ms);

			if (up.PressedDown())
				_player.Walk(3 * ms, _map);

			if (down.PressedDown())
				_player.Walk(-3 * ms, _map);


			data.TimePassed += ms;
			_message = data.TimePassed.ToString();

			return Id;
		}
	}
}