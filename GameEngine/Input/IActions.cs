using ProductiveRage.Immutable;

namespace Raspware.GameEngine.Input
{
	public interface IActions
	{
		NonNullList<Action> Actions { get; }
	}
}
