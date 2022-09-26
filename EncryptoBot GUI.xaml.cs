using RLBotDotNet;
using System;
using System.Threading;
using System.Windows;

namespace EncryptoBot
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class EncryptoBotGui : Window
	{
		Thread botManagerThread;
		private static EncryptoBotMind botMind;
		const int port = 45031;
		public EncryptoBotGui()
		{
			botMind = new EncryptoBotMind();
			InitializeComponent();
			Debugger.Initialize(ConsolePNL.ConsoleLBX);
			BotManager<EncryptoAgent> botManager = new BotManager<EncryptoAgent>(0);
			try
			{
				botManagerThread = new Thread(() => botManager.Start(port));
				botManagerThread.Start();
			}
			catch (Exception e)
			{
				Debugger.Log("{0}",e);
			}
		}
		public static EncryptoBotMind AddBot(EncryptoAgent bot){
			botMind.AddBot(bot);
			return botMind;
		}
	}
}
