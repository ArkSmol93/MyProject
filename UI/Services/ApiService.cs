using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UI.Models;

public class ApiService
{
    private readonly HttpClient _http;

    public ApiService()
    {
        _http = new HttpClient();
        _http.BaseAddress = new System.Uri("https://localhost:5001/");
    }

    // Rejestracja
    public async Task<bool> RegisterUser(User user)
    {
        var response = await _http.PostAsJsonAsync("api/users/register", user);
        return response.IsSuccessStatusCode;
    }

    // Logowanie
    public async Task<string> Login(User user)
    {
        var response = await _http.PostAsJsonAsync("api/users/login", user);
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadAsStringAsync(); // token JWT
    }

    // Pobierz zasoby
    public async Task<List<Resource>> GetResources()
    {
        var response = await _http.GetAsync("api/resources");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Resource>>(json);
    }

    // Dodaj zasób
    public async Task<bool> CreateResource(Resource resource)
    {
        var response = await _http.PostAsJsonAsync("api/resources", resource);
        return response.IsSuccessStatusCode;
    }

    // Edytuj zasób
    public async Task<bool> UpdateResource(Resource resource)
    {
        var response = await _http.PutAsJsonAsync($"api/resources/{resource.Id}", resource);
        return response.IsSuccessStatusCode;
    }

    // Usuń zasób
    public async Task<bool> DeleteResource(int id)
    {
        var response = await _http.DeleteAsync($"api/resources/{id}");
        return response.IsSuccessStatusCode;
    }
}
