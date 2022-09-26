using System.Windows.Controls;

namespace EncryptoBot.UITabs
{
	/// <summary>
	/// Interaction logic for ConsolePanel.xaml
	/// </summary>
	public partial class ConsolePanel : TabItem
	{
		bool ScrollViewToBottom = true;
		public ConsolePanel()
		{
			InitializeComponent();
		}

		private void ScrollView_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (e.VerticalChange > 0)
			{
				ScrollViewToBottom = e.VerticalOffset + e.ViewportHeight == e.ExtentHeight;
			}
			else if(ScrollViewToBottom)
			{
				((ScrollViewer)sender).ScrollToBottom();
			}
		}
	}
}
