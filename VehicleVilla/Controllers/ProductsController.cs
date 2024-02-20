using Microsoft.AspNetCore.Mvc;
using VehicleVilla.Models;
using VehicleVilla.Services;

namespace VehicleVilla.Controllers
{
    public class ProductsController : Controller
    {
        VehicleDAO repo = new VehicleDAO();
        public IActionResult Index()
        {
            return View(repo.AllVehicles());
        }

        public IActionResult SearchForm()
        {
            return View();
        }

        public IActionResult SearchResults(string term)
        {
            List<VehicleModel> vehicleList = repo.SearchVehicles(term);
            return View("Index", vehicleList);
        }

        public IActionResult DeleteVehicle(int id)
        {
            VehicleModel vehicle = repo.GetVehicleById(id);
            repo.DeleteVehicle(vehicle);

            return View("Index", repo.AllVehicles());
        }

        public IActionResult EditVehicle(int id)
        {
            return View(repo.GetVehicleById(id));
        }

        public IActionResult ProcessEdit(VehicleModel vehicle)
        {
            repo.UpdateVehicle(vehicle);
            return View("Index", repo.AllVehicles());
        }

        public IActionResult CreateVehicle()
        {
            return View("CreateVehicle");
        }

        public IActionResult ProcessCreate(VehicleModel vehicle)
        {
            repo.AddVehicle(vehicle);
            return View("Index", repo.AllVehicles());
        }

        public IActionResult ShowVehicleDetails(int id)
        {
            return View(repo.GetVehicleById(id));
        }

        public IActionResult MyListings()
        {
            return View();
        }
    }
}
