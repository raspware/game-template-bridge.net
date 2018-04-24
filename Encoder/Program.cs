using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Encoder
{
	class Program
	{
		static void Main(string[] args)
		{
			// Ref: https://stackoverflow.com/questions/25919387/c-sharp-converting-file-into-base64string-and-back-again
			var resourcePathBase = @"..\..\..\Resources\";

			var audioLocation = new DirectoryInfo(
				Path.GetFullPath(
					Path.Combine(
						Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
						resourcePathBase + "Audio"
					)
				)
			);

			var audioDict = new Dictionary<string, string>();
			audioLocation.EnumerateFiles("*.*", SearchOption.AllDirectories).ToList()
			   .ForEach(
					file => audioDict.Add(
						file.Name.Split('.').First().ToLower(),
						$"data:audio/{file.Extension.Split('.').Last()};base64,{Convert.ToBase64String(File.ReadAllBytes(file.FullName))}"
					)
				);


			
			var imagesLocation = new DirectoryInfo(
				Path.GetFullPath(
					Path.Combine(
						Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
						resourcePathBase + "Images"
					)
				)
			);

			var imagesDict = new Dictionary<string, string>();
			imagesLocation.EnumerateFiles("*.*", SearchOption.AllDirectories).ToList()
			   .ForEach(
					file => imagesDict.Add(
						file.Name.Split('.').First().ToLower(),
						$"data:image/{file.Extension.Split('.').Last()};base64,{Convert.ToBase64String(File.ReadAllBytes(file.FullName))}"
					)
				);

			// TODO: Make this generate a 'resources' static class that can be used by the Bridge game.
			// TODO: Strengthen up checking if a file/direcrtory exsists or not 'Directory.Exists(path)'.
		}
	}
}