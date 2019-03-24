using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebAppServices.Services;

namespace WebApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            ViewBag.status = true;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account UserAccount) {

            var doesUserExist = _authService.IsUserValid(new WebApp.CoreModels.Models.AccountModel
            {
                Password = UserAccount.Password.ToLowerInvariant(),
                UserName = UserAccount.UserName.ToLowerInvariant()
            });

            if (doesUserExist)
                //Redirect here
               return Redirect("~/Home/Index");

            ViewBag.Status = doesUserExist;
            return View("Index");
        }

    }
}
