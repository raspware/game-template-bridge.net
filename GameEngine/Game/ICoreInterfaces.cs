using System;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine
{
	public interface ICoreResolution
	{
		ICoreActions SetResolution(Resolution resolution);
	}

	public interface ICoreActions
	{
		ICoreStageFactory SetActions(NonNullList<Input.Shared.Action> actions);
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
		Resolution Resolution { get; }
		Layers Layers { get; }
		NonNullList<Input.Shared.Action> Actions { get; }
	}
}
