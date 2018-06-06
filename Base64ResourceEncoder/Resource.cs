using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Raspware.GameEngine.ResourceShared;

namespace Raspware.GameEngine.Base64ResourceEncoder
{
	public sealed class Resource
	{
		public Resource(string folderNameAndType)
		{
			if (string.IsNullOrWhiteSpace(folderNameAndType))
				throw new ArgumentException(nameof(folderNameAndType));

			Item = new ResourceTypeAndItems(folderNameAndType, GetObjects(GetResourceLocation(folderNameAndType)));
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

		private static List<Item> GetObjects(DirectoryInfo location)
		{
			var objects = new List<Item>();
			foreach (var file in location.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly).ToList())
			{
				objects.Add(new Item(
					file.Name.Split('.').First(),
					$"data:{location.Name.ToLower()}/{file.Extension.Split('.').Last()};base64,{Convert.ToBase64String(File.ReadAllBytes(file.FullName))}"
				));
			}
			return objects;
		}


		public ResourceTypeAndItems Item { get; }
	}
}