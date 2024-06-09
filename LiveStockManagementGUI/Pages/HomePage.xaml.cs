namespace LiveStockManagementGUI.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    //private async void DataListButton_Clicked(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new DataListPage());
    //}
    private async void SearchButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SearchPage());
    }
}