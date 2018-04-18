using System;
using System.Collections.Generic;
using Bridge.Html5;
using Raspware.TestZone.ReusableObjects;

namespace Raspware.TestZone
{
	public static class Touch_With_Collision

	{
		private const string CSS = @"
			 <style>
				html, body, div, canvas, h2 {
					margin: 0;
					padding:0;
				}
				body {
					margin: 0 60px;
				}
				h2 {
					margin: 20px 0;
				}
				div {
					position: relative;
					background-color: #000;
					width: 600px;
					height: 360px;
					margin: 70px 0px 0 80px;
				}
				canvas {
					position: absolute;
					background-color: #7777FF;
					width: 100%;
					height: 100%;
				}
			</style>";

		private const int Width = 1980;
		private const int Height = 1080;

		private static Dictionary<int, Circle> _currentTouches = new Dictionary<int, Circle>();
		private static HTMLCanvasElement _controls;
		private static IEnumerable<Button> _buttons = new[]
		{
			new Button()
			{
				Circle = new Circle()
				{
					X = 300,
					Y = 300
				}
			},
			new Button()
			{
				Circle = new Circle()
				{
					X = 600,
					Y = 800
				}
			}

		};

		public static void Run()
		{
			var wrapper = new HTMLDivElement()
			{
				Id = "wrapper",
				OnTouchStart = CancelDefault,
				OnTouchEnd = CancelDefault,
				OnTouchCancel = CancelDefault,
				OnTouchEnter = CancelDefault,
				OnTouchLeave = CancelDefault,
				OnTouchMove = CancelDefault,
				OnMouseMove = CancelDefault,
				OnMouseLeave = CancelDefault,
				OnMouseDown = CancelDefault,
				OnMouseEnter = CancelDefault,
				OnMouseOut = CancelDefault,
				OnMouseOver = CancelDefault,
				OnMouseUp = CancelDefault,
				OnMouseWheel = CancelDefault,
				OnContextMenu = CancelDefault
			};

			_controls = new HTMLCanvasElement()
			{
				Id = "Controls",
				Width = Width,
				Height = Height,
				OnTouchStart = (touchEvent) =>
				{
					var touches = touchEvent.ChangedTouches;
					foreach (var touch in touches)
					{
						var xPercentage = (double)(touch.PageX - (touch.Target.OffsetLeft + wrapper.OffsetLeft)) / touch.Target.OffsetWidth;
						var x = Convert.ToInt32(Math.Floor(Width * xPercentage));
						var yPercentage = (double)(touch.PageY - (touch.Target.OffsetTop + wrapper.OffsetTop)) / touch.Target.OffsetHeight;
						var y = Convert.ToInt32(Math.Floor(Height * yPercentage));
						if (!_currentTouches.ContainsKey(touch.Identifier))
							_currentTouches.Add(
								touch.Identifier,
								new Circle()
								{
									X = x,
									Y = y
								}
							);

						SetButtons();
					}
				},
				OnTouchMove = (touchEvent) =>
				{
					var touches = touchEvent.ChangedTouches;
					foreach (var touch in touches)
					{
						// if a collision is found
						// make the button active
						// break from the 'foreach'

						var xPercentage = (double)(touch.PageX - (touch.Target.OffsetLeft + wrapper.OffsetLeft)) / touch.Target.OffsetWidth;
						var x = Convert.ToInt32(Math.Floor(Width * xPercentage));
						var yPercentage = (double)(touch.PageY - (touch.Target.OffsetTop + wrapper.OffsetTop)) / touch.Target.OffsetHeight;
						var y = Convert.ToInt32(Math.Floor(Height * yPercentage));

						// TODO: Maybe we want to know if it does not find this
						if (!_currentTouches.ContainsKey(touch.Identifier))
							return;

						_currentTouches.Get(touch.Identifier).X = x;
						_currentTouches.Get(touch.Identifier).Y = y;

						SetButtons();
					}
				},
				OnTouchEnd = OnTouchEndLeaveAndCancel,
				OnTouchLeave = OnTouchEndLeaveAndCancel,
				OnTouchCancel = OnTouchEndLeaveAndCancel
			};

			var title = new HTMLHeadingElement(HeadingType.H2)
			{
				TextContent = "I am the title"
			};

			wrapper.AppendChild(_controls);

			Document.Body.InnerHTML = CSS;
			Document.Body.AppendChild(title);
			Document.Body.AppendChild(wrapper);

			Tick();
		}

		private static void CancelDefault(Event e)
		{
			if (e == null)
				throw new ArgumentNullException(nameof(e));
			e.PreventDefault();
		}

		private static void OnTouchEndLeaveAndCancel(TouchEvent<HTMLCanvasElement> touchEvent)
		{
			if (touchEvent == null)
				throw new ArgumentNullException(nameof(touchEvent));

			var touches = touchEvent.ChangedTouches;
			foreach (var touch in touches)
			{
				if (_currentTouches.ContainsKey(touch.Identifier))
					_currentTouches.Remove(touch.Identifier);
			}

			SetButtons();
		}

		public static void SetButtons()
		{
			foreach (var button in _buttons)
			{
				var collisionWithButton = false;
				foreach (var touch in _currentTouches)
				{
					if (Collision.Circle(button.Circle, touch.Value))
						collisionWithButton = true;
				}
				button.Active = collisionWithButton;
			}
		}

		private static void Tick()
		{
			var context = _controls.GetContext(CanvasTypes.CanvasContext2DType.CanvasRenderingContext2D);
			context.ClearRect(0, 0, Width, Height);

			foreach (var button in _buttons)
			{
				context.BeginPath();
				context.Arc(
					button.Circle.X,
					button.Circle.Y,
					button.Circle.Radius,
					Math.PI * 2,
					0
				);
				context.FillStyle = button.Active ? "green" : "orange";
				context.ClosePath();
				context.Fill();
			}

			foreach (var touch in _currentTouches)
			{
				context.BeginPath();
				context.Arc(
					touch.Value.X,
					touch.Value.Y,
					touch.Value.Radius,
					Math.PI * 2,
					0
				);
				context.FillStyle = "red";
				context.ClosePath();
				context.Fill();
			}
			Window.RequestAnimationFrame(Tick);
		}
	}
}