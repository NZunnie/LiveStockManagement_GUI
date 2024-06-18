namespace LiveStockManagementGUI.Pages;

public partial class SearchPage : ContentPage
{
    private MainViewModel vm;
    public SearchPage(MainViewModel vm)
    {

        InitializeComponent();

        vm = new MainViewModel();
        this.vm = vm;
        BindingContext = vm;

        // Set the ItemsSource for the Picker
        LivestockPicker.ItemsSource = new string[] { "Cow", "Sheep" };
        LivestockColourPicker.ItemsSource = new string[] { "Black", "Red", "White", "All" };


    }

    


    //private void OnLivestockTypeSelectionChange(object sender, EventArgs e)
    //{
    //    var Picker = (Picker)sender;
    //    int selectedIndex = Picker.SelectedIndex;
    //    if (selectedIndex == -1) return;
    //    string type = (string)Picker.ItemsSource[selectedIndex];
    //    //if (int.TryParse(type, out int quantity))
    //    //{
    //    //    //EstimateInvestment.Text = vm.EstimateInvestment(type, quantity);
    //    //}
    //}

    private void OnLivestockTypeSelectionChange(object sender, EventArgs e)
    {
        if (LivestockPicker.SelectedIndex == -1 || LivestockColourPicker.SelectedIndex == -1)
        {
            return;
        }

        var selectedType = LivestockPicker.SelectedItem.ToString();
        var selectedColor = LivestockColourPicker.SelectedItem.ToString();
        var selectedLivestock = vm.GetFilteredLivestock(selectedType, selectedColor);

    }

    private void SearchBtn_click(object sender, EventArgs e)
    {
        if (LivestockPicker.SelectedIndex == -1 || LivestockColourPicker.SelectedIndex == -1)
        {
            DisplayAlert("Error", "Please select a livestock type and color", "OK");
            return;
        }

        var selectedType = LivestockPicker.SelectedItem.ToString();
        var selectedColor = LivestockColourPicker.SelectedItem.ToString();
        var selectedLivestock = vm.GetFilteredLivestock(selectedType, selectedColor);

        if (selectedLivestock.Count == 0)
        {
            DisplayAlert("No Results", "No livestock found with the selected type and color. Please try again.", "OK");
            return;
        }
        var result = vm.GetLivestockSearch(selectedLivestock);

        SearchResult.Text = result;
    }


    private void ResetClicked(object sender, EventArgs e)
    {
        LivestockPicker.SelectedIndex = -1;
        LivestockColourPicker.SelectedIndex = -1;
        SearchResult.Text = string.Empty;
    }
}
