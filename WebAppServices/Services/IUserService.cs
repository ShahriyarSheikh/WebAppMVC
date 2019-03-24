using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.CoreModels.Models;

namespace WebAppServices.Services
{
    public interface IUserService
    {
        List<UserModel> GetUsers(int currentPage, int pageSize = 0);

        Task<bool> DeleteUser(int id);

        Task<bool> CreateUser(UserModel user);

        UserModel SearchUser(int Id);

        Task<int> GetCount();
    }
}