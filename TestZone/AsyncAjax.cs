using System;
using System.Threading.Tasks;
using Bridge;
using Bridge.Html5;

namespace Raspware.TestZone
{
	public sealed class R2
	{
		public readonly string Src;
		public readonly string Title;
	}

	public sealed class R
	{
		public readonly R2[] Images;
		public readonly R2[] Audio;
	}

	public class AsyncAjax : IPromise
	{
		public async static void Main2()
		{
			try
			{
				var response = await Get("/scripts/resources.json");
				var j = JSON.Parse(response);

				var strongJ = j.As<R>();

				foreach (var a in strongJ.Audio)
					Console.WriteLine(a.Title);

				foreach (var i in strongJ.Images)
					Console.WriteLine(i.Title);

			}
			catch (ErrorException e)
			{
				Console.WriteLine("Error: " + e.Error.Message);
			}
		}

		public static async Task<string> Get(string url)
		{
			var task = Task.FromPromise<string>(
				new AsyncAjax(url),
				(Func<XMLHttpRequest, string>)((request) => request.ResponseText)
			);

			await task;

			return task.Result;
		}

		private string url;

		public AsyncAjax(string url)
		{
			this.url = url;
		}

		public void Then(Delegate fulfilledHandler, Delegate errorHandler, Delegate progressHandler = null)
		{
			var request = new XMLHttpRequest();
			request.OnReadyStateChange = () =>
			{
				if (request.ReadyState != AjaxReadyState.Done)
				{
					return;
				}

				if ((request.Status == 200) || (request.Status == 304))
				{
					fulfilledHandler.Call(null, request);
				}
				else
				{
					errorHandler.Call(null, request.ResponseText);
				}
			};
			request.Open("GET", this.url);
			request.Send();
		}
	}
}