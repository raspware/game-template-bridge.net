namespace Raspware.GameEngine.Input
{
	public interface IEventsFullscreen
	{
		void ApplyFullscreenOnPressUp();
		bool CurrentlyFullscreen();

		// TODO: Create an 'exit-out-of-fullscreen' method.
	}
}