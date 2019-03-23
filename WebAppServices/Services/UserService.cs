using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.CoreModels.Models;
using WebAppServices.Helpers;

namespace WebAppServices.Services
{
    public class UserService : IUserService
    {
        private List<UserModel> userModels;
        private string path = "https://wonderasptest.azurewebsites.net";
        HttpUtility httpHelper;
        public UserService()
        {
            httpHelper =  new HttpUtility(path);
            var users = new UserModel();
            userModels = users.GenerateUserModelsMock();
        }

        public bool CreateUser(UserModel user)
        {
            var result = httpHelper.CreateUser(user);
            userModels.Append(user);
            return true;
        }

        public bool DeleteUser(int id)
        {
            var result = httpHelper.DeleteUser(id);
            var modelToRemove = userModels.Where(x => x.Id == id).FirstOrDefault();
            userModels.Remove(modelToRemove);
            return true;
        }

        public List<UserModel> GetUsers(int page, int skip=0, int limit=0)
        {
            var result = httpHelper.GetListOfUsers();
            var toSkip = (page * skip);
            return userModels.Skip(toSkip).Take(limit).ToList();
            //return userModels;
        }


        public UserModel SearchUser(int id)
        {
            var result = httpHelper.SearchUser(id);
            return userModels.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
