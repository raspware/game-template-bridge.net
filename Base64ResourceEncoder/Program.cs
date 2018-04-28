using System;
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
			writer.AppendLine($"namespace Raspware.ExampleGame.Resource");
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
						"Resource.cs"
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
}