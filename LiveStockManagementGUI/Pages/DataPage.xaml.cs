namespace LiveStockManagementGUI.Pages;

public partial class DataPage : ContentPage
{
	public DataPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm; // this line makes all the binding available which are in the DataPage.xaml
	}
}