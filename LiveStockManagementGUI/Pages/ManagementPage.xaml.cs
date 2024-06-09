using LivestockManagement;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace LiveStockManagementGUI.Pages
{

    public partial class ManagementPage : ContentPage
    {
        private Database _database;
        
        MainViewModel vm;

        public ManagementPage(MainViewModel vm)
        {
            InitializeComponent();


            this.vm = vm;
            BindingContext = vm;

            _database = new Database(); // Initialize  database here
            LivestockPicker.ItemsSource = new string[] { "Insert", "Update", "Delete" };

           
        }

        private void OnLivestockTypeSelectionChange(object sender, EventArgs e)
        {
            var selectedOption = LivestockPicker.SelectedItem.ToString();

            // Hide both layouts initially
            InsertLayout.IsVisible = false;
            UpdateLayout.IsVisible = false;
            DeleteLayout.IsVisible = false;

            // Show the appropriate layout based on the selection
            if (selectedOption == "Insert")
            {
                InsertLayout.IsVisible = true;
            }
            else if (selectedOption == "Update")
            {
                UpdateLayout.IsVisible = true;
            }
            else if (selectedOption == "Delete")
            {
                DeleteLayout.IsVisible = true;
            }


        }

        private void InsertBtn_Click(object sender, EventArgs e)
        {
            // Implement the logic for inserting a livestock record
            string type = LivestockType.SelectedItem?.ToString();
            string colour = LivestockColour.SelectedItem?.ToString();
            var livestock = new Livestock();
             
            if (!double.TryParse(Cost.Text, out double cost))
            {
                DisplayAlert("Invalid Input", "Please enter a valid cost", "OK");
                return;
            }

            if (!double.TryParse(Weight.Text, out double weight))
            {
                DisplayAlert("Invalid Input", "Please enter a valid weight", "OK");
                return;
            }

            if (!double.TryParse(Milk.Text, out double milk))
            {
                DisplayAlert("Invalid Input", "Please enter a valid milk value", "OK");
                return;
            }

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
                    DisplayAlert("Success", $" New Record Added inserted successfully. \n" +
                                       $"Name: {type}\n" +
                                       $"Colour: {colour}\n" +
                                       $"Milk: {milk}\n" +
                                       $"Weight: {weight}\n" +
                                       $"Cost: {cost}", "OK");
                    vm.Livestocks.Add(livestock);

                }
                else
                {
                    DisplayAlert("Failure", "Failed to add record", "OK");
                }
            }
           
        }
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            // Ensure the Id is valid
            if (int.TryParse(UpdateLivestockId.Text, out int Id))
            {
                // Find the existing livestock record
                Livestock existingLivestock = vm.Livestocks.FirstOrDefault(l => l.Id == Id);

                if (existingLivestock == null)
                {
                    DisplayAlert("Failure", "Livestock with the specified Id does not exist", "OK");
                    return;
                }

                // Validate each field separately and provide specific feedback
                string colour = LivestockColour1.SelectedItem?.ToString();

                if (!double.TryParse(Cost1.Text, out double cost))
                {
                    DisplayAlert("Invalid Input", "Please enter a valid cost", "OK");
                    return;
                }

                if (!double.TryParse(Weight1.Text, out double weight))
                {
                    DisplayAlert("Invalid Input", "Please enter a valid weight", "OK");
                    return;
                }

                if (!double.TryParse(Milk1.Text, out double milk))
                {
                    DisplayAlert("Invalid Input", "Please enter a valid milk value", "OK");
                    return;
                }

                // Update livestock details
                existingLivestock.Colour = colour;
                existingLivestock.Cost = cost;
                existingLivestock.Weight = weight;
                existingLivestock.Milk = milk;

                // Update the record in the database
                var update = _database.UpdateItem(existingLivestock);
                if (update > 0)
                {
                    DisplayAlert("Success", "Record updated successfully", "OK");

                    // Optionally refresh the list view or notify the UI of changes
                    var index = vm.Livestocks.IndexOf(existingLivestock);
                    vm.Livestocks[index] = existingLivestock;
                }
                else
                {
                    DisplayAlert("Failure", "Failed to update record", "OK");
                }
            }
            else
            {
                DisplayAlert("Invalid Input", "Please enter a valid livestock ID", "OK");
            }
        }


        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            // deleting a livestock record
            if (int.TryParse(DeleteLivestockId.Text, out int livestockId))
            {
                Livestock itemToRemove = vm.Livestocks.FirstOrDefault(l => l.Id == livestockId);
                

                if (itemToRemove == null)
                {
                    DisplayAlert("Failure", $"Non-existent livestock Id", "OK");
                    return;
                }
                
                
            
                var deleted = _database.DeleteItem(itemToRemove);
                if (deleted > 0)
                {

                    bool removeFromList = vm.Livestocks.Remove(itemToRemove);
                    if (removeFromList)
                    {

                        DisplayAlert("Success", $"Record with Id no. {livestockId} deleted successfully.\n" +
                                       $"Name: {itemToRemove.Name}\n" +
                                       $"Colour: {itemToRemove.Colour}\n" +
                                       $"Milk: {itemToRemove.Milk}\n" +
                                       $"Weight: {itemToRemove.Weight}\n" +
                                       $"Cost: {itemToRemove.Cost}", "OK");
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
           
            LivestockType.SelectedIndex = -1;
            Cost.Text = string.Empty;
            Weight.Text = string.Empty;
            LivestockColour.SelectedIndex = -1;
            Milk.Text = string.Empty;
            DeleteLivestockId.Text = string.Empty;
            UpdateLivestockId.Text = string.Empty;
            InsertLayout.IsVisible = false;
            UpdateLayout.IsVisible = false;
            DeleteLayout.IsVisible = false;
            Cost1.Text = string.Empty;
            Milk1.Text = string.Empty;
            Weight1.Text = string.Empty;
            LivestockColour1.SelectedIndex = -1;
            
        }
    }
}