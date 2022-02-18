using System.Windows.Controls;

namespace EncryptoBot.UITabs
{
	/// <summary>
	/// Interaction logic for HomePanel.xaml
	/// </summary>

	///TODO
	///tabel:
	///BotID BotName	 State Move 
	public partial class HomePanel : TabItem
	{
		public new string Header { get; }
		public HomePanel()
		{
			InitializeComponent();
		}
	}
}
