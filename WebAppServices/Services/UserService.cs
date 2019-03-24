using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<bool> CreateUser(UserModel user)
        {
            var result = await httpHelper.CreateUser(user);
            userModels.Append(user);
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var result = await httpHelper.DeleteUser(id);
            var modelToRemove = userModels.Where(x => x.Id == id).FirstOrDefault();
            userModels.Remove(modelToRemove);
            return true;
        }

        public List<UserModel> GetUsers(int currentPage, int pageSize=0)
        {
            var result = httpHelper.GetListOfUsers();
            return userModels.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            //return userModels;
        }


        public async Task<int> GetCount()
        {
            return userModels.Count;
        }



        public UserModel SearchUser(int id)
        {
            var result = httpHelper.SearchUser(id);
            return userModels.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
