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

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(string username, string password)
        {
            bool verfied = repo.VerifyAccount(username, password);

            if (verfied)
            {
                UserModel user = repo.GetUserByUsername(username);
                context.HttpContext.Session.SetString("username", username);
                context.HttpContext.Session.SetInt32("id", (int) user.Id);
                

                return View("LoginSuccess", user);
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult DisplayAccount()
        {
            string logged = context.HttpContext.Session.GetString("username");


            if (logged != null)
            {
                UserModel user = repo.GetUserByUsername(logged);
                return View("DisplayAccount", user);
            }
            return View("../Products/LoginRequest");
        }

        public IActionResult ProcessEdit(UserModel user)
        {
            user.Id = context.HttpContext.Session.GetInt32("id");
            repo.UpdateAccount(user);
            return View("DisplayAccount", user);
        }

        public IActionResult Logout()
        {
            context.HttpContext.Session.Remove("username");
            context.HttpContext.Session.Remove("id");
            return View("../Home/Index");
        }
    }
}
