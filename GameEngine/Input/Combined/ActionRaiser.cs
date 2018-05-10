using System;
using ProductiveRage.Immutable;

namespace Raspware.GameEngine.Input.Combined
{

	// TODO: Sort this out
	public sealed class ActionRaisers
	{
		private readonly NonNullList<IActionRaisers> _actionsRaisers;
		public ActionRaisers(NonNullList<IActionRaisers> actionsRaisers)
		{
			if (actionsRaisers == null)
				throw new ArgumentNullException(nameof(actionsRaisers));

			_actionsRaisers = actionsRaisers;
		}

		/*public IEvents Up => new Events(_actionsRaisers.Map(_ => _.Up));
        public IEvents Down => new Events(_actionsRaisers.Map(_ => _.Down));
        public IEvents Left => new Events(_actionsRaisers.Map(_ => _.Left));
        public IEvents Right => new Events(_actionsRaisers.Map(_ => _.Right));
        public IEvents Cancel => new Events(_actionsRaisers.Map(_ => _.Cancel));
		public IEvents Button1 => new Events(_actionsRaisers.Map(_ => _.Button1));*/
	}
}
