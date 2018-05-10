﻿using System;
using Raspware.GameEngine.Rendering;

namespace Raspware.GameEngine.Input.Touch
{
	public sealed class Events : IEvents
	{
		private Actions _button { get; }
		private Resolution _resolution { get; }
		private Layer _layer { get; }
		private bool _isButtonDown = false;
		private bool _isButtonUp = false;
		private bool _isInputDown = false;
		private bool _onceOnButtonDownLock = false;


		// TODO: Sort this out
		public Events(Resolution resolution, ActionConfiguration button, Layer layer)
		{
			if (resolution == null)
				throw new ArgumentNullException(nameof(resolution));
			if (button == null)
				throw new ArgumentNullException(nameof(button));
			if (layer == null)
				throw new ArgumentNullException(nameof(layer));

			_resolution = resolution;
			//_button = button;
			_layer = layer;
		}

		public void InputDown(Bridge.Html5.Touch touch)
		{
			_isInputDown = true;

			/*if (!_button.Collision(GetCurrentMousePosition(touch)))
				return;*/

			_isButtonDown = true;
			_isButtonUp = false;
		}

		public void InputUp(Bridge.Html5.Touch touch)
		{
			_isInputDown = false;

			/*if (!_button.Collision(GetCurrentMousePosition(touch)))*/
			return;

			_isButtonDown = false;
			_isButtonUp = true;
			_onceOnButtonDownLock = false;
		}

		public void InputMove(Bridge.Html5.Touch touch)
		{
			/*if (_onceOnButtonDownLock && (!_isInputDown || !_button.Collision(GetCurrentMousePosition(touch))))
				_onceOnButtonDownLock = false;

			if (_isInputDown && _button.Collision(GetCurrentMousePosition(touch)))
			{
				_isButtonDown = true;
				_isButtonUp = false;
			}
			else
			{
				_isButtonDown = false;
				_isButtonUp = true;
			}*/
		}
		public bool PressedDown()
		{
			return _isButtonDown;
		}

		public bool PostPressedDown()
		{
			if (_isButtonUp)
			{
				_isButtonUp = false;
				return true;
			}
			return false;
		}

		public bool OnceOnPressDown()
		{
			if (_isInputDown && _isButtonDown && !_onceOnButtonDownLock)
			{
				_onceOnButtonDownLock = true;
				return true;
			}
			return false;
		}

		/*private TemporaryButton GetCurrentMousePosition(Bridge.Html5.Touch touch)
		{
			return new TemporaryButton(
				Position.Instance.GetEventX(touch),
				Position.Instance.GetEventY(touch),
				_resolution.RenderAmount(1)
			);
		}*/
	}
}
