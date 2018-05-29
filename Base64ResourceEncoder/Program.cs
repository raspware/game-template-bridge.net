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
			var folderNames = Resource.GetResourceFolderNames();

			if (folderNames == null)
				throw new ArgumentNullException(nameof(folderNames));

			var sb = new StringBuilder();
			var sw = new StringWriter(sb);

			using (var writer = new JsonTextWriter(sw))
			{
				writer.Formatting = Formatting.None;
				writer.WriteStartObject();
				folderNames.ForEach(resource => WriteOutResource(writer, new Resource(resource)));
				writer.WriteEndObject();
			}
			var output = sb.ToString();

			var resourceFile = new FileInfo(Path.GetFullPath(
					Path.Combine(
						Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
						"",
						"resources.json"
					)
				));

			File.WriteAllText(resourceFile.FullName, output);

			Console.WriteLine("Done! Press 'any' key.");
			Console.ReadKey();
		}

		private static void WriteOutResource(JsonTextWriter writer, Resource resource)
		{
			if (writer == null)
				throw new ArgumentNullException(nameof(writer));
			if (resource == null)
				throw new ArgumentNullException(nameof(resource));

			writer.WritePropertyName(resource.Type);
			Console.WriteLine("------");
			Console.WriteLine($"{resource.Type}\n------");

			writer.WriteStartArray();
			foreach (var r in resource.Dictionary)
			{
				writer.WriteStartObject();
				writer.WritePropertyName("Title");
				Console.WriteLine($"(*) {r.Key}");
				writer.WriteValue(r.Key);
				writer.WritePropertyName("Src");
				writer.WriteValue(r.Value);
				writer.WriteEndObject();
			}
			writer.WriteEnd();
			Console.WriteLine("\n");
		}
	}
}