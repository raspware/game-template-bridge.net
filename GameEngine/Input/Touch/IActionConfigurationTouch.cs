using Raspware.GameEngine.Shape;

namespace Raspware.GameEngine.Input.Touch
{
	public interface IActionConfigurationTouch
	{
		int Id { get; }
		Circle Circle { get; }
	}
}