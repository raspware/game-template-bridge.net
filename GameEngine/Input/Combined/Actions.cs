using System;
using ProductiveRage.Immutable;

namespace Raspware.GameEngine.Input.Combined
{
	public sealed class Actions : IActions
    {
        private readonly NonNullList<IActions> _actionsRaisers;
        public Actions(NonNullList<IActions> actionsRaisers)
        {
            if (actionsRaisers == null)
                throw new ArgumentNullException(nameof(actionsRaisers));

            _actionsRaisers = actionsRaisers;
        }

        public IEvents Up => new Events(_actionsRaisers.Map(_ => _.Up));
        public IEvents Down => new Events(_actionsRaisers.Map(_ => _.Down));
        public IEvents Left => new Events(_actionsRaisers.Map(_ => _.Left));
        public IEvents Right => new Events(_actionsRaisers.Map(_ => _.Right));
        public IEvents Escape => new Events(_actionsRaisers.Map(_ => _.Escape));
    }
}
