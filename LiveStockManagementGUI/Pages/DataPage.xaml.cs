namespace LiveStockManagementGUI.Pages;

public partial class DataPage : ContentPage
{
	public DataPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}