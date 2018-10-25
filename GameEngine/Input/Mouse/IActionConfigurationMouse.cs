using Raspware.GameEngine.Shape;

namespace Raspware.GameEngine.Input.Mouse
{
	public interface IActionConfigurationMouse
	{
		int Id { get; }
		Circle Circle { get; }
	}
}