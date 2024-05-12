using LivestockManagement;

namespace LiveStockManagementGUI.ViewModels;
//use dependency injection (DI) to make this view model available thruout the app
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
    //public string GetGeneralStats()
    //{
    //    return $"Total staff: {Livestocks.Sum(x => x.NumStaff)}";
    //}

    //public string QueryByStoreType(string type)
    //{
    //    List<Livestock> sts = Livestocks.Where(x => x.GetType().Name.Equals(type)).ToList();
    //    string results = $"{$"Number of {type}:",-30}{sts.Count}\n"; // first line of result
    //    results += $"{"Average number of staff:",-30}{sts.Average(x => x.NumStaff)}";
    //    return results;
    //}

    

}
