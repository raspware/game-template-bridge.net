using System;
using ProductiveRage.Immutable;

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

		public static void RenderActions(this ICore core)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			foreach (var actionKey in core.ActionEvents.Keys)
				RenderAction(core, actionKey);
		}

		public static void DeactivateActions(this ICore core, NonNullList<int> actionIds = null)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			if (actionIds == null)
				actionIds = core.ActionEvents.Keys.As<NonNullList<int>>();

			foreach (var actionId in actionIds)
			{
				if (!core.As<ICoreActionRenderers>().ActionsRenders.ContainsKey(actionId))
					throw new ArgumentException($"Did not have an action with id '{actionId}'");

				core.As<ICoreActionRenderers>().ActionsRenders[actionId].Deactivate();
			}
		}

		public static void ActivateActions(this ICore core, NonNullList<int> actionIds = null)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			if (actionIds == null)
				actionIds = core.ActionEvents.Keys.As<NonNullList<int>>();

			foreach (var actionId in actionIds)
			{
				if (!core.As<ICoreActionRenderers>().ActionsRenders.ContainsKey(actionId))
					throw new ArgumentException($"Did not have an action with id '{actionId}'");

				core.As<ICoreActionRenderers>().ActionsRenders[actionId].Activate();
			}
		}
	}
}