namespace Raspware.GameEngine.Input.Keyboard
{
	public interface IActionConfigurationKeyboard
	{
		Events.KeyCodes KeyCode { get; }
		int Id { get; }
	}
}