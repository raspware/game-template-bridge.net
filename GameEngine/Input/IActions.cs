namespace Raspware.GameEngine.Input
{
	public interface IActions
    {
        IEvents Up { get; }
        IEvents Down { get; }
        IEvents Left { get; }
        IEvents Right { get; }
        IEvents Cancel { get; }
		IEvents Button1 { get; }
	}
}