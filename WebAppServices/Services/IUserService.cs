using System.Collections.Generic;
using WebApp.CoreModels.Models;

namespace WebAppServices.Services
{
    public interface IUserService
    {
        List<UserModel> GetUsers(int page, int skip, int limit);

        bool DeleteUser(int id);

        bool CreateUser(UserModel user);

        UserModel SearchUser(int Id);
    }
}