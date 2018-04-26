using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Raspware.Base64ResourceEncoder
{

	public static class Program
	{
		private static void Main(string[] args)
		{
			var writer = new StringBuilder();
			writer.AppendLine($"namespace Raspware.ExampleGame.Resources");
			writer.AppendLine("{");

			WriteOutClass(writer, new Resource("Audio"));
			writer.Append("\n");
			WriteOutClass(writer, new Resource("Image"));

			writer.Append("}");

			var output = writer.ToString();

			// TODO: Maybe do some logging? As there is only one file, not sure if we need to do a lock.
			var resourceFile = new FileInfo(Path.GetFullPath(
					Path.Combine(
						Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
						@"..\..\..\ExampleGame\",
						"Resources.cs"
					)
				));

			File.WriteAllText(resourceFile.FullName, output);

			Console.WriteLine("Done!");
			Console.ReadKey();
		}

		private static void WriteOutClass(StringBuilder writer, Resource resourse)
		{
			writer.AppendLine($"\t/// <summary>This is automatically generated, DO NOT EDIT!</summary>");
			writer.AppendLine($"\tpublic static class {resourse.Type}");
			writer.AppendLine("\t{");
			resourse.Dictionary.ToList().ForEach(_ => writer.AppendLine($"\t\tpublic static readonly string {_.Key} = \"{_.Value}\";"));
			writer.AppendLine("\t}");
		}
	}

	public sealed class Resource
	{
		public Resource(string folderNameAndType)
		{
			if (string.IsNullOrWhiteSpace(folderNameAndType))
				throw new ArgumentNullException(nameof(folderNameAndType));

			Type = folderNameAndType;
			Dictionary = GetDictionary(GetResourceLocation(folderNameAndType));
		}

		private static DirectoryInfo GetResourceLocation(string folderName)
		{
			// TODO: Strengthen up checking if a file/direcrtory exsists or not 'Directory.Exists(path)'.
			return new DirectoryInfo(
				Path.GetFullPath(
					Path.Combine(
						Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
						@"..\..\..\Resources\" + folderName
					)
				)
			);
		}

		private static Dictionary<string, string> GetDictionary(DirectoryInfo location)
		{
			// TODO: Maybe this needs some validation
			return location.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
				.ToDictionary(
					file => file.Name.Split('.').First(),

					// Ref: https://stackoverflow.com/questions/25919387/c-sharp-converting-file-into-base64string-and-back-again
					file => $"data:{location.Name.ToLower()}/{file.Extension.Split('.').Last()};base64,{Convert.ToBase64String(File.ReadAllBytes(file.FullName))}"
				);
		}

		public Dictionary<string, string> Dictionary { get; }
		public string Type { get; }
	}
}