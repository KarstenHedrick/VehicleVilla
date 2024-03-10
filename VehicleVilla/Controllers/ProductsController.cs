using Microsoft.AspNetCore.Mvc;
using VehicleVilla.Models;
using VehicleVilla.Services;

namespace VehicleVilla.Controllers
{
    public class ProductsController : Controller
    {
        VehicleDAO repo = new VehicleDAO();
        UserDAO userRepo = new UserDAO();

        private readonly IHttpContextAccessor context;

        public ProductsController(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }

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
            context.HttpContext.Session.SetInt32("editId", (int)id);
            return View(repo.GetVehicleById(id));
        }

        public IActionResult ProcessEdit(VehicleModel vehicle)
        {
            int? id = context.HttpContext.Session.GetInt32("editId");
            if (id != null)
            {
                vehicle.Id = id;
                repo.UpdateVehicle(vehicle);
                return View("Index", repo.AllVehicles());
            }
            return View("Index", repo.AllVehicles());
        }

        public IActionResult CreateVehicle()
        {
            return View("CreateVehicle");
        }

        public IActionResult ProcessCreate(VehicleModel vehicle)
        {
            UserModel user = userRepo.GetUserByUsername(context.HttpContext.Session.GetString("username"));
            vehicle.User = user.Id;

            repo.AddVehicle(vehicle);
            return View("Index", repo.AllVehicles());
        }

        public IActionResult ShowVehicleDetails(int id)
        {
            return View(repo.GetVehicleById(id));
        }

        public IActionResult MyListings()
        {
            string logged = context.HttpContext.Session.GetString("username");

            if (logged != null)
            {
                List<VehicleModel> myVehicles = new List<VehicleModel>();
                UserModel user = userRepo.GetUserByUsername(logged);


                myVehicles = repo.GetVehiclesByUser(user);

                return View("MyListings", myVehicles);
            }
            else
            {
                return View("LoginRequest");
            }
            
        }
    }
}
