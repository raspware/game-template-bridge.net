using System;
using System.Collections.Generic;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine
{
	public interface ICoreResolution
	{
		ICoreActions SetResolution(Resolution resolution);
	}

	public interface ICoreActions
	{
		ICoreStageFactory SetActions(NonNullList<ActionConfiguration> actions);
	}

	public interface ICoreStageFactory
	{
		ICoreRun SetStageFactory(Func<ICore, int, IStage> stageFactory);
	}

	public interface ICoreRun
	{
		void Run(int startStageId);
	}

	public interface ICore
	{
		Dictionary<int, IEvents> ActionsEvents { get; }
		NonNullList<ActionConfiguration> ActionConfigurations { get; }
		Resolution Resolution { get; }
		Layers Layers { get;  }
	}
}
