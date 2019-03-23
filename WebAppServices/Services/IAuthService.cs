using WebApp.CoreModels.Models;

namespace WebAppServices.Services
{
    public interface IAuthService
    {

        bool IsUserValid(AccountModel account);
    }
}