using System;
using Bridge.Html5;

namespace Raspware.GameEngine.Input
{
	public static class EventsHelper
	{
		public static bool CurrentlyFullscreen()
		{
			/*@
			if (!document.isFullScreen && !document.fullscreenElement && !document.webkitFullscreenElement && !document.mozFullScreenElement && !document.msFullscreenElement)
				return false;
			*/

			return true;
		}
		public static void ExitFullscreen()
		{
			if (!CurrentlyFullscreen())
				return;

			/*@
			 if(document.exitFullscreen) {
				document.exitFullscreen();
			  } else if(document.mozCancelFullScreen) {
				document.mozCancelFullScreen();
			  } else if(document.webkitExitFullscreen) {
				document.webkitExitFullscreen();
			  }
			 */
		}

		public static void ApplyFullscreen(HTMLDivElement wrapper)
		{
			if (wrapper == null)
				throw new ArgumentNullException(nameof(wrapper));

			if (CurrentlyFullscreen())
				return;

			/*@
				var element = wrapper;
				if(element.requestFullscreen)
					element.requestFullscreen();
				else if(element.mozRequestFullScreen)
					element.mozRequestFullScreen();
				else if(element.webkitRequestFullscreen)
					element.webkitRequestFullscreen();
				else if(element.msRequestFullscreen)
					element.msRequestFullscreen();
			*/
		}
	}
}