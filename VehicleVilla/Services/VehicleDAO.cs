using log4net;
using MySqlConnector;
using VehicleVilla.Models;

namespace VehicleVilla.Services
{
    public class VehicleDAO : IVehicleDataService
    {
        string connectionString = "Server=vehiclevilla.mysql.database.azure.com;User ID=karsten;Password=Sn@wmobile119629;port=3306;Database=vehiclevilla";
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int AddVehicle(VehicleModel vehicle)
        {
            int newIdNumber = -1;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO vehicles (User, Make, Model, Year, Color, Price) VALUES (@User,@Make,@Model,@Year,@Color,@Price)", connection);
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
                _log.Error("AddVehicle Method Failed. Exception: ", ex); // Logger ERROR
                Console.Write(ex.Message);
            }

            _log.Info("AddVehicle Accessed. Vehicle was Created within the Database."); // Logger INFO

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
                _log.Error("AllVehicles Method Failed. Exception: ", ex); // Logger ERROR
                Console.Write(ex.Message);
            }

            _log.Info("AllVehicles Accessed. Vehicles were Gathered from the Database."); // Logger INFO

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
                _log.Error("DeleteVehicle Method Failed. Exception: ", ex); // Logger ERROR
                Console.Write(ex.Message);
            }

            _log.Info("DeleteVehicle Accessed. Vehicle was Deleted from the Database."); // Logger INFO

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
                _log.Error("GetVehicleById Method Failed. Exception: ", ex); // Logger ERROR
                Console.Write(ex.Message);
            }

            _log.Info("GetVehicleById Accessed. Vehicle was Gathered from the Database."); // Logger INFO

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
                _log.Error("SearchVehicles Method Failed. Exception: ", ex); // Logger ERROR
                Console.Write(ex.Message);
            }

            _log.Info("SearchVehicles Accessed. Searched Vehicles were Gathered from the Database."); // Logger INFO

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
                _log.Error("UpdateVehicle Method Failed. Exception: ", ex); // Logger ERROR
                Console.Write(ex.Message);
            }

            _log.Info("UpdateVehicle Accessed. Vehicle was Updated within the Database."); // Logger INFO

            return newIdNumber;
        }

        public List<VehicleModel> GetVehiclesByUser(UserModel user)
        {
            List<VehicleModel> foundVehicles = new List<VehicleModel>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM vehicles WHERE User = @User", connection);
                    cmd.Parameters.AddWithValue("@User", user.Id);
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
                _log.Error("GetVehiclesByUser Method Failed. Exception: ", ex); // Logger ERROR
                Console.Write(ex.Message);
            }

            _log.Info("GetVehiclesByUser Accessed. Vehicles were Gathered from the Database."); // Logger INFO

            return foundVehicles;
        }
    }
}
