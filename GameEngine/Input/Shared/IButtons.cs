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
			return buttons.Where(button => button.Id == buttonId).FirstOrDefault();
		}
	}
}