namespace Raspware.GameEngine.Input.Keyboard
{
	public interface IActionKeyboard
	{
		Events.Keys Key { get; }
		int Id { get; }
	}
}
