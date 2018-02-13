namespace Raspware.Shooter.Input
{
	public interface IActions
    {
        IEvents Up { get; }
        IEvents Down { get; }
        IEvents Left { get; }
        IEvents Right { get; }
        IEvents Escape { get; }
    }
}