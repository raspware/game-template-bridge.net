using System;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine;
using Raspware.GameEngine.Input;

namespace Raspware.ExampleGame.Stage
{
	public sealed class RayCaster : IStage
	{
		private string _message;
		private bool _renderedControls;
		private static readonly float _circle = (float)(Math.PI * 2);
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

		public sealed class Player
		{
			public float X { get; set; }
			public float Y { get; set; }
			public float Direction { get; set; }
			public float Paces { get; set; }
			public Bitmap Weapon { get; set; }


			public Player(float x, float y, float direction) // direction may not be correct
			{
				X = x;
				Y = y;
				Direction = direction;
				Weapon = new Bitmap("assets/knife_hand.png", 319, 320);
				Paces = 0;
			}

			public void Rotate(float angle)
			{
				Direction = (Direction + angle + _circle) % _circle;
			}

			public void Walk(float distance, Map map)
			{
				var dx = (float)Math.Cos(Direction) * distance;
				var dy = (float)Math.Sin(Direction) * distance;

				if (map.Get(X + dx, Y) <= 0)
					X += dx;
				if (map.Get(X, Y + dy) <= 0)
					Y += dy;

				Paces += distance;
			}


		}

		public sealed class Bitmap
		{
			public HTMLImageElement Image { get; set; }
			public float Width { get; set; }
			public float Height { get; set; }
			public Bitmap(string src, float width, float height)
			{
				Image = new HTMLImageElement() { Src = src };
				Width = width;
				Height = height;
			}
		}

		public sealed class Map
		{
			public float Size { get; set; }
			public Uint8Array WallGrid { get; set; }
			public Bitmap SkyBox { get; set; }
			public Bitmap WallTexture { get; set; }
			public float Light { get; set; }
			public Map(float size)
			{
				Size = size;
				WallGrid = new Uint8Array(size * size);
				SkyBox = new Bitmap("assets/deathvalley_panorama.jpg", 2000, 750);
				WallTexture = new Bitmap("assets/wall_texture.jpg", 1024, 1024);
				Light = 0;
			}
			public int Get(float x, float y)
			{
				x = (float)Math.Floor(x);
				y = (float)Math.Floor(y);

				if (x < 0 || x > Size - 1 || y < 0 || y > Size - 1)
					return -1;

				return (int)WallGrid[(y * Size + x).ToString()];
			}

			public void Randomise()
			{
				for (var i = 0; i < Size * Size; i++)
					WallGrid[i] = (byte)(Math.Random() < 0.3 ? 1 : 0);
			}

			public void Update(float seconds)
			{
				if (Light > 0)
					Light = Math.Max(Light - 10 * seconds, 0);
				else if (Math.Random() * 5 < seconds)
					Light = 2;
			}
		}


		private sealed class Cast
		{
			public Cast() {
			}
			public Cast Ray() {
				return null;
			}
			public void Step() { }
			public void Inspect() { }
		}


		
	}
}