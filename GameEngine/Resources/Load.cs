using System;
using Bridge.Html5;

namespace Raspware.GameEngine.Resources
{
	public static class ResourcesOther
	{
		public static bool Loaded { private set; get; }

		public static ResourcePool Pool { private set; get; }

		public static void Load(string url)
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
					var j = JSON.Parse(request.Response.ToString()).As<JSONResources>();
					Pool = new ResourcePool(j);
					Loaded = true;
				}
			};
			request.Open("GET", url);
			request.Send();
		}
	}
}