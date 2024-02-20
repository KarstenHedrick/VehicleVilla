using Microsoft.AspNetCore.Mvc;
using VehicleVilla.Models;
using VehicleVilla.Services;

namespace VehicleVilla.Controllers
{
    public class LoginController : Controller
    {
        UserDAO repo = new UserDAO();

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
                return View("LoginSuccess", user);
            }
            else
            {
                return View("Index");
            }
        }
    }
}
