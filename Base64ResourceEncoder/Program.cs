using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
			var resources = new List<Resource>();

			using (var writer = new JsonTextWriter(sw))
			{
				writer.Formatting = Formatting.None;
				writer.WriteStartObject();
				folderNames.ForEach(folderName =>
				{
					var resource = new Resource(folderName);
					resources.Add(resource);
					WriteOutJSONResource(writer, resource);
				});
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
			Console.WriteLine("Wrote JSON! Press 'any' key.");

			if (!resources.Any())
			{
				Console.WriteLine("No resources to write out Bridge helper!");
				Console.ReadKey();
				return;
			}

			var bridgeStringBuilder = new StringWriter();
			bridgeStringBuilder.WriteLine("namespace Resources \n{");
			resources.ForEach(resource => WriteOutBridgeResource(bridgeStringBuilder, resource));
			bridgeStringBuilder.Write("}");

			output = bridgeStringBuilder.ToString();
			resourceFile = new FileInfo(Path.GetFullPath(
					Path.Combine(
						Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
						"",
						"Resources.cs"
					)
				));
			File.WriteAllText(resourceFile.FullName, output);

			Console.WriteLine("Wrote Bridge File! Press 'any' key.");
			Console.ReadKey();
		}

		private static void WriteOutJSONResource(JsonTextWriter writer, Resource resource)
		{
			if (writer == null)
				throw new ArgumentNullException(nameof(writer));
			if (resource == null)
				throw new ArgumentNullException(nameof(resource));

			writer.WritePropertyName(resource.Item.Type);
			Console.WriteLine("------");
			Console.WriteLine($"{resource.Item.Type}\n------");

			writer.WriteStartArray();
			foreach (var r in resource.Item.Dictionary)
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
		private static void WriteOutBridgeResource(StringWriter writer, Resource resource)
		{
			if (writer == null)
				throw new ArgumentNullException(nameof(writer));
			if (resource == null)
				throw new ArgumentNullException(nameof(resource));

			Console.WriteLine("------");
			Console.WriteLine($"{resource.Item.Type}\n------");

			writer.Write($"\tpublic static class {resource.Item.Type}\n{{");
			foreach (var r in resource.Item.Dictionary)
			{
				Console.WriteLine($"(*) {r.Key}");
				writer.Write($"\n\t\tpublic const string {r.Key} = \"{r.Key}\"; ");
			}
			writer.WriteLine("\n\t}");

			Console.WriteLine("\n");
		}
	}
}