namespace Raspware.GameEngine.Input
{
	public interface IEventsFullscreen
	{
		void ApplyFullscreenOnPressUp();
		void ExitFullscreen();
		bool CurrentlyFullscreen();
	}
}