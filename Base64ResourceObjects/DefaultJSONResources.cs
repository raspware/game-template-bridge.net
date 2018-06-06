using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raspware.GameEngine.Base64ResourceObjects
{
	public sealed class DefaultJSONResources
	{
		public readonly JSONItemObject[] Image;
		public readonly JSONItemObject[] Audio;

		public static Dictionary<string, string> ConvertToDictionary(JSONItemObject[] objects)
		{
			return objects.ToDictionary(
				o => o.Title,
				o => o.Src
			);
		}
	}
}
