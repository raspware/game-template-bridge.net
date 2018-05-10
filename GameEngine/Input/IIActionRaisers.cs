using ProductiveRage.Immutable;

namespace Raspware.GameEngine.Input
{
	public interface IActionRaisers
	{
		NonNullList<ActionConfiguration> Actions { get; }
	}
}
