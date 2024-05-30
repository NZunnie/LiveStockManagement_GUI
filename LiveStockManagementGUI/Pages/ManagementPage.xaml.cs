using LivestockManagement;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace LiveStockManagementGUI.Pages
{

    public partial class ManagementPage : ContentPage
    {
        private Database _database;
        private ObservableCollection<Livestock> _livestock;

        public ManagementPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            _database = new Database(); // Initialize  database here
            _livestock = new ObservableCollection<Livestock>(); // Initialize your livestock collection here
            LivestockPicker.ItemsSource = new string[] { "Insert", "Update", "Delete" };
           
        }

        private void OnLivestockTypeSelectionChange(object sender, EventArgs e)
        {
            var selectedOption = LivestockPicker.SelectedItem.ToString();

            // Hide both layouts initially
            InsertLayout.IsVisible = false;
            DeleteLayout.IsVisible = false;

            // Show the appropriate layout based on the selection
            if (selectedOption == "Insert")
            {
                InsertLayout.IsVisible = true;
            }
            else if (selectedOption == "Update")
            {
                DeleteLayout.IsVisible = true;
            }
            else if (selectedOption == "Delete")
            {
                DeleteLayout.IsVisible = true;
            }

        }

        //private void LivestockBtn_click(object sender, EventArgs e)
        //{
        //    if (!int.TryParse(LivestockType.Text, out int num))
        //    {
        //        DisplayAlert("Error", "Invalid input", "OK");
        //        return;
        //    }
        //    if (!int.TryParse(Cost.Text, out int num1))
        //    {
        //        DisplayAlert("Error", "Invalid input", "OK");
        //        return;
        //    }
        //}
        private void InsertBtn_Click(object sender, EventArgs e)
        {
            // Implement the logic for inserting a livestock record
            string type = LivestockType.SelectedItem?.ToString();
            string colour = LivestockColour.SelectedItem?.ToString();
            var livestock = new Livestock();
            if (double.TryParse(Cost.Text, out double cost) &&
                double.TryParse(Weight.Text, out double weight) &&
                double.TryParse(Milk.Text, out double milk))
               // double.TryParse(Wool.Text, out double wool))
            { 
                if (type == "Cow")
                {
                    livestock = new Cow
                    {
                        Name = type,
                        Colour = colour,
                        Cost = cost,
                        Weight = weight,
                        Milk = milk

                    };
                   
                }
                else
                {
                     livestock = new Sheep
                    {
                        Name = type,
                        Colour = colour,
                        Cost = cost,
                        Weight = weight,
                        Milk = milk

                    };

                }
               
                // Insert into the database
                var inserted = _database.InsertItem(livestock);
                if (inserted > 0)
                {
                    DisplayAlert("Success", "Record added successfully", "OK");
                    _livestock.Add(livestock);
                }
                else
                {
                    DisplayAlert("Failure", "Failed to add record", "OK");
                }
            }
            else
            {
                DisplayAlert("Invalid Input", "Please enter valid numbers for cost, weight, and milk", "OK");
            }
        }
        //private void UpdateBtn_Click(object sender, EventArgs e)
        //{
        //    // Implement the logic for updating a livestock record
        //    int Id = 0;
        //    string colour = Colour.Text;
        //    if (double.TryParse(Cost.Text, out double cost) &&
        //        double.TryParse(Weight.Text, out double weight) &&
        //        double.TryParse(Milk.Text, out double milk))
        //    {
        //        var livestock = new Livestock
        //        {
                    
        //            Colour = colour,
        //            Cost = cost,
        //            Weight = weight,
        //            Milk = milk

        //        };

        //        // Insert into the database
        //        var update = _database.UpdateItem(livestock);
        //        if (update > 0)
        //        {
        //            DisplayAlert("Success", "Record added successfully", "OK");
        //            _livestock.Add(livestock);
        //        }
        //        else
        //        {
        //            DisplayAlert("Failure", "Failed to add record", "OK");
        //        }
        //    }
        //    else
        //    {
        //        DisplayAlert("Invalid Input", "Please enter valid numbers for cost, weight, and milk", "OK");
        //    }
        //}

        private void DeleteBtn_Click(object sender, EventArgs e)
        {   
           // Implement the logic for deleting a livestock record
            if (int.TryParse(DeleteLivestockId.Text, out int livestockId))
                {
                    var deleted = _database.DeleteItem(livestockId);
                    if (deleted > 0)
                    {
                        DisplayAlert("Success", "Record deleted successfully", "OK");
                        var itemToRemove = _livestock.FirstOrDefault(l => l.Id == livestockId);
                        if (itemToRemove != null)
                        {
                            _livestock.Remove(itemToRemove);
                        }
                    }
                    else
                    {
                        DisplayAlert("Failure", "Failed to delete record", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Invalid Input", "Please enter a valid livestock ID", "OK");
                }
        }


        private void ResetBtn_click(object sender, EventArgs e)
        {
            //LivestockType.Text = string.Empty;
            //Cost.Text = string.Empty;

            //LivestockPicker.SelectedIndex = -1;
            LivestockType.SelectedIndex = -1;
            Cost.Text = string.Empty;
            Weight.Text = string.Empty;
            LivestockColour.SelectedIndex = -1;
            Milk.Text = string.Empty;
            DeleteLivestockId.Text = string.Empty;
            //InsertLayout.IsVisible = false;
            ////UpdateLayout.IsVisible = false;
            //DeleteLayout.IsVisible = false;
        }
    }
}