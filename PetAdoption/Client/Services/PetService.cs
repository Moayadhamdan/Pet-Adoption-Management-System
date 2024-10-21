using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using PetAdoption.Shared;

namespace PetAdoption.Client.Services
{
    public class PetService
    {
        private readonly HttpClient _httpClient;

        public PetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PetModel>> GetPetsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PetModel>>("api/pet");
        }

        public async Task<PetModel> AddPetAsync(PetModel pet)
        {
            var response = await _httpClient.PostAsJsonAsync("api/pet", pet);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PetModel>();
        }

        public async Task<PetModel> UpdatePetAsync(int id, PetModel pet)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/pet/{id}", pet);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PetModel>();
        }

        public async Task DeletePetAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/pet/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
