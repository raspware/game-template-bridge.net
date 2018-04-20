using System.Threading.Tasks;
using Bridge.Html5;

namespace Raspware.ExampleGame
{
	public sealed class Data
	{
		public int Score;
		public int Lives;
		public int TimePassed;
		public HTMLImageElement Image { get; set; }
		public HTMLAudioElement OpeningMusic { get; set; }

		public static Data Instance { get; } = new Data();

		private Data()
		{
			Reset();
		}
		public void Reset()
		{
			Score = 0;
			Lives = 3;
			TimePassed = 0;
		}

		public Task<HTMLImageElement> LoadImage()
		{
			var promise = new TaskCompletionSource<HTMLImageElement>();
			var image = new HTMLImageElement();

			image.Src = "/images/test.png";
			image.AddEventListener(EventType.LoadedData, _ => promise.SetResult(image));

			Image = image; // will get raised, when the work is done

			return promise.Task;
		}

		public Task<HTMLAudioElement> LoadAudio()
		{
			var promise = new TaskCompletionSource<HTMLAudioElement>();
			var audio = new HTMLAudioElement()
			{
				Src = "audio/Theme.ogg",
				OnLoadedData = (ev) =>
				{
					promise.SetResult(ev.CurrentTarget);
				}
			};

			OpeningMusic = audio; // will get raised, when the work is done
			return promise.Task;
		}
	}
}