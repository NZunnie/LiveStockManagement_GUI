namespace LiveStockManagementGUI.Pages;

public partial class HomePage : ContentPage
{

    public HomePage()
    {
        InitializeComponent();
    }

    private void DataListButton_Clicked(object sender, EventArgs e)
    {
        // Handle the Data List button click event here navigate to the Data List page
        Shell.Current.GoToAsync("//DataPage");
    }

    private void ReportButton_Clicked(object sender, EventArgs e)
    {
        // navigate to the Report page
        Shell.Current.GoToAsync("//ReportPage");
    }

    private void SearchButton_Clicked(object sender, EventArgs e)
    {
        // navigate to the Search page
        Shell.Current.GoToAsync("//SearchPage");
    }

    private void ManagementButton_Clicked(object sender, EventArgs e)
    {
        // navigate to the Management page
        Shell.Current.GoToAsync("//ManagementPage");
    }






 //   private MainViewModel vm;
 //   public HomePage()
	//{
	//	InitializeComponent();
 //       vm = new MainViewModel();
      
 //       BindingContext = vm;
 //   }

 //   private async void DataListButton_Clicked(object sender, EventArgs e)
 //   {
 //       await Navigation.PushAsync(new DataPage(vm));
 //   }

 //   private async void ReportButton_Clicked(object sender, EventArgs e)
 //   {
 //       await Navigation.PushAsync(new ReportPage(vm));
 //   }

 //   private async void SearchButton_Clicked(object sender, EventArgs e)
 //   {
 //       await Navigation.PushAsync(new SearchPage(vm));

 //   }

 //   private async void ManagementButton_Clicked(object sender, EventArgs e)
 //   {
 //       await Navigation.PushAsync(new ManagementPage(vm));
 //   }
}