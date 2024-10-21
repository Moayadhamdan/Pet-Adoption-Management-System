using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PetAdoption.Shared;
namespace PetAdoption.Client.Services
{
    public class AdoptionService
    {
        private readonly HttpClient _httpClient;

        public AdoptionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AdoptionModel>> GetAdoptionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AdoptionModel>>("api/adoption");
        }

        public async Task<AdoptionModel> AddAdoptionAsync(AdoptionModel adoption)
        {
            var response = await _httpClient.PostAsJsonAsync("api/adoption", adoption);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AdoptionModel>();
        }

        public async Task<AdoptionModel> UpdateAdoptionAsync(int id, AdoptionModel adoption)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/adoption/{id}", adoption);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AdoptionModel>();
        }

        public async Task DeleteAdoptionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/adoption/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
