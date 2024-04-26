using log4net;
using Microsoft.AspNetCore.Mvc;
using VehicleVilla.Models;
using VehicleVilla.Services;

namespace VehicleVilla.Controllers
{
    public class RegisterController : Controller
    {
        UserDAO repo = new UserDAO();

        // Gather Logger
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Main Register Input Form
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            _log.Info("User Accessed Register Page."); // Logger INFO

            return View();
        }

        /// <summary>
        /// REgisters New Account into the Database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult ProcessRegister(UserModel user)
        {
            _log.Info("User Created a New Account."); // Logger INFO

            repo.CreateAccount(user);
            return View("ProcessRegister", user);
        }
    }
}
