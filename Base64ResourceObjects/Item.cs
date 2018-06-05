using System;
using System.Collections.Generic;

namespace Raspware.GameEngine.Base64ResourceObjects
{
	public sealed class Item
	{
		public Item(string type, List<JSONItemObject> objects)
		{
			if (string.IsNullOrWhiteSpace(type))
				throw new ArgumentException(nameof(type));
			if (objects == null)
				throw new ArgumentNullException(nameof(objects));

			Type = type;
			Objects = objects;
		}
		
		public string Type { get; }
		public List<JSONItemObject> Objects{ get; }
	}
}