using Microsoft.AspNetCore.Mvc;
using VehicleVilla.Models;
using VehicleVilla.Services;

namespace VehicleVilla.Controllers
{
    public class RegisterController : Controller
    {
        UserDAO repo = new UserDAO();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRegister(UserModel user)
        {
            repo.CreateAccount(user);
            return View("ProcessRegister", user);
        }
    }
}
