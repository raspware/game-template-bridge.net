using System;
using Bridge.Html5;
using Raspware.GameEngine.Input.Shared;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Mouse
{
	public sealed class Actions
	{
		public static IActions Instance { get; private set; }
		private static bool _configured { get; set; } = false;
		private Actions(Resolution resolution, Shared.IActions buttons, Layer layer)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));
			if (layer == null)
				throw new ArgumentNullException(nameof(layer));


			var a = buttons.Actions


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
			layer.CanvasElement.OnMouseMove = (e) =>
			{
				InputMouseMove(e);
			};
		}

		private void InputMouseDown(MouseEvent<HTMLCanvasElement> e)
		{
			Up.As<Events>().InputDown(e);
			Down.As<Events>().InputDown(e);
			Left.As<Events>().InputDown(e);
			Right.As<Events>().InputDown(e);
			Cancel.As<Events>().InputDown(e);
			Button1.As<Events>().InputDown(e);
		}

		private void InputMouseUp(MouseEvent<HTMLCanvasElement> e)
		{
			Up.As<Events>().InputUp(e);
			Down.As<Events>().InputUp(e);
			Left.As<Events>().InputUp(e);
			Right.As<Events>().InputUp(e);
			Cancel.As<Events>().InputUp(e);
			Button1.As<Events>().InputUp(e);
		}
		private void InputMouseMove(MouseEvent<HTMLCanvasElement> e)
		{
			Up.As<Events>().InputMove(e);
			Down.As<Events>().InputMove(e);
			Left.As<Events>().InputMove(e);
			Right.As<Events>().InputMove(e);
			Cancel.As<Events>().InputMove(e);
			Button1.As<Events>().InputMove(e);
		}


		public static void ConfigureInstance(Shared.IActions buttons, Layer layer)
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
