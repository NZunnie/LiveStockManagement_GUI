namespace LiveStockManagementGUI.Pages;

public partial class HomePage : ContentPage
{

    public HomePage()
    {
        InitializeComponent();
    }

    #region Handle to navigate to each page
    private void DataListButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//DataPage");
    }

    private void ReportButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ReportPage");
    }

    private void SearchButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//SearchPage");
    }

    private void ManagementButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ManagementPage");
    }
    #endregion
}