using System;
using ProductiveRage.Immutable;
using Raspware.ExampleGame.Stage;
using Raspware.GameEngine;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.ExampleGame
{
	public class App
	{
		public static void Main()
		{
			/** Generic Game Configuration **/
			//Game.ConfigureInstance(GetStage);

			/** Custom Game Configuration **/
			Resolution.ConfigureInstance(Resolution.PixelSize._FHD, Resolution.OrientationTypes.Landscape);
			Layers.ConfigureInstance();
			DefaultActions.ConfigureInstance();

			GameEngine.Input.Mouse.Actions.ConfigureInstance(
				DefaultActions.Instance,
				Layers.Instance.GetLayer(Layers.GenericLayerIds.Controls)
			);
			GameEngine.Input.Touch.Actions.ConfigureInstance(
				DefaultActions.Instance,
				Layers.Instance.GetLayer(Layers.GenericLayerIds.Controls)
			);

			var actionRaiser = new GameEngine.Input.Combined.Actions(
					NonNullList.Of(
						GameEngine.Input.Keyboard.Actions.Instance,
						GameEngine.Input.Mouse.Actions.Instance,
						GameEngine.Input.Touch.Actions.Instance
					)
				);

			Game.ConfigureInstance(GetStage, actionRaiser, DefaultActions.Instance);

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