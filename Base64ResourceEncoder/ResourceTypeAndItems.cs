using System;
using System.Collections.Generic;
using Raspware.GameEngine.ResourceShared;

namespace Raspware.GameEngine.Base64ResourceEncoder
{
	public sealed class ResourceTypeAndItems
	{
		public ResourceTypeAndItems(string type, List<Item> items)
		{
			if (string.IsNullOrWhiteSpace(type))
				throw new ArgumentException(nameof(type));
			if (items == null)
				throw new ArgumentNullException(nameof(items));

			Type = type;
			Items = items;
		}

		public string Type { get; }
		public List<Item> Items { get; }
	}
}