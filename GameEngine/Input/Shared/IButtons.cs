using ProductiveRage.Immutable;

namespace Raspware.GameEngine.Input.Shared
{
	public interface IButtons
	{
		NonNullList<Button> Buttons { get; }
	}
}