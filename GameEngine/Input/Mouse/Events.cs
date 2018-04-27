using System;
using System.Collections.Generic;
using Raspware.GameEngine.Input.SharedButtons;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Mouse
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
