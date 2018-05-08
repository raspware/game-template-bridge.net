using System;
using System.Linq;
using Bridge.Html5;
using ProductiveRage.Immutable;

namespace Raspware.GameEngine.Rendering
{
	public sealed class Layers
	{
		private NonNullList<Layer> _stageLayers { get; set; }
		private Resolution _resolution { get; set; }
		public static HTMLDivElement Wrapper { get; } = new HTMLDivElement();
		private int _lastHeight = 0;
		private int _lastWidth = 0;
		public Layer Controls { get; private set; }

		public Layers(Resolution resolution)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));

			_resolution = resolution;

			Controls = new Layer(_resolution, 0, Wrapper);

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

			foreach (var layer in _stageLayers)
				layer.Resize();

			Controls.Resize();

			_lastHeight = Wrapper.ClientHeight;
			_lastWidth = Wrapper.ClientWidth;
		}

		public Layer GetStageLayer(int id)
		{
			var layer = _stageLayers.Where(_ => _.Id == id).FirstOrDefault();
			if (layer == null)
				throw new ArgumentNullException(nameof(layer));

			return layer;
		}

		public void Reset(NonNullList<int> ids)
		{
			if (ids == null)
				throw new ArgumentNullException(nameof(ids));

			_stageLayers = NonNullList.Of(ids.Select(id => new Layer(_resolution, id, Wrapper)).ToArray()); // Build up new layers.

			Wrapper.InnerHTML = ""; // reset HTML (there is no event listeners on the children, so this should be fine.

			foreach (var layer in _stageLayers)
				Wrapper.AppendChild(layer.CanvasElement);

			Wrapper.AppendChild(Controls.CanvasElement); // remember to add the 'Controls' back and do it last as we always want this to be on top
		}

		private static void CancelDefault(Event e)
		{
			if (e == null)
				throw new ArgumentNullException(nameof(e));
			e.PreventDefault();
		}
	}
}