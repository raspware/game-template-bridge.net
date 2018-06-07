using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Html5;

namespace Raspware.GameEngine
{
	public sealed partial class ResourcePool
	{
		public bool Loaded { private set; get; }
		public readonly Dictionary<string, HTMLAudioElement> Audio = new Dictionary<string, HTMLAudioElement>();
		public readonly Dictionary<string, HTMLImageElement> Images = new Dictionary<string, HTMLImageElement>();

		public void Load(string url)
		{
			if (Loaded)
			{
				Console.WriteLine("Already loaded resources!");
				return;
			}

			var request = new XMLHttpRequest();
			request.OnReadyStateChange = () =>
			{
				if (request.ReadyState != AjaxReadyState.Done)
					return;

				if ((request.Status == 200) || (request.Status == 304))
				{
					var jsonResources = JSON.Parse(request.Response.ToString()).As<JSONResources>();
					if (jsonResources == null)
						throw new ArgumentNullException(nameof(jsonResources));

					var total = jsonResources.Audio.Count() + jsonResources.Image.Count();
					var amountLoaded = 0;

					jsonResources.Audio.ForEach(_ => Audio.Add(_.Title, new HTMLAudioElement()
					{
						Src = _.Src,
						OnLoadedData = (e) =>
						{
							amountLoaded++;
							if (amountLoaded == total)
								Loaded = true;
						}
					}));
					jsonResources.Image.ForEach(_ => Images.Add(_.Title, new HTMLImageElement()
					{
						Src = _.Src,
						OnLoad = (e) =>
						{
							amountLoaded++;
							if (amountLoaded == total)
								Loaded = true;
						}
					}));
				}
			};
			request.Open("GET", url);
			request.Send();
		}
	}
}