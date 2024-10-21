using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PetAdoption.Shared;
namespace PetAdoption.Client.Services
{
    public class OwnerService
    {
        private readonly HttpClient _httpClient;

        public OwnerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OwnerModel>> GetOwnersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<OwnerModel>>("api/owner");
        }

        public async Task<OwnerModel> AddOwnerAsync(OwnerModel owner)
        {
            var response = await _httpClient.PostAsJsonAsync("api/owner", owner);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OwnerModel>();
        }

        public async Task<OwnerModel> UpdateOwnerAsync(int id, OwnerModel owner)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/owner/{id}", owner);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OwnerModel>();
        }

        public async Task DeleteOwnerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/owner/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
