using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VehicleVilla.Models;

//log4net Logging
using log4net;


namespace VehicleVilla.Controllers
{
    public class HomeController : Controller
    {
        // Gather Logger and HttpContextAccessor
        private readonly IHttpContextAccessor context;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }

        /// <summary>
        /// Home Page of Application
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // Get Logged User
            string logged = context.HttpContext.Session.GetString("username");

            // Logger Info
            _log.Info("User Accessed Home Page");

            if (logged != null)
            {
                return View("HomeWelcome");
            }
            return View();
        }

        /// <summary>
        /// Project Privacy Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _log.Error("An error occurred.");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
