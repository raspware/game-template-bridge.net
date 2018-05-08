using System;
using System.Linq;
using ProductiveRage.Immutable;

namespace Raspware.GameEngine.Input
{
	public static class ActionsHelper
	{
		public static Action GetAction(this NonNullList<Action> actions, int actionId)
		{
			var action = actions.Where(_ => _.Id == actionId).FirstOrDefault();
			if (action == null)
				throw new ArgumentNullException(nameof(action));
			return action;
		}
	}
}