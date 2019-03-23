using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.CoreModels.Models;

namespace WebAppServices.Helpers
{
    public class HttpUtility
    {
        private static HttpClientHandler httpClientHandler = new HttpClientHandler();
        private static HttpClient client;
        readonly bool _ignoreSslErrors ;
        private readonly string _path;

        public HttpUtility(string path, bool ignoreSslErrors = false)
        {
            _path = path;
            _ignoreSslErrors = ignoreSslErrors;
        }

        public async Task<(List<UserModel>,HttpStatusCode)> GetListOfUsers() {

            EnsureClient();

            var response = await client.GetAsync($"{_path}/api/test");

            if (response.StatusCode != HttpStatusCode.OK) {
                return (null, response.StatusCode);
            }

            var responseContent = await response.Content.ReadAsAsync<List<UserModel>>();
            return (responseContent, HttpStatusCode.OK);

        }

        public async Task<(bool, HttpStatusCode)> DeleteUser(int id) {

            EnsureClient();
            var response = await client.DeleteAsync($"{_path}/api/test/{id}");
            if (response.StatusCode != HttpStatusCode.OK) {
                return (false, response.StatusCode);
            }
            var responseContent = await response.Content.ReadAsAsync<DResponse>();
            return (responseContent.Message != null ? false : true, HttpStatusCode.OK);
        }

        public async Task<(UserModel, HttpStatusCode)> SearchUser(int id)
        {

            EnsureClient();
            var response = await client.GetAsync($"{_path}/api/test/{id}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return (null, response.StatusCode);
            }
            var responseContent = await response.Content.ReadAsAsync<UserModel>();
            return (responseContent, HttpStatusCode.OK);
        }

        public async Task<(DResponse, HttpStatusCode)> CreateUser(UserModel user) {

            EnsureClient();
                var userModelSerialized = JsonConvert.SerializeObject(user);
                var response = await client.PostAsync($"{_path}/api/test", new StringContent(userModelSerialized, Encoding.UTF8, "application/json"));

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return (null, response.StatusCode);
                }
                //TODO: after determining proper type, cast here
                var responseContent = await response.Content.ReadAsAsync<DResponse>();
                return (responseContent, response.StatusCode);
        }


        public async Task<(Movies, HttpStatusCode)> SearchMovie(string name) {

            EnsureClient();
            var response = await client.GetAsync($"http://www.omdbapi.com/?t={name.Replace(" ","+")}&apikey=8236ca1");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return (null, response.StatusCode);
            }
            var responseContent = await response.Content.ReadAsAsync<MovieSearchResult>();
            var ToMovieModel = new Movies {
                Genre = responseContent.Genre,
                Name = responseContent.Title,
                ReleasedDate = Convert.ToDateTime(responseContent.Released)
            };
            return (ToMovieModel, HttpStatusCode.OK);
        }

        private void EnsureClient()
        {
            if (client != null) return;

            SetupCertCheckIgnoreForDebug();

            client = new HttpClient(httpClientHandler);
            var baseAddress = this.GetServerUri();
            client.BaseAddress = baseAddress;
        }

        private void SetupCertCheckIgnoreForDebug()
        {
            if (!_ignoreSslErrors) return;

            httpClientHandler.ServerCertificateCustomValidationCallback = (message,cert, chain, errors) => { return true; };
        }

        public Uri GetServerUri()
        {
            var parsedUrl = new Uri(_path);
            return parsedUrl;
        }



    }

    public class DResponse {
        public string Message { get; set; }
        
    }
}
