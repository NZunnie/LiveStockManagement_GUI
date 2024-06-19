namespace LiveStockManagementGUI.Pages;

public partial class SearchPage : ContentPage
{
    private MainViewModel vm;
    public SearchPage(MainViewModel vm)
    {
        InitializeComponent();

        this.vm = vm;
        BindingContext = vm;

        // Set the ItemsSource for the Picker
        LivestockPicker.ItemsSource = new string[] { "Cow", "Sheep" };
        LivestockColourPicker.ItemsSource = new string[] { "Black", "Red", "White", "All" };
    }

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
    #region Search Button
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

        var resultLines = result.Split('\n');
        TotalCountLabel.Text = resultLines[0];
        PercentageLabel.Text = resultLines[1];
        DailyTaxLabel.Text = resultLines[2];
        ProfitLabel.Text = resultLines[3];
        AverageWeightLabel.Text = resultLines[4];
        TotalProduceLabel.Text = resultLines[5];
    }
    #endregion
    #region Reset Button
    private void ResetClicked(object sender, EventArgs e)
    {
        LivestockPicker.SelectedIndex = -1;
        LivestockColourPicker.SelectedIndex = -1;

        TotalCountLabel.Text = string.Empty;
        PercentageLabel.Text = string.Empty;
        DailyTaxLabel.Text = string.Empty;
        ProfitLabel.Text = string.Empty;
        AverageWeightLabel.Text = string.Empty;
        TotalProduceLabel.Text = string.Empty;
    }
    #endregion
}
