using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.CoreModels.Models;
using WebAppData;
using WebAppData.Models;
using WebAppServices.Helpers;

namespace WebAppServices.Services
{
    public class SearchHistory : ISearchHistory
    {
        private readonly DatabaseContext _context;
        private readonly HttpUtility httpHelper;

        public SearchHistory(DatabaseContext context)
        {
            _context = context;
            httpHelper = new HttpUtility("");
        }

        public async Task<Movies> GetMovieByMovieName(int userId ,string name)
        {
            var (result, statusCode) = await httpHelper.SearchMovie(name);
            await AddHistoryToDb(userId, result);

            return result;
        }

        public IEnumerable<UserSessions> GetAll(int userId) {
            return _context.UserSession.Where(x => x.Id == userId);
        }

        private async Task AddHistoryToDb(int userId, Movies result)
        {
            var userSession = new UserSessions()
            {
                MovieName = result.Name,
                UserId = userId,
                ReleaseDate = result.ReleasedDate,
                SearchedOn = DateTime.UtcNow
            };
            _context.Add(userSession);
            //await _context.SaveChangesAsync();
        }

        public UserSessions GetBy(int id)
        {
            return _context.UserSession.FirstOrDefault(x=>x.Id == id);
        }
    }
}
