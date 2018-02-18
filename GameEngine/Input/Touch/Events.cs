using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
{
	// TODO: Implement this!
	public sealed class Events : IEvents
	{
		public Events(Resolution resolution) { }
		public bool OnceOnPressDown()
		{
			return false;
		}

		public bool PostPressedDown()
		{
			return false;
		}

		public bool PressedDown()
		{
			return false;
		}
	}
}
