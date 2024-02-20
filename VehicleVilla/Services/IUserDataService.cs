using VehicleVilla.Models;

namespace VehicleVilla.Services
{
    public interface IUserDataService
    {
        bool VerifyAccount(string user, string pass);
        int CreateAccount(UserModel user);
        UserModel GetUserByUsername(string username);
        int UpdateAccount(UserModel user);
    }
}
