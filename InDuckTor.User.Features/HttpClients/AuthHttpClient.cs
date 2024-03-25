using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace InDuckTor.User.Features.HttpClients
{
    public record RegisterCredentialsRequest(string Login, string Password);

    public interface IAuthHttpClient
    {
        Task<HttpClientResponse<long>> RegisterCredentials(RegisterCredentialsRequest request);
    }
    public class AuthHttpClient : IAuthHttpClient
    {
        private HttpClient _client;

        public AuthHttpClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://89.19.214.8:6666");
        }

        [HttpPost]
        public async Task<HttpClientResponse<long>> RegisterCredentials(RegisterCredentialsRequest request)
        {
            //var json = JsonConvert.SerializeObject(request);
            //var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsJsonAsync("/credentials", request);

            if (!response.IsSuccessStatusCode) {
                return new HttpClientResponse<long>(false);
            }

            return new HttpClientResponse<long>(true, await response.Content.ReadAsAsync<long>());
        }
    }
}
