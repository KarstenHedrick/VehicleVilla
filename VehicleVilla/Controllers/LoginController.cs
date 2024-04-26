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
        // Gather UserDAO
        UserDAO repo = new UserDAO();

        // Gather Logger and HttpContextAccessor
        private readonly IHttpContextAccessor context;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }

        /// <summary>
        /// Main Login Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            _log.Info("User Accessed Login Page."); // Logger INFO

            return View();
        }

        /// <summary>
        /// Logs user into account, and validates user is within the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Shows account details, and account edit page
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Edits and updates accoun within the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult ProcessEdit(UserModel user)
        {
            // Gather User from User ID
            user.Id = context.HttpContext.Session.GetInt32("id");
            repo.UpdateAccount(user);

            _log.Info("User Updated Account Information."); // Logger INFO

            return View("DisplayAccount", user);
        }

        /// <summary>
        /// Logs user out of the webpage, and clears context
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            context.HttpContext.Session.Remove("username");
            context.HttpContext.Session.Remove("id");

            _log.Info("User Logged Out Successfully. Cleared Session Variables."); // Logger INFO

            return View("../Home/Index");
        }
    }
}
