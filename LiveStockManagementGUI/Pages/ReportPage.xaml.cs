namespace LiveStockManagementGUI.Pages;

public partial class ReportPage : ContentPage
{
	MainViewModel vm;
	public ReportPage(MainViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
		LivestockPicker.ItemsSource = new string[] { "Cow", "Sheep" };
		GenStatsLabel.Text = vm.GetGeneralStats();
	}

    private void OnLivestockTypeSelectionChange(object sender, EventArgs e)
    {
		var Picker = (Picker)sender;
		int selectedIndex = Picker.SelectedIndex;
		if(selectedIndex == -1) return;
		string type = (string)Picker.ItemsSource[selectedIndex];
		ResultLabel.Text = vm.QueryByLivestockType(type);
    }
}