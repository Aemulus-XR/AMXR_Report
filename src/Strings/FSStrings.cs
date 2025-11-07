using log4net;

using System.Diagnostics;

namespace AemulusConnect.Strings
{
	public static class FSStrings
	{
		private static readonly ILog _logger = LogManager.GetLogger(typeof(FSStrings));

		// Changed to the device Documents folder where headset files were found via USB/MTP
		// This maps to /storage/emulated/0/Documents/ on the device
		public static string ReportsLocation = "sdcard\\Documents\\";
		// Keep archive in a Documents/Archive subfolder so fetched reports are moved out of the main Documents listing
		public static string ArchiveLocation = "sdcard\\Documents\\Archive\\";
		public static string OutputLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\AemulusXRReporting\\";
		private static string? _adbExeLocation = Path.Combine(Application.StartupPath, "platform-tools", "adb.exe");
		public static string ADBEXELocation
		{
			get
			{
				if (_adbExeLocation == null)
				{
					_adbExeLocation = Path.Combine(Application.StartupPath, "platform-tools", "adb.exe");
					// Use both Debug.WriteLine and logger to ensure we see the output
					Debug.WriteLine($"Application.StartupPath: {Application.StartupPath}");
					Debug.WriteLine($"Full ADB.exe path: {_adbExeLocation}");
					Debug.WriteLine($"ADB.exe exists: {File.Exists(_adbExeLocation)}");
					_logger.Debug($"Application.StartupPath: {Application.StartupPath}");
					_logger.Debug($"Full ADB.exe path: {_adbExeLocation}");
					_logger.Debug($"ADB.exe exists: {File.Exists(_adbExeLocation)}");
				}
				return _adbExeLocation;
			}
			set
			{
				var oldPath = _adbExeLocation;
				// Always ensure the path includes 'src' if needed
				if (value?.Contains("\\src\\") == false && value?.Contains("\\AMXR_Report\\bin\\") == true)
				{
					// Fix the path by inserting 'src'
					var parts = value.Split(new[] { "\\AMXR_Report\\bin\\" }, StringSplitOptions.None);
					if (parts.Length == 2)
					{
						_adbExeLocation = Path.Combine(parts[0], "AMXR_Report", "src", "bin", parts[1]);
					}
					else
					{
						_adbExeLocation = value;
					}
				}
				else
				{
					_adbExeLocation = value;
				}
				Debug.WriteLine($"ADBEXELocation changed from: {oldPath} to: {_adbExeLocation}");
				Debug.WriteLine($"New path exists: {File.Exists(_adbExeLocation)}");
				_logger.Debug($"ADBEXELocation changed from: {oldPath} to: {_adbExeLocation}");
				_logger.Debug($"New path exists: {File.Exists(_adbExeLocation)}");
			}
		}
	}
}
