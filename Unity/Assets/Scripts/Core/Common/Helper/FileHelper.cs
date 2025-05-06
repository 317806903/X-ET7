using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ET
{
	public static class FileHelper
	{
		public static List<string> GetAllFiles(string dir, string searchPattern = "*")
		{
			List<string> list = new List<string>();
			GetAllFiles(list, dir, searchPattern);
			return list;
		}

		public static void GetAllFiles(List<string> files, string dir, string searchPattern = "*")
		{
			string[] fls = Directory.GetFiles(dir);
			foreach (string fl in fls)
			{
				files.Add(fl);
			}

			string[] subDirs = Directory.GetDirectories(dir);
			foreach (string subDir in subDirs)
			{
				GetAllFiles(files, subDir, searchPattern);
			}
		}

		public static void CreateDirectory(string filePath)
		{
			string directoryName = Path.GetDirectoryName(filePath); // 获取文件所在的目录名称
			if (directoryName != null && !Directory.Exists(directoryName)) {
				Directory.CreateDirectory(directoryName); // 创建目录（若不存在）
			}
		}

		public static void CleanDirectory(string dir)
		{
			if (!Directory.Exists(dir))
			{
				return;
			}
			foreach (string subdir in Directory.GetDirectories(dir))
			{
				Directory.Delete(subdir, true);
			}

			foreach (string subFile in Directory.GetFiles(dir))
			{
				File.Delete(subFile);
			}
		}

		public static void CopyDirectory(string srcDir, string tgtDir)
		{
			DirectoryInfo source = new DirectoryInfo(srcDir);
			DirectoryInfo target = new DirectoryInfo(tgtDir);

			if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
			{
				throw new Exception("父目录不能拷贝到子目录！");
			}

			if (!source.Exists)
			{
				return;
			}

			if (!target.Exists)
			{
				target.Create();
			}

			FileInfo[] files = source.GetFiles();

			for (int i = 0; i < files.Length; i++)
			{
				File.Copy(files[i].FullName, Path.Combine(target.FullName, files[i].Name), true);
			}

			DirectoryInfo[] dirs = source.GetDirectories();

			for (int j = 0; j < dirs.Length; j++)
			{
				CopyDirectory(dirs[j].FullName, Path.Combine(target.FullName, dirs[j].Name));
			}
		}

		public static void ReplaceExtensionName(string srcDir, string extensionName, string newExtensionName)
		{
			if (Directory.Exists(srcDir))
			{
				string[] fls = Directory.GetFiles(srcDir);

				foreach (string fl in fls)
				{
					if (fl.EndsWith(extensionName))
					{
						File.Move(fl, fl.Substring(0, fl.IndexOf(extensionName)) + newExtensionName);
						File.Delete(fl);
					}
				}

				string[] subDirs = Directory.GetDirectories(srcDir);

				foreach (string subDir in subDirs)
				{
					ReplaceExtensionName(subDir, extensionName, newExtensionName);
				}
			}
		}

		public static (bool, string) ReadFileText(string fullFileName)
		{
			if (System.IO.File.Exists(fullFileName) == false)
			{
				return (false, "");
			}
			return (true, System.IO.File.ReadAllText(fullFileName));
		}

		public static (bool, byte[]) ReadFileBytes(string fullFileName)
		{
			if (System.IO.File.Exists(fullFileName) == false)
			{
				return (false, null);
			}
			return (true, System.IO.File.ReadAllBytes(fullFileName));
		}

		public static void WriteFileText(string fullFileName, string content, bool isOverWrite)
		{
			if (isOverWrite == false && System.IO.File.Exists(fullFileName))
			{
				Log.Error($"-- File.Exists({fullFileName})");
				return;
			}

			FileHelper.CreateDirectory(fullFileName);
			System.IO.File.WriteAllText(fullFileName, content);
		}

		public static void WriteFileBytes(string fullFileName, byte[] data, bool isOverWrite)
		{
			if (isOverWrite == false && System.IO.File.Exists(fullFileName))
			{
				Log.Error($"-- File.Exists({fullFileName})");
				return;
			}

			FileHelper.CreateDirectory(fullFileName);
			System.IO.File.WriteAllBytes(fullFileName, data);
		}

	}
}
