using Raspware.Shooter.Rendering;

namespace Raspware.Shooter.Input
{
	// TODO: Implement this!
	public sealed class TouchEvents : IEvents
	{
		public TouchEvents(Resolution resolution) { }
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
