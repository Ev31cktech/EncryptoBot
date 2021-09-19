using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace EncryptoBot
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class EncryptoBotGui : Window
	{
		Thread botManagerThread;
		private ControlWriter consoleWriter;
		const int port = 45031;
		Properties.Settings settings = new Properties.Settings();
		public EncryptoBotGui()
		{
			InitializeComponent();
			Height = settings.WindowHeight;
			Width = settings.WindowWidth;
			WindowState = (WindowState) settings.WindowState;

			RLBotDotNet.BotManager<EncryptoAgent> botManager = new RLBotDotNet.BotManager<EncryptoAgent>(0);
			botManagerThread = new Thread(() => botManager.Start(port));
			consoleWriter = new ControlWriter(ConsolePNL.ConsoleTBK);
			Console.SetOut(consoleWriter);
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			settings.WindowHeight = Height;
			settings.WindowWidth = Width;
			settings.WindowState = (int)WindowState;

			settings.Save();
		}
	}
	class ControlWriter : TextWriter
	{
		public TextBlock textBlock { get; private set; }
		public override Encoding Encoding { get { return Encoding.ASCII; } }

		public ControlWriter(TextBlock _control)
		{
			textBlock = _control;
		}
		public override void Write(char value)
		{
			textBlock.Text += value;
		}

		public override void Write(string value)
		{
			textBlock.Text += value;
		}
	}
}
