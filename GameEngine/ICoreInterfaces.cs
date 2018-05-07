using System;
using ProductiveRage.Immutable;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine
{
	public interface ICoreResolution
	{
		ICoreButtons Resolution(Resolution resolution);
	}

	public interface ICoreButtons
	{
		ICoreStageFactory Buttons(NonNullList<Button> Buttons);
	}

	public interface ICoreStageFactory
	{
		ICoreRun StageFactory(Func<ICore, int, IStage> stageFactory);
	}

	public interface ICoreRun
	{
		void Run(int startStageId);
	}

	public interface ICore
	{
		Resolution GetResolution();
	}
}
