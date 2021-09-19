using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EncryptoBot
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		Properties.Settings settings = new Properties.Settings();
		public App()
		{
			MainWindow = new EncryptoBotGui();
			MainWindow.Closing += Application_Closing;
			MainWindow.Height = settings.WindowHeight;
			MainWindow.Width = settings.WindowWidth;
			MainWindow.Left = settings.WindowLocation.X;
			MainWindow.Top = settings.WindowLocation.Y;
			MainWindow.Show();
			MainWindow.WindowState = (WindowState)settings.WindowState;
		}
		private void Application_Closing(object sender, EventArgs e)
		{
			settings.WindowHeight = MainWindow.Height;
			settings.WindowWidth = MainWindow.Width;
			settings.WindowState = (int)MainWindow.WindowState;
			settings.WindowLocation = new Point(MainWindow.Left, MainWindow.Top);
			settings.Save();
			Environment.Exit(1);
		}
	}
}
