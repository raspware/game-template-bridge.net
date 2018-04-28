using System;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine
{
	public sealed class Game
	{
		private static IStage _stage;
		private int _lastFrame;
		private Func<int, IStage> _getStage { get; }

		public static Game Instance { get; private set; }

		public IActions ActionRaiser { get; }
		public IButtons Buttons { get; }

		private static bool _configured { get; set; } = false;

		private static bool _alreadyRan { get; set; } = false;
		public static void ConfigureInstance(Func<int, IStage> getStage, IActions actionRaiser, IButtons buttons)
		{
			if (_configured)
				throw new Exception($"'{nameof(Instance)}' has already been configured!");

			Instance = new Game(getStage, actionRaiser, buttons);
			_configured = true;
		}

		public static void ConfigureInstance(Func<int, IStage> getStage)
		{
			ConfigureDefaults(getStage);
		}

		private static void ConfigureDefaults(Func<int, IStage> getStage)
		{
			Resolution.ConfigureInstance(Resolution.PixelSize._FHD, Resolution.OrientationTypes.Landscape);
			Layers.ConfigureInstance();
			DefaultButtons.ConfigureInstance();

			Input.Mouse.Actions.ConfigureInstance(
				DefaultButtons.Instance,
				Layers.Instance.GetLayer(Layers.Id.Controls)
			);
			Input.Touch.Actions.ConfigureInstance(
				DefaultButtons.Instance,
				Layers.Instance.GetLayer(Layers.Id.Controls)
			);

			var actionRaiser = new Input.Combined.Actions(
					NonNullList.Of(
						Input.Keyboard.Actions.Instance,
						Input.Mouse.Actions.Instance,
						Input.Touch.Actions.Instance
					)
				);

			ConfigureInstance(getStage, actionRaiser, DefaultButtons.Instance);
		}


		private Game(Func<int, IStage> getStage, IActions actionRaiser, IButtons buttons)
		{
			// TODO: Change these to tell the user they need to be initalised first
			if (Resolution.Instance == null)
				throw new ArgumentNullException(nameof(Resolution.Instance));
			if (Layers.Instance == null)
				throw new ArgumentNullException(nameof(Layers.Instance));
			if (getStage == null)
				throw new ArgumentNullException(nameof(getStage));

			ActionRaiser = actionRaiser;
			Buttons = buttons;
			_getStage = getStage;
		}

		public void Run(int startingStage)
		{
			// TODO: Maybe do a warning here
			if (!_configured || _alreadyRan)
				return;

			_stage = _getStage(startingStage);
			_alreadyRan = true;
			Tick();
		}

		private void Tick()
		{
			var now = (int)Window.Performance.Now();
			var ms = now - _lastFrame;

			Layers.Instance.Resize();
			var returnedId = _stage.Update(ms);
			if (_stage.Id == returnedId)
				_stage.Draw();
			else
				_stage = _getStage(returnedId);

			_lastFrame = now;
			Window.RequestAnimationFrame(Tick);
		}
	}
}