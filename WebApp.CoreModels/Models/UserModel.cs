using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace WebApp.CoreModels.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<UserModel> GenerateUserModelsMock() {

            var listOfUsers = new List<UserModel>();

            for (int i = 0; i < 15; i++) {
                listOfUsers.Add(new UserModel
                {
                    Email = $"johndoe{i}@gmail.com",
                    FirstName = $"john{i}",
                    Id = i,
                    LastName = $"doe{i}"
                });
            }

            return listOfUsers;
        }

    }



}
