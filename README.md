# Game Template (Bridge.NET)

This project was my attempt of learning the [Bridge.NET](http://bridge.net/) open source C#-to-JavaScript Compiler. Rather than doing the usual `hello world` application I thought I'd do something more fun!


## Prerequisites

This project was originally developed in Visual Studio 2017. Although this isn't currently available as a NuGet Package, the latest binaries are available in the `dist` folder of this repository. The version of Bridge.NET used for this project is `v17.1.0`. I have experienced compilation issues when mixing projects with different versions of Bridge.NET; so, if the binaries are used directly from this repo, I'd recommend keeping the versions of Bridge.NET the same for all projects.


## The Basic Flow of Control

The `Game` instance is in a constant loop. Per loop it will:

1. First, execute the `Update` method of the current `Stage`.

2.1. If the `Update` returns the same `Id` as the current `Stage`, the 'Draw' method of that Stage will execute.

2.2. However, if a different `Id` is returned from calling the current `Stage` `Update` method, the `StageFactory` will fetch the new `Stage` based on the returning `Id`.


## Getting Started

When the binaries are added to a Bridge.NET project you can configure and run a `Game` instance like so.

1. First create a way to identify a `Stage` instance (this must return an `int`).
```csharp
namespace Example.Stage
{
	public static class Id
	{
		public const int Example = 1;
	}
}
```

2. (Optional) Create some form of Singleton style `State`.
```csharp
namespace Example
{
	public sealed class State
	{
		public int TimePassed;
		public int CurrentMS;
		public static State Instance { get; } = new State();

		private State()
		{
			Reset();
		}
		public void Reset()
		{
			CurrentMS = 0;
			TimePassed = 0;
		}
	}
}
```


3. Create a `Stage` instance using the newly created `Id` class.
```csharp
namespace Example.Stage
{
	public sealed class Example : IStage
	{
		private readonly ICore _core;
		private bool _renderedControls;
		public int Id => Stage.Id.Example;

		public Example(ICore core)
		{
			if (core == null)
				throw new ArgumentNullException(nameof(core));

			_core = core; // shortcut to the Core
			_core.Layers.Reset(NonNullList.Of(0)); // clear any old layers
			_core.Layers.Controls.Clear(); // clear any old control layers
		}

		public void Draw()
		{
			if (!_renderedControls) // These are on a sperate layer and therefore only need to be rendered once.
			{
				_core.RenderActions();
				_renderedControls = true;
			}

			var levelContext = _core.Layers.GetStageLayer(0).GetContext();
			var resolution = _core.Resolution;

			levelContext.FillStyle = "blue"; // clear the screen
			levelContext.FillRect(0, 0, resolution.Width, resolution.Height);

			levelContext.FillStyle = "white"; // set the text properties
			levelContext.Font = resolution.MultiplyClamp(10) + "px Consolas";

			// render out information
			levelContext.FillText(State.Instance.TimePassed + " ms passed", resolution.MultiplyClamp(10), resolution.MultiplyClamp(10));
			levelContext.FillText((1000 / State.Instance.CurrentMS) + " FPS", resolution.MultiplyClamp(10), resolution.MultiplyClamp(30));
		}

		public int Update(int ms)
		{
			State.Instance.CurrentMS = ms;
			State.Instance.TimePassed += State.Instance.CurrentMS;
			return Id;
		}
	}
}

```


4. Hook everything up and run the `Game`.

```csharp
namespace Example
{
	public class App
	{
		public static void Main()
		{
			Game.DefaultSettings()
				.SetStageFactory(StageFactory)
				.Run(Id.Example);
		}

		public static IStage StageFactory(ICore core, int id)
		{
			switch (id)
			{
				case Id.Example:
					return new Example(core);
				default:
					throw new ArgumentException(nameof(id));
			}
		}
	}
}
```


## What to Expect?

You should expect to see something like the below screen capture.

![alt text](https://raw.githubusercontent.com/raspware/game-template-bridge.net/42a8abbd20772e38d8ae9526eb86b1acbc5c34cd/screencapture.png?token=ABTB73YQVCQ6ZEO75NDQHCS6JVBI2 "Screen Capture")
