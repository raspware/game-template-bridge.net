using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Raspware.GameEngine.Base64ResourceObjects;

namespace Raspware.Base64ResourceEncoder
{
	public sealed class Resource
	{
		public Resource(string folderNameAndType)
		{
			if (string.IsNullOrWhiteSpace(folderNameAndType))
				throw new ArgumentException(nameof(folderNameAndType));

			Item = new Item(folderNameAndType, GetDictionary(GetResourceLocation(folderNameAndType)));
		}

		private static DirectoryInfo GetResourceLocation(string folderName)
		{
			// TODO: Strengthen up checking if a file/direcrtory exsists or not 'Directory.Exists(path)'.
			return new DirectoryInfo(
				Path.GetFullPath(
					Path.Combine(
						Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
						folderName
					)
				)
			);
		}

		public static List<string> GetResourceFolderNames()
		{
			var resourcesPath =
				Path.GetFullPath(
					Path.Combine(
						Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
					)
				);

			var folderNames = new List<string>();
			foreach (string s in Directory.GetDirectories(resourcesPath))
				folderNames.Add(s.Remove(0, resourcesPath.Length).Replace("\\", ""));

			return folderNames.Any() ? folderNames : null;
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


		public Item Item { get; }
	}
}