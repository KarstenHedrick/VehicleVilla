using log4net;
using Microsoft.AspNetCore.Mvc;
using VehicleVilla.Models;
using VehicleVilla.Services;

namespace VehicleVilla.Controllers
{
    public class ProductsController : Controller
    {
        // Gather VehicleDAO and UserDAO
        VehicleDAO repo = new VehicleDAO();
        UserDAO userRepo = new UserDAO();

        // Gather Logger and HttpContextAccessor
        private readonly IHttpContextAccessor context;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProductsController(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }

        /// <summary>
        /// Main Products Browse Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            _log.Info("User Accessed Browse Page."); // Logger INFO
            return View(repo.AllVehicles());
        }

        /// <summary>
        /// Form to Search for Vehicles
        /// </summary>
        /// <returns></returns>
        public IActionResult SearchForm()
        {
            _log.Info("User Accessed Search Page."); // Logger INFO
            return View();
        }

        /// <summary>
        /// Gathers and Lists Search Results
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public IActionResult SearchResults(string term)
        {
            _log.Info("User Searched Vehicles."); // Logger INFO

            List<VehicleModel> vehicleList = repo.SearchVehicles(term);
            return View("Index", vehicleList);
        }
        
        /// <summary>
        /// Removes User from the Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteVehicle(int id)
        {
            _log.Info("User Deleted Vehicle by Vehicle ID."); // Logger INFO

            VehicleModel vehicle = repo.GetVehicleById(id);
            repo.DeleteVehicle(vehicle);

            return View("Index", repo.AllVehicles());
        }

        /// <summary>
        /// Edit Vehicle Input Form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult EditVehicle(int id)
        {
            _log.Info("User Accessed Vehicle Edit Form."); // Logger INFO

            context.HttpContext.Session.SetInt32("editId", (int)id);
            return View(repo.GetVehicleById(id));
        }

        /// <summary>
        /// Updates Vehicle Information within the Database
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create Vehicle Input Form
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateVehicle()
        {
            _log.Info("User Accessed Vehicle Creation Page."); // Logger INFO

            return View("CreateVehicle");
        }

        /// <summary>
        /// Creates a new vehicle in the databse
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public IActionResult ProcessCreate(VehicleModel vehicle)
        {
            UserModel user = userRepo.GetUserByUsername(context.HttpContext.Session.GetString("username"));
            vehicle.User = user.Id;

            repo.AddVehicle(vehicle);

            _log.Info("User Created a New Vehicle."); // Logger INFO

            return View("Index", repo.AllVehicles());
        }

        /// <summary>
        /// Shows Vehicle Information base on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult ShowVehicleDetails(int id)
        {
            _log.Info("User Accessed Vehicle Details."); // Logger INFO

            return View(repo.GetVehicleById(id));
        }

        /// <summary>
        /// Displays a List of Vehicles created by logged user
        /// </summary>
        /// <returns></returns>
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
