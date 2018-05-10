using System;
using System.Linq;
using Bridge.Html5;

namespace Raspware.GameEngine.Input
{
	public static class EventHelper
	{
		public static void RenderEvent(this ICore core, int actionId, CanvasRenderingContext2D context)
		{
			if(core == null)
				throw new ArgumentNullException(nameof(core));
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			core.ActionConfigurations.Where(_ => _.Id == actionId).ToList().ForEach(_ => _.Render(context));
		}
	}
}