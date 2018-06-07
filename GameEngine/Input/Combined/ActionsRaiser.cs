using System;
using System.Collections.Generic;
using System.Linq;
using ProductiveRage.Immutable;

namespace Raspware.GameEngine.Input.Combined
{
	public sealed class ActionsRaisers : IActionsRaisers
	{
		public Dictionary<int, IEvents> Events { get; private set; }

		public ActionsRaisers(NonNullList<IActionConfiguration> actionConfigurations, NonNullList<IActionsRaisers> actionsRaisers)
		{
			if (actionConfigurations == null)
				throw new ArgumentNullException(nameof(actionConfigurations));
			if (actionsRaisers == null)
				throw new ArgumentNullException(nameof(actionsRaisers));

			var actionsEvents = new Dictionary<int, IEvents>();
			foreach (var action in actionConfigurations)
				actionsEvents.Add(
					action.Id,
					new Events(
						NonNullList.Of(
							 actionsRaisers
								 .SelectMany(actionsRaiser => actionsRaiser.Events)
								 .Where(eventsDictionary => eventsDictionary.Key == action.Id)
								 .Select(eventsDictionary => eventsDictionary.Value)
								 .ToArray()
						 )
					)
				);

			Events = actionsEvents;
		}
	}
}