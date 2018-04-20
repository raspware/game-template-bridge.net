using System.Threading.Tasks;
using Bridge.Html5;

namespace Raspware.TestZone
{
	public static class Audio
	{
		private static HTMLAudioElement ThemeMusic;

		public static void Run()
		{
			var heading = new HTMLHeadingElement()
			{
				TextContent = "RASPWARE!",
				Id = "title"

			};

			LoadAudio();
			Document.Body.AppendChild(heading);
			ThemeMusic.Play();

			ThemeMusic.AddEventListener(EventType.Ended, ev =>
				{
					Document.GetElementById("title").Remove();
				}
			);
		}

		private static Task<HTMLAudioElement> LoadAudio()
		{
			var promise = new TaskCompletionSource<HTMLAudioElement>();
			var audio = new HTMLAudioElement()
			{
				Src = "Theme.ogg",
				OnLoadedData = (ev) =>
				{
					promise.SetResult(ev.CurrentTarget);
				}
			};

			ThemeMusic = audio; // will get raised, when the work is done
			return promise.Task;
		}
	}
}