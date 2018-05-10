using Bridge.Html5;

namespace Raspware.GameEngine.Input
{
	public interface IActionConfigurationTouchAndMouse
	{
		int Id { get; }
		Point Point { get; }
		void Render(CanvasRenderingContext2D context);
	}
}
