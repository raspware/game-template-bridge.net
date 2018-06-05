using System;

namespace Raspware.GameEngine.Base64ResourceObjects
{
	public sealed class JSONItemObject
	{
		public JSONItemObject(string title, string src)
		{
			if (string.IsNullOrWhiteSpace(title))
				throw new ArgumentNullException(nameof(title));
			if (string.IsNullOrWhiteSpace(src))
				throw new ArgumentNullException(nameof(src));

			Title = Title;
			Src = src;
		}

		public string Title { get; }
		public string Src { get; }
	}
}
