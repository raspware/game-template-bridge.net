using System;

namespace Raspware.GameEngine.ResourceShared
{
	public sealed class Item
	{
		public Item(string title, string src)
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