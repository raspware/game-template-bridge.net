using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Encoder
{

	public static class Program
	{
		private static void Main(string[] args)
		{
			var audio = GetDictionary(
				GetResourceLocation("Audio"),
				"audio"
			);

			var images = GetDictionary(
				GetResourceLocation("Images"),
				"image"
			);

			var summary = "/// <summary>This is automatically generated, DO NOT EDIT!</summary>";

			var writer = new StringBuilder();
			writer.AppendLine($"namespace Raspware.ExampleGame.Resources");
			writer.AppendLine("{");
			writer.AppendLine($"\t{summary}");
			writer.AppendLine("\tpublic static class Audio");
			writer.AppendLine("\t{");
			audio.ToList().ForEach(_ => writer.AppendLine($"\t\tpublic static readonly string {_.Key.ToLower()} = \"{_.Value}\";"));
			writer.AppendLine("\t}");

			writer.AppendLine($"\t{summary}");
			writer.AppendLine("\n\tpublic static class Images");
			writer.AppendLine("\t{");
			images.ToList().ForEach(_ => writer.AppendLine($"\t\tpublic static readonly string {_.Key.ToLower()} = \"{_.Value}\";"));
			writer.AppendLine("\t}");

			writer.AppendLine("}");

			var output = writer.ToString();

			// TODO: Maybe do some logging? As there is only one file, not sure if we need to do a lock.
			var controlFile = new FileInfo(Path.GetFullPath(
					Path.Combine(
						Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
						@"..\..\..\ExampleGame\",
						"Resources.cs"
					)
				));

			File.WriteAllText(controlFile.FullName, output);

			Console.WriteLine("Done!");
			Console.ReadKey();
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

		private static Dictionary<string, string> GetDictionary(DirectoryInfo location, string type)
		{
			// TODO: Maybe this needs some validation
			// Ref: https://stackoverflow.com/questions/25919387/c-sharp-converting-file-into-base64string-and-back-again
			return location.EnumerateFiles("*.*", SearchOption.AllDirectories)
				.ToDictionary(
					file => file.Name.Split('.').First().ToLower(),
					file => $"data:{type}/{file.Extension.Split('.').Last()};base64,{Convert.ToBase64String(File.ReadAllBytes(file.FullName))}"
				);
		}
	}
}