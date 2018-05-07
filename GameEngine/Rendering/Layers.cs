using System;
using System.Linq;
using Bridge.Html5;
using ProductiveRage.Immutable;

namespace Raspware.GameEngine.Rendering
{
	// TODO: Turn 'Layers' into an interface.
	// TODO: Pass in NonNullList of 'Layer' rather than presume we are going to use 'Layers'.
	public sealed class Layers
	{
		private NonNullList<Layer> _layers { get; set; }

		//TODO: Move these into a single class
		public static HTMLDivElement Wrapper { get; } = new HTMLDivElement();
		private int _lastHeight = 0;
		private int _lastWidth = 0;
		public Layer Controls { get; private set; }

		public Layers(Resolution resolution)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			Controls = new Layer(resolution, 0, Wrapper, 0);

			Wrapper.AppendChild(Controls.CanvasElement);
			Document.Body.AppendChild(Wrapper);

			Wrapper.OnTouchStart = CancelDefault;
			Wrapper.OnTouchEnd = CancelDefault;
			Wrapper.OnTouchCancel = CancelDefault;
			Wrapper.OnTouchEnter = CancelDefault;
			Wrapper.OnTouchLeave = CancelDefault;
			Wrapper.OnTouchMove = CancelDefault;
			Wrapper.OnMouseMove = CancelDefault;
			Wrapper.OnMouseLeave = CancelDefault;
			Wrapper.OnMouseDown = CancelDefault;
			Wrapper.OnMouseEnter = CancelDefault;
			Wrapper.OnMouseOut = CancelDefault;
			Wrapper.OnMouseOver = CancelDefault;
			Wrapper.OnMouseUp = CancelDefault;
			Wrapper.OnMouseWheel = CancelDefault;
			Wrapper.OnContextMenu = CancelDefault;
		}

		public void Resize()
		{
			if (Wrapper.ClientHeight == _lastHeight && Wrapper.ClientWidth == _lastWidth)
				return;

			_layers.ToList().ForEach(layer => layer.Resize());

			_lastHeight = Wrapper.ClientHeight;
			_lastWidth = Wrapper.ClientWidth;
		}

		private static void CancelDefault(Event e)
		{
			if (e == null)
				throw new ArgumentNullException(nameof(e));
			e.PreventDefault();
		}
	}
}