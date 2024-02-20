namespace VehicleVilla.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public int User { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }

        public VehicleModel(int id, int user, string make, string model, int year, string color, float price)
        {
            Id = id;
            User = user;
            Make = make;
            Model = model;
            Year = year;
            Color = color;
            Price = price;
        }

        public VehicleModel() { }
    }
}
