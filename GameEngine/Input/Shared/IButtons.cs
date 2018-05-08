using System;
using System.Linq;
using ProductiveRage.Immutable;

namespace Raspware.GameEngine.Input.Shared
{
	public interface IButtons
	{
		NonNullList<Button> Buttons { get; }
	}

	public static class ButtonHelper
	{
		public static Button GetButton(this NonNullList<Button> buttons, int buttonId)
		{
			var button = buttons.Where(_ => _.Id == buttonId).FirstOrDefault();
			if (button == null)
				throw new ArgumentNullException(nameof(button));
			return button;
		}
	}
}