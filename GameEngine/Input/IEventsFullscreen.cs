namespace Raspware.GameEngine.Input
{
	public interface IEventsFullscreen
	{
		void ApplyFullscreenOnPressUp();

		// TODO: Add a method for detecting if the 'wrapper' is already set as fullscreen or not.
		// This will help with not applying the fullscreen code multiple times and also aid a
		// fullscreen toggle.
		// TODO: Create an 'exit-out-of-fullscreen' method.
	}
}