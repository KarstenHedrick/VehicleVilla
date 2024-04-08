using log4net;
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
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProductsController(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }

        public IActionResult Index()
        {
            _log.Info("User Accessed Browse Page."); // Logger INFO
            return View(repo.AllVehicles());
        }

        public IActionResult SearchForm()
        {
            _log.Info("User Accessed Search Page."); // Logger INFO
            return View();
        }

        public IActionResult SearchResults(string term)
        {
            _log.Info("User Searched Vehicles."); // Logger INFO

            List<VehicleModel> vehicleList = repo.SearchVehicles(term);
            return View("Index", vehicleList);
        }

        public IActionResult DeleteVehicle(int id)
        {
            _log.Info("User Deleted Vehicle by Vehicle ID."); // Logger INFO

            VehicleModel vehicle = repo.GetVehicleById(id);
            repo.DeleteVehicle(vehicle);

            return View("Index", repo.AllVehicles());
        }

        public IActionResult EditVehicle(int id)
        {
            _log.Info("User Accessed Vehicle Edit Form."); // Logger INFO

            context.HttpContext.Session.SetInt32("editId", (int)id);
            return View(repo.GetVehicleById(id));
        }

        public IActionResult ProcessEdit(VehicleModel vehicle)
        {
            // Gather Vehicle ID to Update
            int? id = context.HttpContext.Session.GetInt32("editId");

            if (id != null)
            {
                _log.Info("User Edited Vehicle by Vehicle ID."); // Logger INFO
                vehicle.Id = id;
                repo.UpdateVehicle(vehicle);
                return View("Index", repo.AllVehicles());
            }

            _log.Warn("Vehicle Could Not be Updated."); // Logger INFO

            return View("Index", repo.AllVehicles());
        }

        public IActionResult CreateVehicle()
        {
            _log.Info("User Accessed Vehicle Creation Page."); // Logger INFO

            return View("CreateVehicle");
        }

        public IActionResult ProcessCreate(VehicleModel vehicle)
        {
            UserModel user = userRepo.GetUserByUsername(context.HttpContext.Session.GetString("username"));
            vehicle.User = user.Id;

            repo.AddVehicle(vehicle);

            _log.Info("User Created a New Vehicle."); // Logger INFO

            return View("Index", repo.AllVehicles());
        }

        public IActionResult ShowVehicleDetails(int id)
        {
            _log.Info("User Accessed Vehicle Details."); // Logger INFO

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

                _log.Info("User Accessed User Listings Page."); // Logger INFO

                return View("MyListings", myVehicles);
            }
            else
            {
                _log.Warn("User Not Logged In! Redirected to Login Page."); // Logger INFO

                return View("LoginRequest");
            }
            
        }
    }
}
