using System;
using Bridge.Html5;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Mouse
{

	// TODO: Implement this!
	public sealed class Actions : IActions
	{
		public static IActions Instance { get; private set; }
		private static bool _configured { get; set; } = false;
		private Actions(Resolution resolution, IButtons buttons, Layer layer)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));
			if (layer == null)
				throw new ArgumentNullException(nameof(layer));

			Up = new Events(resolution, buttons.Up, layer);
			Down = new Events(resolution, buttons.Down, layer);
			Left = new Events(resolution, buttons.Left, layer);
			Right = new Events(resolution, buttons.Right, layer);
			Cancel = new Events(resolution, buttons.Cancel, layer);
			Button1 = new Events(resolution, buttons.Button1, layer);

			layer.CanvasElement.OnMouseDown = (e) =>
			{
				InputMouseDown(e);
			};
			layer.CanvasElement.OnMouseUp = (e) =>
			{
				InputMouseUp(e);
			};
		}

		private void InputMouseDown(MouseEvent<HTMLCanvasElement> e)
		{
			Up.As<Events>().InputMouseDown(e);
			Down.As<Events>().InputMouseDown(e);
			Left.As<Events>().InputMouseDown(e);
			Right.As<Events>().InputMouseDown(e);
			Cancel.As<Events>().InputMouseDown(e);
			Button1.As<Events>().InputMouseDown(e);
		}

		private void InputMouseUp(MouseEvent<HTMLCanvasElement> e)
		{
			Up.As<Events>().InputMouseUp(e);
			Down.As<Events>().InputMouseUp(e);
			Left.As<Events>().InputMouseUp(e);
			Right.As<Events>().InputMouseUp(e);
			Cancel.As<Events>().InputMouseUp(e);
			Button1.As<Events>().InputMouseUp(e);
		}

		public static void ConfigureInstance(IButtons buttons, Layer layer)
		{
			if (_configured)
				throw new Exception($"'{nameof(Instance)}' has already been configured!");
			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));

			Instance = new Actions(Resolution.Instance, buttons, layer);
			_configured = true;
		}

		public IEvents Up { get; private set; }
		public IEvents Down { get; private set; }
		public IEvents Left { get; private set; }
		public IEvents Right { get; private set; }
		public IEvents Cancel { get; private set; }
		public IEvents Button1 { get; private set; }
	}
}
