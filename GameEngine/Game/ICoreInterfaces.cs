using System;
using Raspware.GameEngine.Input;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine
{
	public interface ICoreResolution
	{
		ICoreButtons SetResolution(Resolution resolution);
	}

	public interface ICoreButtons
	{
		ICoreStageFactory SetButtons(IButtons buttons);
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
		IActions Actions { get; }
		Layers Layers { get; }
		IButtons Buttons { get; }
	}
}
