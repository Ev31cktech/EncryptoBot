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
		private ControlWriter consoleWriter = new ControlWriter();
		const int port = 45031;
		public EncryptoBotGui()
		{
			InitializeComponent();
			RLBotDotNet.BotManager<EncryptoAgent> botManager = new RLBotDotNet.BotManager<EncryptoAgent>(0);
			botManagerThread = new Thread(() => botManager.Start(port));
			Console.SetOut(consoleWriter);
		}
	}
	class ControlWriter : TextWriter
	{
		public Control control { get; private set;}
		public override Encoding Encoding{get { return Encoding.ASCII; }}

		public ControlWriter(Control _control)
		{
			control = _control;
		}

	}
}
