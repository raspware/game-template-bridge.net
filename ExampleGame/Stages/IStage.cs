namespace Raspware.ExampleGame.Stages
{
	public enum Id
	{
		Loading,
		Opening,
		Level,
		PauseGame,
		GameOver,
		GameComplete
	}

	public interface IStage
	{
		Id Update(int ms);
		void Draw();
		Id Id { get; }
	}
}
