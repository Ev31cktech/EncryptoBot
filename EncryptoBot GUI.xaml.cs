using EncryptoBot.UITabs;
using System;
using System.Collections.Generic;
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
		private static EncryptoBotMind botMind;
		const int port = 45031;
		List<RLBotDotNet.Bot> botList = new List<RLBotDotNet.Bot>();
		public EncryptoBotGui()
		{

			InitializeComponent();
			RLBotDotNet.BotManager<EncryptoAgent> botManager = new RLBotDotNet.BotManager<EncryptoAgent>(0);
			consoleWriter = new ControlWriter(ConsolePNL);
			Console.SetOut(consoleWriter);
			botManagerThread = new Thread(() => botManager.Start(port));
			botManagerThread.Start();
		}
<<<<<<< HEAD

		private void Button_Click(object sender, RoutedEventArgs e)
		{

=======
		public static void AddBot(RLBotDotNet.Bot bot)
		{
			botMind.BotList.Add(bot);
>>>>>>> bb0b8c3c1684fe2e23e66a12b0e8e63f2be3239f
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
