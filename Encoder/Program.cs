using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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

			// TODO: Make this generate a 'resources' static class that can be used by the Bridge game.

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