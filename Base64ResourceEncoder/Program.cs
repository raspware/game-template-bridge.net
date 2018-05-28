using System;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Raspware.Base64ResourceEncoder
{

	public static class Program
	{
		private static void Main(string[] args)
		{
			var sb = new StringBuilder();
			var sw = new StringWriter(sb);

			using (var writer = new JsonTextWriter(sw))
			{
				writer.Formatting = Formatting.Indented;
				writer.WriteStartObject();

				// TODO: Collect 'Audio/Image' names from the folder names rather than hardcoding them
				WriteOutResource(writer, new Resource("Audio"));
				WriteOutResource(writer, new Resource("Image"));
				writer.WriteEndObject();
			}
			var output = sb.ToString();

			// TODO: Maybe do some logging? As there is only one file, not sure if we need to do a lock.
			var resourceFile = new FileInfo(Path.GetFullPath(
					Path.Combine(
						Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
						"",
						"resources.json"
					)
				));

			File.WriteAllText(resourceFile.FullName, output);

			Console.WriteLine("Done!");
			Console.ReadKey();
		}

		private static void WriteOutResource(JsonTextWriter writer, Resource resourse)
		{
			if (writer == null)
				throw new ArgumentNullException(nameof(writer));
			if (resourse == null)
				throw new ArgumentNullException(nameof(resourse));

			writer.WritePropertyName(resourse.Type);
			writer.WriteStartArray();
			foreach (var r in resourse.Dictionary)
			{
				writer.WriteStartObject();
				writer.WritePropertyName("Title");
				writer.WriteValue(r.Key);
				writer.WritePropertyName("Src");
				writer.WriteValue(r.Value);
				writer.WriteEndObject();
			}
			writer.WriteEnd();
		}
	}
}