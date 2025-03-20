using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace GIBDD
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static SQLiteConnect context;
		public static bool IsAdmin { get; set; }
		public static bool IsDriver { get; set; }
		protected override void OnStartup(StartupEventArgs e)
		{
			context = new();
		}

		public static string PhotographyDirectory
		{
			get
			{
				string dirPath = @"\Photo";
				string programPath = Directory.GetCurrentDirectory();
				string path = programPath + dirPath;
				if (Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				return path;
			}
		}
	}
}
