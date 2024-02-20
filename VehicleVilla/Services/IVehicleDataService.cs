using System.Runtime.InteropServices;
using VehicleVilla.Models;

namespace VehicleVilla.Services
{
    public interface IVehicleDataService
    {
        List<VehicleModel> AllVehicles();
        List<VehicleModel> SearchVehicles(string term);
        VehicleModel GetVehicleById(int id);
        int AddVehicle(VehicleModel vehicle);
        int DeleteVehicle(VehicleModel vehicle);
        int UpdateVehicle(VehicleModel vehicle);
        List<VehicleModel> GetVehiclesByUser(UserModel user);
    }
}
