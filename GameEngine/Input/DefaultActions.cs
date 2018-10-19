using System;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Input.Keyboard;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input
{
	public static class DefaultActions
	{
		public const int Up = 0;
		public const int Down = 1;
		public const int Left = 2;
		public const int Right = 3;
		public const int Button1 = 4;
		public const int Menu = 5;

		public static NonNullList<ActionConfiguration> GetActionConfigurations(Resolution resolution)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			var spacer = resolution.Multiply(2);
			var defaultRadius = resolution.Multiply(8);
			var actionButtonsRadius = resolution.Multiply(16);

			var down = new ActionConfiguration(
				Down,
				Events.KeyCodes._downArrow,
				new Point(
					resolution.Clamp(spacer + (defaultRadius * 3)),
					resolution.Clamp(resolution.Height - defaultRadius - spacer),
					defaultRadius
				),
				resolution
			);

			var up = new ActionConfiguration(
				Up,
				Events.KeyCodes._upArrow,
				new Point(
					down.Point.X,
					resolution.Clamp(down.Point.Y - (defaultRadius * 4)),
					defaultRadius
				),
				resolution
			);

			var left = new ActionConfiguration(
				Left,
				Events.KeyCodes._leftArrow,
				new Point(
					resolution.Clamp(defaultRadius + spacer),
					resolution.Clamp(down.Point.Y - (defaultRadius * 2)),
					defaultRadius
				),
				resolution
			);

			var right = new ActionConfiguration(
				Right,
				Events.KeyCodes._rightArrow,
				new Point(
					resolution.Clamp(left.Point.X + (defaultRadius * 4)),
					left.Point.Y,
					defaultRadius
				),
				resolution
			);

			var button1 = new ActionConfiguration(
				Button1,
				Events.KeyCodes._space,
				new Point(
					resolution.Clamp(resolution.Width - actionButtonsRadius - spacer),
					resolution.Clamp(resolution.Height - actionButtonsRadius - spacer),
					actionButtonsRadius
				),
				resolution
			);

			var menu = new ActionConfiguration(
				Menu,
				Events.KeyCodes._m,
				new Point(
					resolution.Clamp(resolution.Width - defaultRadius - spacer),
					resolution.Clamp(defaultRadius + spacer),
					defaultRadius
				),
				resolution
			);

			return NonNullList.Of(
				up,
				down,
				left,
				right,
				button1,
				menu
			);
		}
	}
}