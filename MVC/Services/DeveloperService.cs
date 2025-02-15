using Newtonsoft.Json;
using MVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MVC.Services
{
    public class DeveloperService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public DeveloperService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        // 1. GET all developers
        public async Task<List<Developer>> GetAllDevs()
        {
            //var response = await _httpClient.GetStringAsync(_apiBaseUrl + "developer");
            //var developers = JsonConvert.DeserializeObject<List<Developer>>(response);
            //return developers;

            var response = await _httpClient.GetStringAsync(_apiBaseUrl + "developer");
            return JsonConvert.DeserializeObject<List<Developer>>(response);
        }

        // 2. GET a single developer by ID
        public async Task<Developer> GetDevDetails(int id)
        {
            var response = await _httpClient.GetStringAsync(_apiBaseUrl + "developer/" + id);
            var developer = JsonConvert.DeserializeObject<Developer>(response);
            return developer;
        }

        // 3. POST (Create) a new developer
        public async Task<Developer> CreateNewDev(Developer developer)
        {
            var json = JsonConvert.SerializeObject(developer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl + "developer", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var createdDeveloper = JsonConvert.DeserializeObject<Developer>(responseString);

            return createdDeveloper;
        }

        // 4. PUT (Update) an existing developer
        public async Task<Developer> UpdateDev(int id, Developer developer)
        {
            var json = JsonConvert.SerializeObject(developer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(_apiBaseUrl + "developer/" + id, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var updatedDeveloper = JsonConvert.DeserializeObject<Developer>(responseString);

            return updatedDeveloper;
        }

        // 5. DELETE a developer by ID
        public async Task<bool> DeleteDev(int id)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + "developer/" + id);
            return response.IsSuccessStatusCode;  // returns true if delete was successful
        }
    }
}
