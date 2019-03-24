using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;
using WebAppServices.Services;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISearchHistory _searchService;
        private readonly PagedResult pagedResult;
        public static int pageNumber = 0;
        public static bool isInitialized = false;

        public UserController(IUserService userService, ISearchHistory searchHistory)
        {
            _userService = userService;
            _searchService = searchHistory;

        }
        public IActionResult Index()
        {
            var listOfUsers = _userService.GetUsers(1, 1);
            var modelToReturn = new List<User>();
            foreach (var user in listOfUsers)
            {
                modelToReturn.Add(new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
            var pagedProperty = new PagedResult(_userService);
            
            ViewBag.PageNumber = pageNumber;
            return View(pagedProperty);
        }

        [HttpGet]
        public IActionResult GetListOfUsers() {

            var user = new List<User>() { new User {Id = 0,Email = "0jd@gmail.com",FirstName="0j",LastName = "0d" },
                new User {Id = 1,Email = "1jd@gmail.com",FirstName="1j",LastName = "1d" },
                new User {Id = 2,Email = "2jd@gmail.com",FirstName="2j",LastName = "2d" },
                new User {Id = 3,Email = "3jd@gmail.com",FirstName="3j",LastName = "3d" } };

            return View(user);
        }

        public IActionResult CreateUser() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(User user) {
            var result = await _userService.CreateUser(new WebApp.CoreModels.Models.UserModel {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName
            });

            if (result) {
                return Redirect("Index");
            }

            return Redirect("~/Shared/Error");
        }

     
        public async Task<IActionResult> DeleteUser(string id) {
            var result = await _userService.DeleteUser(Convert.ToInt32(id));

            if (result)
                return Redirect("/User");

            return View("~/Shared/Error");
        }

        [HttpGet]
        public IActionResult SearchUser(string id) {
            var result = _userService.SearchUser(Convert.ToInt32(id));
            if (result == null)
                return View("SearchedUser", null);

            return View("SearchedUser",new User {
                LastName = result.LastName,
                Id = result.Id,
                FirstName = result.FirstName,
                Email = result.Email
            });
        }

        public async Task<IActionResult> SearchMovie(int id,string movieName) {
            var result = await _searchService.GetMovieByMovieName(id,movieName);
            var movieResponse = new Movie
            {
                ReleasedDate = result.ReleasedDate,
                Genre = result.Genre,
                Name = result.Name
            };

            return View(movieResponse);

        }


    }
}
