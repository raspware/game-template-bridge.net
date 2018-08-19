using Bridge.Html5;

namespace Raspware.ExampleGame.Stage.RayCasterAdditional
{
	public sealed class Bitmap
	{
		public HTMLImageElement Image { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public Bitmap(string src, double width, double height)
		{
			Image = new HTMLImageElement() { Src = src };
			Width = width;
			Height = height;
		}
	}
}
