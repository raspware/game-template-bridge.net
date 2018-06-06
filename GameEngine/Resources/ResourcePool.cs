using System;
using System.Collections.Generic;
using Bridge.Html5;

namespace Raspware.GameEngine.Resources
{
	public class ResourcePool
	{
		public readonly Dictionary<string, HTMLAudioElement> Audio = new Dictionary<string, HTMLAudioElement>();
		public readonly Dictionary<string, HTMLImageElement> Images = new Dictionary<string, HTMLImageElement>();

		public ResourcePool(JSONResources jsonResources)
		{
			if (jsonResources == null)
				throw new ArgumentNullException(nameof(jsonResources));

			jsonResources.Audio.ForEach(_ => Audio.Add(_.Title, new HTMLAudioElement() { Src = _.Src }));
			jsonResources.Image.ForEach(_ => Images.Add(_.Title, new HTMLImageElement() { Src = _.Src }));
		}
	}
}