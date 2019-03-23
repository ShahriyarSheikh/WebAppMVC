using System.Threading.Tasks;
using WebApp.CoreModels.Models;
using WebAppData.Models;

namespace WebAppServices.Services
{
    public interface ISearchHistory
    {
        Task<Movies> GetMovieByMovieName(int userId,string name);
        UserSessions GetBy(int id);
    }
}