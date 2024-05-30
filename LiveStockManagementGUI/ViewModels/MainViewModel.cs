using LivestockManagement;
using static Microsoft.Maui.Controls.Device;

namespace LiveStockManagementGUI.ViewModels;
//use dependency injection (DI) to make this view model available throught the app
public class MainViewModel
{
    public ObservableCollection<Livestock> Livestocks { get; set; }
    public readonly Database _database;
    public MainViewModel()
    {
        _database = new();
        Livestocks = new();
        _database.ReadItems().ForEach(x => Livestocks.Add(x));
    }

    // CowMilkPrice $ per kg
    private const double CowMilkPrice = 9.4;
    // SheepWoolPrice $ per kg
    private const double SheepWoolPrice = 6.2;
    // Tax $ per kg, per day
    private const double GovernmentTax = 0.02;

    #region Statistic -Report page
    public string GetGeneralStats()
    {
        return $"Total Weight of Livestock: {Livestocks.Sum(x => x.Weight)}";
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
    public string AveWeight()
    {
        return $"Average weight of all livestocks: {Livestocks.Average(x => x.Weight):F2} kg";
    }

    //Display current profit
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
    //Based on current livestock data
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
    #region InvestmentForecast  -Report Page
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

        return $"Buying {quantity} {type}s would bring in estimated daily profit: ${estimatedDailyProfit:F2}";

    }
        #endregion

        public string QueryByLivestockType(string type)
    {
        List<Livestock> sts = Livestocks.Where(x => x.GetType().Name.Equals(type)).ToList();
        string results = $"{$"Number of {type}:",-30}{sts.Count}\n"; // first line of result
        results += $"{"Average weight of livestock:",-30}{sts.Average(x => x.Weight)}";
        return results;
    }

    //public string InsertByLivestockType(string type)
    //{
    //    List<Livestock> sts = Livestocks.Where(x => x.GetType().Name.Equals(type)).ToList();
    //    string results = $"{$" {type} inserted:",-30}{sts.Count}\n"; // first line of result
    //    //results += $"{"Average weight of livestock:",-30}{sts.Average(x => x.Weight)}";
    //    return results;
    //}


    //  public void DeleteById(int id)
    //{
    //    Livestock? livestock = Livestocks.FirstOrDefault(s => s.Id == id);
    //    if (livestock != null)
    //    {
    //        if(_database.DeleteItem(id) >0)
    //            Livestocks.Remove(livestock);
    //    }
    //}

}
