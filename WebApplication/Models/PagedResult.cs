using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.CoreModels.Models;
using WebAppServices.Services;

namespace WebApplication.Models
{
    public class PagedResult
    {
        private readonly IUserService _userService;

        public PagedResult(IUserService userService)
        {
            _userService = userService;
            this.OnGetAsync();
        }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext =>CurrentPage<TotalPages;
        public int PageNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 15;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public List<User> Element { get; set; }

        public void  OnGetAsync()
        {
            Element = ConvertToUser(_userService.GetUsers(CurrentPage, PageSize));
            Count =  _userService.GetCount().Result;
        }

        private List<User> ConvertToUser(List<UserModel> users) {
            var list = new List<User>();
            foreach (var lis in users) {
                list.Add(new User {
                    Email = lis.Email,
                    FirstName = lis.FirstName,
                    Id = lis.Id,
                    LastName = lis.LastName
                });
            }
            return list;
        }
    }
}
