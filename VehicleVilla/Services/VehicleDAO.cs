using MySqlConnector;
using VehicleVilla.Models;

namespace VehicleVilla.Services
{
    public class VehicleDAO : IVehicleDataService
    {
        string connectionString = "Server=localhost;User ID=root;Password=root;port=3306;Database=vehiclevilla";

        public int AddVehicle(VehicleModel vehicle)
        {
            int newIdNumber = -1;

            try
            {

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO vehicles (Id, User, Make, Model, Year, Color, Price) VALUES (@Id,@User,@Make,@Model,@Year,@Color,@Price)", connection);
                    cmd.Parameters.AddWithValue("@Id", vehicle.Id);
                    cmd.Parameters.AddWithValue("@User", vehicle.User);
                    cmd.Parameters.AddWithValue("@Make", vehicle.Make);
                    cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                    cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                    cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                    cmd.Parameters.AddWithValue("@Price", vehicle.Price);
                    

                    newIdNumber = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return newIdNumber;
        }

        public List<VehicleModel> AllVehicles()
        {
            List<VehicleModel> foundVehicles = new List<VehicleModel>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM vehicles", connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        VehicleModel vehicle = new VehicleModel();
                        vehicle.Id = Convert.ToInt32(reader["Id"]);
                        vehicle.User = Convert.ToInt32(reader["User"]);
                        vehicle.Make = reader["Make"].ToString();
                        vehicle.Model = reader["Model"].ToString();
                        vehicle.Year = Convert.ToInt32(reader["Year"]);
                        vehicle.Color = reader["Color"].ToString();
                        vehicle.Price = (float) Convert.ToDouble(reader["Price"]);
                        foundVehicles.Add(vehicle);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return foundVehicles;
        }

        public int DeleteVehicle(VehicleModel vehicle)
        {
            int deleteId = -1;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM vehicles WHERE Id = @Id", connection);
                    cmd.Parameters.AddWithValue("@Id", vehicle.Id);
                    deleteId = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return deleteId;
        }

        public VehicleModel GetVehicleById(int id)
        {
            VehicleModel foundVehicle = new VehicleModel();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM vehicles WHERE Id = @Id", connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        foundVehicle.Id = Convert.ToInt32(reader["Id"]);
                        foundVehicle.User = Convert.ToInt32(reader["User"]);
                        foundVehicle.Make = reader["Make"].ToString();
                        foundVehicle.Model = reader["Model"].ToString();
                        foundVehicle.Year = Convert.ToInt32(reader["Year"]);
                        foundVehicle.Color = reader["Color"].ToString();
                        foundVehicle.Price = (float)Convert.ToDouble(reader["Price"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return foundVehicle;
        }

        public List<VehicleModel> SearchVehicles(string term)
        {
            List<VehicleModel> foundVehicles = new List<VehicleModel>();

            try
            {

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM vehicles WHERE Model LIKE @Model", connection);
                    cmd.Parameters.AddWithValue("@Model", '%' + term + '%');

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        VehicleModel vehicle = new VehicleModel();
                        vehicle.Id = Convert.ToInt32(reader["Id"]);
                        vehicle.User = Convert.ToInt32(reader["User"]);
                        vehicle.Make = reader["Make"].ToString();
                        vehicle.Model = reader["Model"].ToString();
                        vehicle.Year = Convert.ToInt32(reader["Year"]);
                        vehicle.Color = reader["Color"].ToString();
                        vehicle.Price = (float)Convert.ToDouble(reader["Price"]);
                        foundVehicles.Add(vehicle);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return foundVehicles;
        }

        public int UpdateVehicle(VehicleModel vehicle)
        {
            int newIdNumber = -1;

            try
            {

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE vehicles SET Make = @Make, Model = @Model, Year = @Year, Color = @Color, Price = @Price WHERE Id = @Id", connection);
                    cmd.Parameters.AddWithValue("@Make", vehicle.Make);
                    cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                    cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                    cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                    cmd.Parameters.AddWithValue("@Price", vehicle.Price);
                    cmd.Parameters.AddWithValue("@Id", vehicle.Id);

                    newIdNumber = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return newIdNumber;
        }
    }
}
