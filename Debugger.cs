using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace EncryptoBot
{
	static class Debugger
	{
		public enum Severity { Info, Warning, Error, Debug }
		private static ListBox ConsoleLBX;
		public static void Initialize(ListBox clbx)
		{
			ConsoleLBX = clbx;
		}
		public static void Log(String pattern, Severity severity, params object[] objects)
		{
			List<object> paramList = new List<object>() { severity.ToString(), DateTime.Now.ToString("hh:mm:ss") };
			paramList.AddRange(objects);
			ConsoleLBX.Items.Add(String.Format($"{0}[{1}]" + pattern, paramList));
		}
		public static void Log(String pattern, params object[] objects)
		{ Log(pattern, Severity.Info, objects); }

		public static void Info(String pattern, params object[] objects)
		{ Log(pattern, Severity.Info, objects); }

		public static void Debug(String pattern, params object[] objects)
		{ Log(pattern, Severity.Debug, objects); }

		public static void Error(String pattern, params object[] objects)
		{ Log(pattern, Severity.Error, objects); }

		public static void Warn(String pattern, params object[] objects)
		{ Log(pattern, Severity.Warning, objects); }

		public static void Log(String pattern)
		{ Log(pattern, Severity.Info, new object[] { }); }

		public static void Info(String pattern)
		{ Log(pattern, Severity.Info, new object[] { }); }

		public static void Debug(String pattern)
		{ Log(pattern, Severity.Debug, new object[] { }); }

		public static void Error(String pattern)
		{ Log(pattern, Severity.Error, new object[] { }); }

		public static void Warn(String pattern)
		{ Log(pattern, Severity.Warning, new object[] { }); }
	}
}
