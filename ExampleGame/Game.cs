using System;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.ExampleGame.Stages;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame
{
	public sealed class Game
	{
		private readonly IActions _actionRaiser;
		private readonly IButtons _buttons;
		private IStage _stage;
		private int _lastFrame;

		public Game(IActions actionRaiser, IButtons buttons)
		{
			if (actionRaiser == null)
				throw new ArgumentNullException(nameof(actionRaiser));
			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));

			// TODO: Change these to tell the user they need to be initalised first
			if (Data.Instance == null)
				throw new ArgumentNullException(nameof(Data.Instance));
			if (Resolution.Instance == null)
				throw new ArgumentNullException(nameof(Resolution.Instance));
			if (Layers.Instance == null)
				throw new ArgumentNullException(nameof(Layers.Instance));

			_actionRaiser = actionRaiser;
			_buttons = buttons;

			// TODO: Move this into its own class
			_stage = GetStage(Id.Opening);

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
				_stage = GetStage(returnedId);

			_lastFrame = now;
			Window.RequestAnimationFrame(Tick);
		}

		private IStage GetStage(Id id)
		{
			switch (id)
			{
				case Id.Opening: return new Opening();
				case Id.Title: return new Title();
				case Id.Level:
					return new Level(
						_actionRaiser,
						NonNullList.Of(
							_buttons.Up,
							_buttons.Down,
							_buttons.Left,
							_buttons.Right,
							_buttons.Cancel,
							_buttons.Button1
						)
					);
				case Id.PauseGame:
					return new PauseGame(
						_actionRaiser.Cancel,
						_buttons.Cancel
					);
				case Id.GameOver: return new GameOver();
				case Id.GameComplete: return new GameComplete();
				default:
					throw new ArgumentException(nameof(id));
			}
		}
	}
}