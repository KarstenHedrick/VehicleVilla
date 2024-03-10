using MySqlConnector;
using VehicleVilla.Models;

namespace VehicleVilla.Services
{
    public class UserDAO : IUserDataService
    {
        string connectionString = "Server=vehiclevilla.mysql.database.azure.com;User ID=karsten;Password=Sn@wmobile119629;port=3306;Database=vehiclevilla";

        public int CreateAccount(UserModel user)
        {
            int newIdNumber = -1;

            try
            {

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO users (Username, Password, Email, Firstname, Lastname) VALUES (@Username,@Password,@Email,@Firstname,@Lastname)", connection);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Firstname", user.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", user.Lastname);


                    newIdNumber = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return newIdNumber;
        }

        public UserModel GetUserByUsername(string username)
        {
            UserModel user = new UserModel();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE Username = @Username", connection);
                    cmd.Parameters.AddWithValue("@Username", username);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.Username = reader["Username"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.Firstname = reader["Firstname"].ToString();
                        user.Lastname = reader["Lastname"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return user;
        }

        public int UpdateAccount(UserModel user)
        {
            int newIdNumber = -1;

            try
            {

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE users SET Username = @Username, Password = @Password, Email = @Email, Firstname = @Firstname, Lastname = @Lastname WHERE Id = @Id", connection);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Firstname", user.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", user.Lastname);
                    cmd.Parameters.AddWithValue("@Id", user.Id);

                    newIdNumber = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return newIdNumber;
        }

        public bool VerifyAccount(string username, string password)
        {
            UserModel foundUser = new UserModel();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE Username = @Username", connection);
                    cmd.Parameters.AddWithValue("@Username", username);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        foundUser.Id = Convert.ToInt32(reader["Id"]);
                        foundUser.Username = reader["Username"].ToString();
                        foundUser.Password = reader["Password"].ToString();
                        foundUser.Email = reader["Email"].ToString();
                        foundUser.Firstname = reader["Firstname"].ToString();
                        foundUser.Lastname = reader["Lastname"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            if (username == foundUser.Username && password == foundUser.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
