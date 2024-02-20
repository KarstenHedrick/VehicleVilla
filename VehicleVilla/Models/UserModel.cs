namespace VehicleVilla.Models
{
    public class UserModel
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public UserModel(int id, string username, string password, string email, string firstname, string lastname)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
        }

        public UserModel() { }
    }
}
