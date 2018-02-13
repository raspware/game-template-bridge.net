using System;
using ProductiveRage.Immutable;

namespace Raspware.Shooter.Input
{
	public sealed class CombinedActions : IActions
    {
        private readonly NonNullList<IActions> _actionsRaisers;
        public CombinedActions(NonNullList<IActions> actionsRaisers)
        {
            if (actionsRaisers == null)
                throw new ArgumentNullException(nameof(actionsRaisers));

            _actionsRaisers = actionsRaisers;
        }

        public IEvents Up => new CombinedEvents(_actionsRaisers.Map(_ => _.Up));
        public IEvents Down => new CombinedEvents(_actionsRaisers.Map(_ => _.Down));
        public IEvents Left => new CombinedEvents(_actionsRaisers.Map(_ => _.Left));
        public IEvents Right => new CombinedEvents(_actionsRaisers.Map(_ => _.Right));
        public IEvents Escape => new CombinedEvents(_actionsRaisers.Map(_ => _.Escape));
    }
}
