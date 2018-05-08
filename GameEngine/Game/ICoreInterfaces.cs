using System;
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
		ICoreStageFactory Buttons(IButtons buttons);
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
