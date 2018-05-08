using System;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Input.Keyboard;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input
{
	public sealed class DefaultActions: IActions
	{
		public const int Up = 0;
		public const int Down = 1;
		public const int Left = 2;
		public const int Right = 3;
		public const int Button1 = 4;
		public const int Cancel = 5;

		public NonNullList<Action> Actions { get; }

		public DefaultActions(Resolution resolution)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			var spacer = resolution.RenderAmount(2);
			var defaultRadius = resolution.RenderAmount(8);
			var actionButtonsRadius = resolution.RenderAmount(16);

			var down = new Action(
				Down,
				new Point(
					spacer + (defaultRadius * 3),
					resolution.Height - defaultRadius - spacer,
					defaultRadius
				),
				Events.Keys._downArrow
			);

			var up = new Action(
				Up,
				new Point(
					down.Point.X,
					down.Point.Y - (defaultRadius * 4),
					defaultRadius
				),
				Events.Keys._upArrow
			);

			var left = new Action(
				Left,
				new Point(
					defaultRadius + spacer,
					down.Point.Y - (defaultRadius * 2),
					defaultRadius
				),
				Events.Keys._leftArrow
			);

			var right = new Action(
				Right,
				new Point (
					left.Point.X + (defaultRadius * 4),
					left.Point.Y,
					defaultRadius
				),
				Events.Keys._rightArrow
			);

			var button1 = new Action(
				Button1,
				new Point (
						resolution.Width - actionButtonsRadius - spacer,
					resolution.Height - actionButtonsRadius - spacer,
					actionButtonsRadius
				),
				Events.Keys._space
			);

			var cancel = new Action(
				Cancel,
				new Point(
					resolution.Width - defaultRadius - spacer,
					defaultRadius + spacer,
					defaultRadius
				),
				Events.Keys._escape
			);

			Actions = NonNullList.Of(
				up,
				down,
				left,
				right,
				button1,
				cancel
			);
		}
	}
}