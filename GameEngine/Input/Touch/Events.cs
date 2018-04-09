using System;
using System.Collections.Generic;
using Bridge.Html5;
using Raspware.GameEngine.Input.Touch.Buttons;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
{
	// TODO: Implement this!
	public sealed class Events : IEvents
	{
		private Button _button { get; }
		private Resolution _resolution { get; }
		private Layer _layer { get; }

		private Dictionary<int, Button> _currentTouchers = new Dictionary<int, Button>();

		public Events(Resolution resolution, Button button, Layer layer)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (button == null)
				throw new ArgumentNullException(nameof(button));
			if (layer == null)
				throw new ArgumentNullException(nameof(layer));

			_resolution = resolution;
			_button = button;
			_layer = layer;

			Document.AddEventListener(EventType.TouchStart, TouchStart);

			// TODO: Finish these
			Document.AddEventListener(EventType.TouchEnd, PlaceholderEvent);
			Document.AddEventListener(EventType.TouchLeave, PlaceholderEvent);
			Document.AddEventListener(EventType.TouchMove, PlaceholderEvent);
			Document.AddEventListener(EventType.TouchCancel, PlaceholderEvent);
	
		}
		private void TouchStart(Event e)
		{
			if (e == null)
				throw new ArgumentNullException(nameof(e));

			var touchEvent = e.As<TouchEvent>();
			touchEvent.PreventDefault();

			foreach (var touch in touchEvent.ChangedTouches)
			{
				var xPercentage = (double)(touch.PageX - _layer.CanvasElement.OffsetLeft) / _layer.CanvasElement.OffsetWidth;
				var x = Convert.ToInt32(Math.Floor(Resolution.Instance.Width * xPercentage));

				var yPercentage = (double)(touch.PageY - _layer.CanvasElement.OffsetTop) / _layer.CanvasElement.OffsetHeight;
				var y = Convert.ToInt32(Math.Floor(Resolution.Instance.Height * yPercentage));

				_currentTouchers.Add(
					touch.Identifier,
					new Button(
						x,
						y,
						_resolution.RenderAmount(8)
					)
				);
			}

			Console.WriteLine(_currentTouchers.Count);
		}

		private void PlaceholderEvent(Event e)
		{
			if (e == null)
				throw new ArgumentNullException(nameof(e));

			var touchEvent = e.As<TouchEvent>();
			touchEvent.PreventDefault();

			_currentTouchers = new Dictionary<int, Button>(); // re-initalising so stop error occuring when it adds on the 'TouchStart' event.
		}

		public bool OnceOnPressDown()
		{
			return false;
		}

		public bool PostPressedDown()
		{
			return false;
		}

		public bool PressedDown()
		{
			return false;
		}
	}
}
