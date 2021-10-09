using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
