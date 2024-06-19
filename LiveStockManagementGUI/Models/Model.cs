namespace LivestockManagement;

public class Livestock
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; } //primary Key 
    public virtual string ?Name { get; set; }
    public string ?Colour { get; set; }
    public virtual double Milk { get; set; }

    public double Weight { get; set; }
    public double Cost { get; set; }
   

    public override string ToString() // used if there is instance of Livestock class 
    {
        //GetType().Name : either Cow or  Sheep
        
        return $"{this.GetType().Name,-15} {this.Id,-10}{this.Colour,-10} {this.Milk,-10} {this.Weight,-10} {this.Cost,-10}";
    }
}
    [Table("Cow")] //Cow from the data table, feature fo mapped, table name
    public class Cow : Livestock
    {

        public Cow()
        {
            Name = "Cow";  // Name 
        }
    public int NumCows { get; set; }
        public override string ToString()
        {
            return base.ToString() + $"{this.NumCows}"; // subclass of ToString (up)
        }
    }
    [Table("Sheep")]
    public class Sheep : Livestock
    {
        public Sheep()
        {
            Name = "Sheep";  // Name 속성 설정
        }
        public int NumSheeps {  get; set; }
        // Wool property to hold the wool value
        public double Wool { get; set; }

        // Override the Milk property to return the Wool value
        public override double Milk
        {
            get => Wool;
            set => Wool = value; // Allow setting Milk to set Wool
        }

    public override string ToString()
        {
            return base.ToString() + $"{this.NumSheeps}";
        }
    }

