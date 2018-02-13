using System;
using System.Linq;
using ProductiveRage.Immutable;

namespace Raspware.Shooter.Input
{
	public sealed class CombinedEvents : IEvents
    {
        private readonly NonNullList<IEvents> _eventsReaders;
        public CombinedEvents(NonNullList<IEvents> eventsReaders)
        {
            if (eventsReaders == null)
                throw new ArgumentNullException(nameof(eventsReaders));

            _eventsReaders = eventsReaders;
        }

        public bool OnceOnPressDown() => _eventsReaders.Any(ev => ev.OnceOnPressDown());
        public bool PostPressedDown() => _eventsReaders.Any(ev => ev.PostPressedDown());
        public bool PressedDown() => _eventsReaders.Any(ev => ev.PressedDown());
    }
}
