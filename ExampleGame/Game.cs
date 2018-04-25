using System;
using Bridge.Html5;
using ProductiveRage.Immutable;
using Raspware.ExampleGame.Stages;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Touch.Buttons;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame
{
	public sealed class Game
	{
		private readonly Resolution _resolution;
		private readonly IActions _actionRaiser;
		private readonly Layers _layers;
		private readonly IButtons _buttons;
		private int _lastFrame;
		private IStage _stage;
		private Data _data;

		public Game(Data data, Layers layers, Resolution resolution, IActions actionRaiser, IButtons buttons)
		{
			if (data == null)
				throw new ArgumentNullException(nameof(data));
			if (actionRaiser == null)
				throw new ArgumentNullException(nameof(actionRaiser));
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (layers == null)
				throw new ArgumentNullException(nameof(layers));
			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));

			_data = data;
			_actionRaiser = actionRaiser;
			_resolution = resolution;
			_layers = layers;
			_buttons = buttons;
			_stage = GetStage(Id.Opening);

			Tick();
		}
		private void Tick()
		{
			var now = (int)Window.Performance.Now();
			var ms = now - _lastFrame;

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
				case Id.Opening:
					return new Opening(_resolution, _layers, _data);
				case Id.Title:
					return new Title(_resolution, _layers);
				case Id.Level:
					return new Level(
						_resolution,
						_layers,
						_actionRaiser,
						_data,
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
					return new PauseGame(_resolution, _layers, _actionRaiser.Cancel, _buttons.Cancel);
				case Id.GameOver:
					return new GameOver(_resolution, _layers, _data);
				case Id.GameComplete:
					return new GameComplete(_resolution, _layers);
				default:
					throw new ArgumentException(nameof(id));
			}
		}
	}
}