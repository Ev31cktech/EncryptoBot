using EncryptoBot.UITabs;
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
		public EncryptoBotGui()
		{
			InitializeComponent();
			RLBotDotNet.BotManager<EncryptoAgent> botManager = new RLBotDotNet.BotManager<EncryptoAgent>(0);
			consoleWriter = new ControlWriter(ConsolePNL);
			Console.SetOut(consoleWriter);
			botManagerThread = new Thread(() => botManager.Start(port));
			botManagerThread.Start();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}
	}
	class ControlWriter : TextWriter
	{
		public ConsolePanel consolePanel { get; private set; }
		public override Encoding Encoding { get { return Encoding.ASCII; } }

		public ControlWriter(ConsolePanel _control)
		{
			consolePanel = _control;
		}
		public override void Write(char value)
		{
			consolePanel.Dispatcher.InvokeAsync(() => UpdateText(value.ToString()));
		}
		public void UpdateText(string value)
		{
			consolePanel.ConsoleTBK.Text += value;
		}

		public override void Write(string value)
		{
			consolePanel.Dispatcher.InvokeAsync(() => UpdateText(value.ToString()));
		}
	}
}
