using System;
using System.Collections.Generic;

namespace Raspware.GameEngine.Base64ResourceObjects
{
	public sealed class Item
	{
		public Item(string type, Dictionary<string, string> dictionary)
		{
			if (string.IsNullOrWhiteSpace(type))
				throw new ArgumentException(nameof(type));
			if (dictionary == null)
				throw new ArgumentNullException(nameof(dictionary));

			Type = type;
			Dictionary = dictionary;
		}
		
		public string Type { get; }
		public Dictionary<string, string> Dictionary { get; }
	}
}
