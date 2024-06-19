using LivestockManagement;

namespace LiveStockManagementGUI.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Livestock> Livestocks { get; set; }
    public readonly Database _database;
   
    // CowMilkPrice $ per kg
    private const double CowMilkPrice = 9.4;
    // SheepWoolPrice $ per kg
    private const double SheepWoolPrice = 6.2;
    // Tax $ per kg, per day
    private const double GovernmentTax = 0.2;

    public MainViewModel()
    {
        _database = new();
        Livestocks = new();
        _database.ReadItems().ForEach(x => Livestocks.Add(x));

    }

    #region General Statistic -Report page 
    public string GetTotalWeight()
    {
        return $"Total Weight of Livestock: {Livestocks.Sum(x => x.Weight)}";
    }
    public string AveWeight()
    {
        return $"Average weight of all livestocks: {Livestocks.Average(x => x.Weight):F2} kg";
    }
    public string MonthlyTax()
    {
        return $"30 days govt tax: ${Livestocks.Sum(x => x.Weight * GovernmentTax * 30):F2}";
        //return $"${Livestocks.Sum(x => x.Weight * GovernmentTax * 30):F2}";
    }
    public string DailyProfit()
    {
        return $"Farm daily profit: ${Livestocks.Sum(x => CalculateDailyProfit(x)):F2}";
    }
    #endregion

    #region Current Statistic -Report page
    public string TotalCowProfit()
    {
        double totalCowProfit = Livestocks.Where(x => x is Cow).Sum(CalculateDailyProfit);
        return $"Current daily profit of all cows: \n${totalCowProfit:F2}";
    }
    public string TotalSheepProfit()
    {
        double totalSheepProfit = Livestocks.Where(x => x is Sheep).Sum(CalculateDailyProfit);
        return $"Current daily profit of all sheep: \n${totalSheepProfit:F2}";
    }
    public string AveCowProfit()
    {
        int totalCowCount = Livestocks.Count(x => x is Cow);
        double totalCowProfit = Livestocks.Where(x => x is Cow).Sum(CalculateDailyProfit);
        double averageCowProfit = totalCowCount > 0 ? totalCowProfit / totalCowCount : 0;
        return $"On average, a single cow makes daily profit: \n${averageCowProfit:F2}";
    }
    public string AveSheepProfit()
    {
        int totalSheepCount = Livestocks.Count(x => x is Sheep);
        double totalSheepProfit = Livestocks.Where(x => x is Sheep).Sum(CalculateDailyProfit);
        double averageSheepProfit = totalSheepCount > 0 ? totalSheepProfit / totalSheepCount : 0;
        return $"On average, a single sheep makes daily profit: \n${averageSheepProfit:F2}";
    }
    #endregion

    #region Daily Profit -Report page / Search Page
    private double CalculateDailyProfit(Livestock livestock)
    {
        double income = livestock switch
        {
            Cow cow => cow.Milk * CowMilkPrice,
            Sheep sheep => sheep.Milk * SheepWoolPrice,
            _ => 0 //default
        };

        double cost = livestock.Cost;
        double tax = livestock.Weight * GovernmentTax;
        double profit = income - cost - tax;
        return profit;
    }
    #endregion

    #region Estimate Investment  -Report Page
    public string EstimateInvestment(string type, int quantity)
    {
        double estimatedDailyProfit;

        if (type.ToLower() == "cow")
        {
            double totalCowMilk = Livestocks.OfType<Cow>().Sum(x => x.Milk);
            double totalCowProfit = totalCowMilk * CowMilkPrice;
            int cowCount = Livestocks.OfType<Cow>().Count();
            double averageCowProfit = cowCount > 0 ? totalCowProfit / cowCount : 0;
            estimatedDailyProfit = averageCowProfit * quantity;
        }
        else if (type.ToLower() == "sheep")
        {
            double totalSheepWool = Livestocks.OfType<Sheep>().Sum(x => x.Milk);
            double totalSheepProfit = totalSheepWool * SheepWoolPrice;
            int sheepCount = Livestocks.OfType<Sheep>().Count();
            double averageSheepProfit = sheepCount > 0 ? totalSheepProfit / sheepCount : 0;
            estimatedDailyProfit = averageSheepProfit * quantity;
        }
        else
        {
            return $"Invalid livestock type: {type}";
        }
        return $"For {quantity} {type}s \nestimated daily profit: \n${estimatedDailyProfit:F2}";
    }
    #endregion

    #region Search by type & color -Search Page
  
    public List<Livestock> GetFilteredLivestock(string type, string color)
    {
        return Livestocks.Where(x => (type == "All" || x.GetType().Name == type) &&
                                      (color == "All" || x.Colour == color)).ToList();
    }

    public string GetLivestockSearch<T>(List<T> selectedLivestock) where T : Livestock
    {
        int totalCount = selectedLivestock.Count;
        int totalCows = Livestocks.Count(x => x is Cow);
        int totalSheep = Livestocks.Count(x => x is Sheep);
        int totalLivestock = totalCows + totalSheep;

        double percentage = totalLivestock > 0 ? (totalCount / (double)totalLivestock) * 100 : 0;
        double totalWeight = selectedLivestock.Sum(x => x.Weight);
        double totalProfit = selectedLivestock.Sum(x => CalculateDailyProfit(x));
        double averageWeight = selectedLivestock.Average(x => x.Weight);
        double totalTax = selectedLivestock.Sum(x => x.Weight * GovernmentTax);
        
        double totalProduce = 0;
        if (selectedLivestock.Count == 0)
        {
            return "No livestock found";
        }

        if (selectedLivestock.All(x => x is Cow))
        {
            totalProduce = selectedLivestock.OfType<Cow>().Sum(x => x.Milk);
        }
        else if (selectedLivestock.All(x => x is Sheep))
        {
            totalProduce = selectedLivestock.OfType<Sheep>().Sum(x => x.Milk);
        }

        return $"Total number of selected livestock: {totalCount}\n" +
               $"Percentage of selected livestock: {percentage:F2}%\n" +
               $"Daily tax of selected livestock: ${totalTax:F2}\n" +
               $"Daily profit: ${totalProfit:F2}\n" +
               $"Average weight: {averageWeight:F2} kg\n" +
               $"Total produce amount: {totalProduce:F2} kg";
    }

    #endregion


    #region

    public async Task<bool> InsertLivestockAsync(Livestock livestock)
    {
        var inserted = await _database.InsertItemAsync(livestock);
        if (inserted > 0)
        {
            Livestocks.Add(livestock);
            return true;
        }
        return false;
    }
    public async Task<bool> UpdateLivestockAsync(int id, string colour, double cost, double weight, double milk)
    {
        var existingLivestock = Livestocks.FirstOrDefault(l => l.Id == id);
        if (existingLivestock == null)
        {
            return false;
        }

        existingLivestock.Colour = colour;
        existingLivestock.Cost = cost;
        existingLivestock.Weight = weight;
        existingLivestock.Milk = milk;

        var updated = await _database.UpdateItemAsync(existingLivestock);
        if (updated > 0)
        {
            var index = Livestocks.IndexOf(existingLivestock);
            Livestocks[index] = existingLivestock;
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteLivestockAsync(int id)
    {
        var livestock = Livestocks.FirstOrDefault(l => l.Id == id);
        if (livestock == null)
        {
            return false;
        }

        var deleted = await _database.DeleteItemAsync(livestock);
        if (deleted > 0)
        {
            Livestocks.Remove(livestock);
            return true;
        }
        return false;
    }

    #endregion


}
