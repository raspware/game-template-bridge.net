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

			var audioDict = new Dictionary<string, string>();
			var audioLocation = new DirectoryInfo(
				Path.Combine(
					Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
					"audio"
				)
			);
			audioLocation.EnumerateFiles("*.*", SearchOption.AllDirectories).ToList()
			   .ForEach(
					file => audioDict.Add(
						file.Name.Split('.').First().ToLower(),
						$"data:audio/{file.Extension.Split('.').Last()};base64,{Convert.ToBase64String(File.ReadAllBytes(file.FullName))}"
					)
				);


			var imagesDict = new Dictionary<string, string>();
			var imagesLocation = new DirectoryInfo(
				Path.Combine(
					Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
					"images"
				)
			);
			imagesLocation.EnumerateFiles("*.*", SearchOption.AllDirectories).ToList()
			   .ForEach(
					file => imagesDict.Add(
						file.Name.Split('.').First().ToLower(),
						$"data:image/{file.Extension.Split('.').Last()};base64,{Convert.ToBase64String(File.ReadAllBytes(file.FullName))}"
					)
				);

			// TODO: Make it target a folder that does not need to copy files into the 'debug' folder as indicating that a file should be copied in the
			// properties panel will tedious if there is loads of files.
			// TODO: Make this generate a 'resources' static class that can be used by the Bridge game.
			// TODO: Strengthen up checking if a file/direcrtory exsists or not 'Directory.Exists(path)'.
		}
	}
}