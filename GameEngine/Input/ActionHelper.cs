using System;

namespace Raspware.GameEngine.Input
{
	public static class ActionHelper
	{
		public static void RenderAction(this ICore core, int actionId)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			core.As<ICoreActionRenderers>().ActionsRenders[actionId].Render(core.Layers.Controls.GetContext());
		}
	}
}