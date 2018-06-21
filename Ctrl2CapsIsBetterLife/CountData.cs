using System;
using System.IO;
using System.Linq;

namespace Ctrl2CapsIsBetterLife
{
	internal class CountData
	{
		private static readonly string DirectoryPath = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"ats124/Ctrl2CapsIsBetterLife");

		public static string FilePath { get; } = Path.Combine(DirectoryPath, "count.txt");

		public int Ctrl { get; set; }
		public int Caps { get; set; }

		public static CountData GetCurrent()
		{
			if (File.Exists(FilePath))
			{
				var allLines = File.ReadAllLines(FilePath);
				if (allLines.Any())
				{
					var lastLine = allLines.Last();
					var data = lastLine.Split(',');
					if (data.Length == 2)
						if (int.TryParse(data[0], out var ctrl) && int.TryParse(data[1], out var caps))
							return new CountData {Ctrl = ctrl, Caps = caps};
				}
			}

			return new CountData();
		}

		public void WriteCurrent()
		{
			var allLines = File.Exists(FilePath)
				? File.ReadAllLines(FilePath)
				: new string[1];

			if (allLines.Length == 0) allLines = new string[1];
			allLines[allLines.Length - 1] = $"{Ctrl},{Caps}";
			Directory.CreateDirectory(DirectoryPath);
			File.WriteAllLines(FilePath, allLines);
		}

		public static void ResetCurrent()
		{
			Directory.CreateDirectory(DirectoryPath);
			File.AppendAllLines(FilePath, new[] {"0,0"});
		}
	}
}