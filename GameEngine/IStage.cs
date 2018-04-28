namespace Raspware.GameEngine
{
	public interface IStage
	{
		int Update(int ms);
		void Draw();
		int Id { get; }
	}
}
