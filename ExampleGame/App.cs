using System;
using ProductiveRage.Immutable;
using Raspware.ExampleGame.Stage;
using Raspware.GameEngine;

namespace Raspware.ExampleGame
{
	public class App
	{
		public static void Main()
		{
			Game.ConfigureInstance(GetStage);
			Game.Instance.Run(Id.Opening);
		}

		public static IStage GetStage(int id)
		{
			var actionRaiser = Game.Instance.ActionRaiser;
			var buttons = Game.Instance.Buttons;

			switch (id)
			{
				case Id.Opening: return new Opening();
				case Id.Title: return new Title();
				case Id.Level:
					return new Level(
						actionRaiser,
						NonNullList.Of(
							buttons.Up,
							buttons.Down,
							buttons.Left,
							buttons.Right,
							buttons.Cancel,
							buttons.Button1
						)
					);
				case Id.PauseGame:
					return new PauseGame(
						actionRaiser.Cancel,
						buttons.Cancel
					);
				case Id.GameOver: return new GameOver();
				case Id.GameComplete: return new GameComplete();
				default:
					throw new ArgumentException(nameof(id));
			}
		}
	}
}