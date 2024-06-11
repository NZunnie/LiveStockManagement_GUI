using LivestockManagement;
using System.IO;
using System.Threading.Tasks;
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
            using Stream stream = Current.OpenAppPackageFileAsync(dbName).Result;
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
        var livestocks = new List<Livestock>();
        var lst1 = _connection.Table<Cow>().ToList();
        livestocks.AddRange(lst1);
        var lst2 = _connection.Table<Sheep>().ToList();
        livestocks.AddRange(lst2);
        return livestocks;
    }
    public async Task<int> InsertItemAsync(Livestock item)
    /*public int InsertItem(Livestock item)*/ //insert super class "Livestock" , insert Cow or Sheep
    {
        return await Task.Run(() => _connection.Insert(item));
        //return _connection.Insert(item);
    }
    public async Task<int> DeleteItemAsync(Livestock livestockId)
    //public int DeleteItem(Livestock livestockId)
    {
        return await Task.Run(() => _connection.Delete(livestockId));
        //return _connection.Delete(livestockId);
    }
    public async Task<int> UpdateItemAsync(Livestock item)
    //public int UpdateItem(Livestock item)
    {
        return await Task.Run(() => _connection.Update(item));
        //return _connection.Update(item);
    }
}
