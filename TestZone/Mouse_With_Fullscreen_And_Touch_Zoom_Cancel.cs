using System;
using Bridge.Html5;

namespace Raspware.TestZone
{
	public static class Mouse_With_Fullscreen_And_Touch_Zoom_Cancel
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
				div.fullscreen {
					position: absolute;
					width: 100%;
					height: 100%;
					margin: 0;
				}
				canvas {
					position: absolute;
					background-color: #7777FF;
					width: 100%;
					height: 100%;
				}
			</style>";

		private const string TextContent = @"
Lorem ipsum dolor sit amet, ubique apeirian phaedrum ei est. Qui an essent qualisque. In duo ocurreret scribentur. Eum complectitur consequuntur ut. Autem iudico eligendi usu te. Dictas vituperatoribus id vim, pro tibique rationibus ne, mundi aliquando cu per.

Eius voluptua rationibus qui ne, iudico detracto ad sea, at omnes explicari accusamus mel. Suscipit patrioque gubergren his ei, nec in civibus appareat oportere, vim probo atqui ad. Pro ludus fuisset interesset cu, harum quaeque evertitur est at. Ad sed cetero eruditi, ut vim quis consulatu. Duo virtute concludaturque at, sumo albucius pri et.

Cu augue pericula est, et wisi erroribus voluptaria usu. Antiopam torquatos argumentum sea ne, no commodo consulatu deseruisse pri. Quo id audire elaboraret, per et facilisi constituam interesset. Stet debet mollis an mea, ea zril tritani prodesset eum. Veri persius usu ei, mutat noluisse in vis. Quem atqui choro usu ne. Sonet voluptaria nam ut.

Nec ut persequeris reformidans, vel ut omnis dicam eirmod. Cum nihil mentitum consetetur ex, menandri conceptam eos ut. Pri cu putent sententiae philosophia, vim doctus theophrastus et. Et vis vidit errem, solum incorrupte ius te.

Eu etiam mazim cum, eu vel audiam vocibus iudicabit. Quo erant vocibus torquatos ad. Id eros mazim interesset pri. Mei vidisse comprehensam ad, exerci accusata accommodare in eam, pri no omnis persequeris. Mel cu modo corrumpit. Duis tota id mei, eam te modus deserunt principes, ea legimus intellegat assueverit mel. Audire propriae sed ea, ei mentitum comprehensam eam.

Mel ut audiam epicuri intellegebat, regione integre evertitur ex ius. Vix luptatum splendide id, an cum antiopam necessitatibus. Ea pri diam verterem consequat, duis nusquam sed cu. Ea simul dolorum legendos ius. Cum in ludus persius. Ius oblique fastidii ne, pro ei nullam tamquam eripuit, nam agam mazim altera an.

Ut facete gubergren pri, vis maiorum maluisset elaboraret at. Usu at nullam nostrum aliquando. Latine persius dignissim nam cu, audiam suscipit iudicabit vix no. Has ne saepe eligendi temporibus, meis malis corrumpit vis ne.

Alii nihil vix ei. Nihil elaboraret mel no, at facilisis accommodare est. Est ea partiendo dissentias definitiones, mel electram accusamus in. Ius ut malis inani, ius no sonet ocurreret philosophia. No nam accumsan inimicus, lobortis repudiare ut usu. An dicam nominati complectitur his, te sumo cibo eruditi sit.

Pro ut falli iusto. No nostrud euismod est, vix molestiae adipiscing ex, in vidit facete aperiam sed. Vel at cetero argumentum, semper melius appareat est an. Ad iracundia voluptatibus pro, omnis feugiat ad nam. Quo te sapientem voluptaria.

Cu odio facer veniam cum. Te mea nulla appareat invidunt, vim ceteros officiis cu. Usu diceret invidunt phaedrum ne, pri vidit ludus accusam ex, ea vix adhuc fabulas. No facete iudicabit sea, pro te eripuit probatus. Eu pri primis iuvaret disputando. Ei tibique corpora argumentum sed. Vis accusam pertinax tractatos ut.

Vis te alii cotidieque, ne soluta quidam convenire eam. Vis ex perfecto scripserit, case atomorum pri ea, vim possim doctus suavitate no. Eu pri option expetenda, epicurei deterruisset ex sea. Vix ad dicit abhorreant sententiae, per amet audire adipisci in. Dolore appetere deserunt eu his. Voluptaria necessitatibus an pro, eu his viderer propriae.

Usu ei natum prima maiestatis, et vix nemore delectus. Salutandi interpretaris ea ius, cu sea prodesset mnesarchum. Ei duis dolor sit, mea ad nulla quando bonorum. Ne vel autem urbanitas, pri ut ignota eirmod molestiae, cu vidit legimus epicurei vix.

Rebum feugiat accumsan ea eam, no usu officiis invidunt adipiscing, eu esse munere causae nam. In vel fierent percipit, labitur eripuit concludaturque no mea, mei placerat accommodare no. Paulo latine phaedrum sed at. Cum postea quaestio te.

Quaestio voluptatibus vix ut, ei habeo corpora quaerendum cum. Usu dicunt deserunt sapientem ei. Ceteros postulant at pri, ea audire aliquando vel. Saepe semper volutpat qui an.

Qui te dicat dicta tacimates, solum scaevola cum ne. Sed putent praesent an. No pri posse quaestio splendide. Vide meis vis ad.

Mei ea ignota everti lucilius, pri at autem malorum theophrastus, ea cum debitis theophrastus. Discere aliquid eu nec, vis nibh postea delenit ex. Vel ad primis adolescens, usu ad alia nihil, eum forensibus appellantur ut. Quas feugait deterruisset vim ne, nam postea sapientem dissentiet et. Id quo elitr noster timeam. Ea eam noster graecis.

Qui legere eligendi comprehensam in, an ius indoctum urbanitas. Pri et feugiat equidem reprimique, his utroque percipit suavitate at. Ne usu modo probatus. Vis splendide adolescens referrentur ut. Quando graeco aperiri usu ne, ne dicunt dolores vel. Ea sed malis scripta tacimates, te commodo convenire vel.

Est elitr epicuri placerat in. Enim sale maluisset sea ut, nam sumo minim voluptua ad. Audiam accusamus urbanitas eum in, modus essent detracto at nec, regione praesent per ad. Te vix graecis fabellas, cu diam mutat suscipiantur sea. Vel aperiam tamquam eu, has cu duis scriptorem.

Purto complectitur est ne, ex possit persius persecuti sit. Percipit elaboraret instructior id sed, id dicit ponderum sit, id sensibus instructior vim. Ex scripserit philosophia ius, mel aperiam saperet indoctum ea. Mel id lucilius comprehensam. Aliquip similique definiebas ut has, esse periculis an nec. Eum ex verear latine consectetuer.
";

		private const int Width = 1980;
		private const int Height = 1080;

		public static void Run()
		{
			var title = new HTMLHeadingElement(HeadingType.H2)
			{
				TextContent = "I am the title"
			};

			var wrapper = new HTMLDivElement()
			{
				Id = "wrapper",
				OnTouchStart = CancelDefault,
				OnTouchEnd = CancelDefault,
				OnTouchCancel = CancelDefault,
				OnTouchEnter = CancelDefault,
				OnTouchLeave = CancelDefault,
				OnTouchMove = CancelDefault
			};

			var background = new HTMLCanvasElement()
			{
				Id = "Background",
				Width = Width,
				Height = Height,
				OnTouchStart = CancelDefault,
				OnTouchEnd = CancelDefault,
				OnTouchCancel = CancelDefault,
				OnTouchEnter = CancelDefault,
				OnTouchLeave = CancelDefault,
				OnTouchMove = CancelDefault
			};

			var foreground = new HTMLCanvasElement()
			{
				Id = "Foreground",
				Width = Width,
				Height = Height,
				OnTouchStart = CancelDefault,
				OnTouchEnd = CancelDefault,
				OnTouchCancel = CancelDefault,
				OnTouchEnter = CancelDefault,
				OnTouchLeave = CancelDefault,
				OnTouchMove = CancelDefault
			};

			var controls = new HTMLCanvasElement()
			{
				Id = "Controls",
				Width = Width,
				Height = Height,
				OnTouchStart = CancelDefault,
				OnTouchEnd = CancelDefault,
				OnTouchCancel = CancelDefault,
				OnTouchEnter = CancelDefault,
				OnTouchLeave = CancelDefault,
				OnTouchMove = CancelDefault,
				OnMouseMove = (ev) =>
				{
					var xPercentage = (double)(ev.PageX - (ev.Target.OffsetLeft + wrapper.OffsetLeft)) / ev.Target.OffsetWidth;
					var x = Convert.ToInt32(Math.Floor(Width * xPercentage));
					var yPercentage = (double)(ev.PageY - (ev.Target.OffsetTop + wrapper.OffsetTop)) / ev.Target.OffsetHeight;
					var y = Convert.ToInt32(Math.Floor(Height * yPercentage));
					var context = ev.Target.GetContext("2d").As<CanvasRenderingContext2D>();
					context.ClearRect(0, 0, Width, Height);
					context.BeginPath();
					context.Arc(x, y, 10, Math.PI * 2, 0);
					context.FillStyle = "red";
					context.ClosePath();
					context.Fill();
				},
				OnMouseLeave = (ev) =>
				{
					var context = ev.Target.GetContext("2d").As<CanvasRenderingContext2D>();
					context.ClearRect(0, 0, Width, Height);
					context.BeginPath();
				},
				OnMouseUp = (ev) =>
				{
					// Fullscreen
					/*@
						var element = document.getElementsByTagName("div")[0];
						if(element.requestFullscreen)
							element.requestFullscreen();
						else if(element.mozRequestFullScreen)
							element.mozRequestFullScreen();
						else if(element.webkitRequestFullscreen)
							element.webkitRequestFullscreen();
						else if(element.msRequestFullscreen)
							element.msRequestFullscreen();
					*/
				},
			};

			Window.OnResize = (ev) =>
			{
				// Apply fullscreen CSS if it is fullscrren.
				/*@
				var element = document.getElementsByTagName("div")[0];
				var full_screen_element = document.fullscreenElement || document.webkitFullscreenElement || document.mozFullScreenElement || document.msFullscreenElement || null;
				if(full_screen_element === null)
					element.className = "";
				else
					element.className = "fullscreen";
				 */
			};

			wrapper.AppendChild(background);
			wrapper.AppendChild(foreground);
			wrapper.AppendChild(controls);

			Document.Body.InnerHTML = CSS;
			Document.Body.AppendChild(title);
			Document.Body.AppendChild(new HTMLParagraphElement() { TextContent = TextContent });
			Document.Body.AppendChild(new HTMLParagraphElement() { TextContent = TextContent });
			Document.Body.AppendChild(new HTMLParagraphElement() { TextContent = TextContent });
			Document.Body.AppendChild(wrapper);
			Document.Body.AppendChild(new HTMLParagraphElement() { TextContent = TextContent });
		}

		private static void CancelDefault(Event e)
		{
			var ev = e.As<TouchEvent>();
			ev.PreventDefault();
		}
	}
}