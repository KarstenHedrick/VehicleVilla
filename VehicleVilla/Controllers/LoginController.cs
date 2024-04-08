using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleVilla.Models;
using VehicleVilla.Services;

namespace VehicleVilla.Controllers
{
    public class LoginController : Controller
    {
        UserDAO repo = new UserDAO();

        private readonly IHttpContextAccessor context;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }

        public IActionResult Index()
        {
            _log.Info("User Accessed Login Page."); // Logger INFO

            return View();
        }

        public IActionResult ProcessLogin(string username, string password)
        {
            bool verfied = repo.VerifyAccount(username, password);

            if (verfied)
            {
                // Gather Logged User Information
                UserModel user = repo.GetUserByUsername(username);
                context.HttpContext.Session.SetString("username", username);
                context.HttpContext.Session.SetInt32("id", (int) user.Id);
                
                _log.Info("User Login was Successful."); // Logger INFO

                return View("LoginSuccess", user);
            }
            else
            {
                _log.Warn("User Login was Unsuccesssful."); // Logger WARN

                return View("Index");
            }
        }

        public IActionResult DisplayAccount()
        {
            string logged = context.HttpContext.Session.GetString("username");

            if (logged != null)
            {
                UserModel user = repo.GetUserByUsername(logged);

                _log.Info("User Accessed Account Display"); // Logger INFO

                return View("DisplayAccount", user);
            }
            _log.Info("User Not Logged. Sent to Login Request"); // Logger INFO

            return View("../Products/LoginRequest");
        }

        public IActionResult ProcessEdit(UserModel user)
        {
            // Gather User from User ID
            user.Id = context.HttpContext.Session.GetInt32("id");
            repo.UpdateAccount(user);

            _log.Info("User Updated Account Information."); // Logger INFO

            return View("DisplayAccount", user);
        }

        public IActionResult Logout()
        {
            context.HttpContext.Session.Remove("username");
            context.HttpContext.Session.Remove("id");

            _log.Info("User Logged Out Successfully. Cleared Session Variables."); // Logger INFO

            return View("../Home/Index");
        }
    }
}
