namespace LiveStockManagementGUI.Pages;

public partial class ReportPage : ContentPage
{
	MainViewModel vm;
	public ReportPage(MainViewModel vm)
	{
		InitializeComponent();
        vm = new MainViewModel();

        this.vm = vm;
        BindingContext = vm;

        //General 
        MonthTax.Text = vm.MonthlyTax();
		DayProfit.Text = vm.DailyProfit();
		AveAllWeight.Text = vm.AveWeight();
        
        // Set the ItemsSource for the Picker
        LivestockPicker.ItemsSource = new string[] { "Cow", "Sheep" };

        TotalCow.Text = vm.TotalCowProfit();
        AveCow.Text = vm.AveCowProfit();
        TotalSheep.Text = vm.TotalSheepProfit();
        AveSheep.Text = vm.AveSheepProfit();
        
            
    }

    private void OnLivestockTypeSelectionChange(object sender, EventArgs e)
    {
		var Picker = (Picker)sender;
		int selectedIndex = Picker.SelectedIndex;
		if(selectedIndex == -1) return;
		string type = (string)Picker.ItemsSource[selectedIndex];
        if (int.TryParse(type, out int quantity))
        {
            EstimateInvestment.Text = vm.EstimateInvestment(type, quantity);
        }
    }

    private void NumberEntryBtn_click(object sender, EventArgs e)
    {
        if (!int.TryParse(Quentity.Text, out int quantity))
        {
            DisplayAlert("Error", "Invalid quantity input", "OK");
            return;
        }
        string type = (string)LivestockPicker.ItemsSource[LivestockPicker.SelectedIndex];
        EstimateInvestment.Text = vm.EstimateInvestment(type, quantity);
    }

    private void ResetClicked(object sender, EventArgs e)
    {
        Quentity.Text = string.Empty;
        EstimateInvestment.Text = string.Empty;
        LivestockPicker.SelectedIndex = -1;
    }
}