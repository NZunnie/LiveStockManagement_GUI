using LivestockManagement;
using static LivestockManagement.Livestock;

namespace LiveStockManagementGUI.Models;

public class Database
{
    private readonly SQLiteConnection _connection; //SQLiteConnection 객체를 저장
    public Database()
    {
        string dbName = "FarmDataOriginal.db";
        string dbPath = Path.Combine(Current.AppDataDirectory, dbName);
        if (!File.Exists(dbPath))
        {
            //open the db file
            using Stream stream = Current.OpenAppPackageFileAsync(dbPath).Result;
            using MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            //write db data to app directory
            File.WriteAllBytes(dbPath, memoryStream.ToArray());
        }
        _connection = new SQLiteConnection(dbPath);
        _connection.CreateTables<Cow, Sheep>(); // create if not exist
    }
    public List<Livestock> ReadItems()
    {
        var livestock = new List<Livestock>();
        var lst1 = _connection.Table<Cow>().ToList();
        livestock.AddRange(lst1);
        var lst2 = _connection.Table<Sheep>().ToList();
        livestock.AddRange(lst2);
        return livestock;
    }
    public int InsertItem(Livestock item) //insert super class "Store" , insert Restaurant or Salon
    {
        return _connection.Insert(item);
    }
    public int DeleteItem(Livestock item)
    {
        return _connection.Delete(item);
    }
    public int UpdateItem(Livestock item)
    {
        return _connection.Update(item);
    }
}
