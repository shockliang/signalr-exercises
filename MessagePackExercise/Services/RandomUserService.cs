using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MessagePackExercise.Services
{
    public class RandomUserService : IRandomUserService
    {
        const string URL = "https://randomuser.me/api/";
        private readonly HttpClient httpClient;

        public RandomUserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        
        public async Task<IEnumerable<RandomUser>> GetUsers(int max = 1)
        {
            if (max > 5000) max = 5000;

            var jsonResult = await httpClient.GetStringAsync($"{URL}?results={max}");
            var randomUserRoot = JsonSerializer.Deserialize<RandomUserRoot>(jsonResult);

            return randomUserRoot.Results;
        }
    }
}