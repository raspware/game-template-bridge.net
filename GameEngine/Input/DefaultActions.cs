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

			var spacer = resolution.RenderAmount(2);
			var defaultRadius = resolution.RenderAmount(8);
			var actionButtonsRadius = resolution.RenderAmount(16);

			var down = new ActionConfiguration(
				Down,
				Events.KeyCodes._downArrow,
				new Point(
					spacer + (defaultRadius * 3),
					resolution.Height - defaultRadius - spacer,
					defaultRadius
				)
			);

			var up = new ActionConfiguration(
				Up,
				Events.KeyCodes._upArrow,
				new Point(
					down.Point.X,
					down.Point.Y - (defaultRadius * 4),
					defaultRadius
				)
			);

			var left = new ActionConfiguration(
				Left,
				Events.KeyCodes._leftArrow,
				new Point(
					defaultRadius + spacer,
					down.Point.Y - (defaultRadius * 2),
					defaultRadius
				)
			);

			var right = new ActionConfiguration(
				Right,
				Events.KeyCodes._rightArrow,
				new Point(
					left.Point.X + (defaultRadius * 4),
					left.Point.Y,
					defaultRadius
				)
			);

			var button1 = new ActionConfiguration(
				Button1,
				Events.KeyCodes._space,
				new Point(
						resolution.Width - actionButtonsRadius - spacer,
					resolution.Height - actionButtonsRadius - spacer,
					actionButtonsRadius
				)
			);

			var menu = new ActionConfiguration(
				Menu,
				Events.KeyCodes._m,
				new Point(
					resolution.Width - defaultRadius - spacer,
					defaultRadius + spacer,
					defaultRadius
				)
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