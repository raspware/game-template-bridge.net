using System;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Shared
{
	public sealed class DefaultButtons : IButtons
	{
		public const int Up = 0;
		public const int Down = 1;
		public const int Left = 2;
		public const int Right = 3;
		public const int Button1 = 4;
		public const int Cancel = 5;

		private Resolution _resolution { get; }
		public NonNullList<Button> Buttons { get; }

		public DefaultButtons(Resolution resolution)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			var spacer = resolution.RenderAmount(2);
			var defaultRadius = resolution.RenderAmount(8);
			var actionButtonsRadius = resolution.RenderAmount(16);

			var down = new Button(
				spacer + (defaultRadius * 3),
				resolution.Height - defaultRadius - spacer,
				defaultRadius,
				Down
			);

			var up = new Button(
				down.X,
				down.Y - (defaultRadius * 4),
				defaultRadius,
				Up
			);

			var left = new Button(
				defaultRadius + spacer,
				down.Y - (defaultRadius * 2),
				defaultRadius,
				Left
			);

			var right = new Button(
				left.X + (defaultRadius * 4),
				left.Y,
				defaultRadius,
				Right
			);

			var button1 = new Button(
				resolution.Width - actionButtonsRadius - spacer,
				resolution.Height - actionButtonsRadius - spacer,
				actionButtonsRadius,
				Button1
			);

			var cancel = new Button(
				resolution.Width - defaultRadius - spacer,
				defaultRadius + spacer,
				defaultRadius,
				Cancel
			);

			Buttons = NonNullList.Of(
				up,
				down,
				left,
				right,
				button1,
				cancel
			);
		}
	}

	// TODO: Put this somewhere else

}