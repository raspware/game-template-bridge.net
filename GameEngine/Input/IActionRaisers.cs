using System.Collections.Generic;

namespace Raspware.GameEngine.Input
{
	public interface IActionsRaisers
	{
		Dictionary<int, IEvents> Events { get; }

	}
}
