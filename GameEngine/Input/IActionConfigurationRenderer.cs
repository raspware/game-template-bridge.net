using Bridge.Html5;

namespace Raspware.GameEngine.Input
{
	public interface IActionConfigurationRenderer
	{
		void Render(CanvasRenderingContext2D context);
		void Activate();
		void Deactivate();
	}
}
